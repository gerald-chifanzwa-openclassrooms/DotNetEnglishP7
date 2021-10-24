using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public interface IRuleRepository
    {
        /// <summary>
        /// Adds a RuleName to the database
        /// </summary>
        /// <returns>A collection of the rules in the databse</returns>
        Task<IReadOnlyCollection<RuleName>> Add(RuleName ruleName);
        /// <summary>
        /// Updates a ruleName in the database
        /// </summary>
        /// <returns>The updated collection of rules in the database</returns>
        Task<IReadOnlyCollection<RuleName>> Update(int id, RuleName ruleName);
        /// <summary>
        /// Gets all the ruleNames in the database
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyCollection<RuleName>> GetAll();
        /// <summary>
        /// Gets a single ruleName with a given <paramref name="id"/>
        /// </summary>
        /// <returns>A single rulename from the database or null if not found.</returns>
        Task<RuleName> Get(int id);
        /// <summary>
        /// Deletes a ruleName from the database
        /// </summary>
        /// <returns>The updated collection of ruleNames in the database</returns>
        Task<IReadOnlyCollection<RuleName>> Delete(int id);
    }
}
