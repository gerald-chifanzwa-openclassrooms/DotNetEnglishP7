using Dot.Net.WebApi.Domain;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Validators;

namespace Dot.Net.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper(config =>
            {
                config.CreateMap<BidListModel, BidList>();
                config.CreateMap<CurvePointModel, CurvePoint>();
                config.CreateMap<RatingModel, Rating>();
                config.CreateMap<RuleNameModel, RuleName>();
            });
            services.AddFluentValidation();

            services.AddScoped<IBidRepository, BidRepository>();
            services.AddScoped<ICurvePointRepository, CurvePointRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IRuleRepository, RuleRepository>();

            services.AddTransient<IValidator<BidListModel>, BidListModelValidator>();
            services.AddTransient<IValidator<CurvePointModel>, CurvePointModelValidator>();
            services.AddTransient<IValidator<RatingModel>, RatingModelValidator>();
            services.AddTransient<IValidator<RuleNameModel>, RuleNameModelValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
