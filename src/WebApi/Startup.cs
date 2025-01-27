using System.Text;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using WebApi.Domain;
using WebApi.Middleware;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Services;
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
            services.AddControllers()
                    .AddMvcOptions(options => options.Filters.Add(new ExceptionHandler()));
            services.AddAutoMapper(config =>
            {
                config.CreateMap<BidListModel, BidList>();
                config.CreateMap<CurvePointModel, CurvePoint>();
                config.CreateMap<RatingModel, Rating>();
                config.CreateMap<RuleNameModel, RuleName>();
                config.CreateMap<User, UserViewModel>();
            });
            services.AddFluentValidation();

            services.AddDbContext<LocalDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));
            services.AddScoped<IBidRepository, BidRepository>();
            services.AddScoped<ICurvePointRepository, CurvePointRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IRuleRepository, RuleRepository>();
            services.AddScoped<ITradeRepository, TradeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IPasswordHashService, PasswordHashService>();
            services.AddScoped<IUsersService, UsersService>();

            services.AddTransient<IValidator<BidListModel>, BidListModelValidator>();
            services.AddTransient<IValidator<CurvePointModel>, CurvePointModelValidator>();
            services.AddTransient<IValidator<RatingModel>, RatingModelValidator>();
            services.AddTransient<IValidator<RuleNameModel>, RuleNameModelValidator>();
            services.AddTransient<IValidator<TradeModel>, TradeModelValidator>();
            services.AddTransient<IValidator<LoginViewModel>, LoginViewModelValidator>();
            services.AddTransient<IValidator<UserModel>, UserModelValidator>();
            services.Configure<AuthenticationOptions>(Configuration.GetSection(nameof(AuthenticationOptions)));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                var authenticationOptions = new AuthenticationOptions();
                Configuration.GetSection(nameof(AuthenticationOptions)).Bind(authenticationOptions);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = authenticationOptions.Issuer,
                    ValidAudience = authenticationOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationOptions.Key))
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<LocalDbContext>();
                    dbContext.Database.EnsureCreated();
                }
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
