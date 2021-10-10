using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Repositories
{
    public class RuleRepository : IRuleRepository
    {
        private readonly LocalDbContext _dbContext;
        private readonly ILogger<RuleRepository> _logger;

        public RuleRepository(LocalDbContext dbContext, ILogger<RuleRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IReadOnlyCollection<RuleName>> Add(RuleName rule)
        {
            if (rule == null) throw new ArgumentNullException(nameof(rule));

            _logger.LogInformation("Adding new rule {@RuleName}", rule);
            _dbContext.Rules.Add(rule);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Rule Saved {@RuleName}", rule);

            return await _dbContext.Rules.ToListAsync();
        }

        public async Task<IReadOnlyCollection<RuleName>> Update(int id, RuleName rule)
        {
            if (rule == null) throw new ArgumentNullException(nameof(rule));

            var ruleItem = await _dbContext.Rules.FirstOrDefaultAsync(b => b.Id == id);
            if (ruleItem == null) throw new EntityNotFoundException();

            _logger.LogInformation("Updating rule {@RuleItem} to {@Rule}", ruleItem, rule);

            ruleItem.Json = rule.Json;
            rule.SqlPart = rule.SqlPart;
            rule.SqlStr = rule.SqlStr;
            rule.Description = rule.Description;
            rule.Name = rule.Name;
            rule.Template = rule.Template;

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Rule Saved {@Rule}", rule);

            return await _dbContext.Rules.ToListAsync();
        }

        public async Task<IReadOnlyCollection<RuleName>> GetAll()
        {
            _logger.LogInformation("Listing rules in the databaase");
            var rules = await _dbContext.Rules.ToListAsync();
            return rules;
        }

        public async Task<RuleName> Get(int id)
        {
            _logger.LogInformation("Getting rule with id \"{Id}\"", id);

            var rule = await _dbContext.Rules.FirstOrDefaultAsync(b => b.Id == id);
            return rule == null ? throw new EntityNotFoundException() : rule;
        }

        public async Task<IReadOnlyCollection<RuleName>> Delete(int id)
        {
            _logger.LogInformation("Deleting rule with id \"{Id}\"", id);

            var rule = await _dbContext.Rules.FirstOrDefaultAsync(b => b.Id == id);
            if (rule == null) throw new EntityNotFoundException();

            _dbContext.Rules.Remove(rule);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Bid Deleted {@Rule}", rule);

            return await _dbContext.Rules.ToListAsync();
        }
    }
}
