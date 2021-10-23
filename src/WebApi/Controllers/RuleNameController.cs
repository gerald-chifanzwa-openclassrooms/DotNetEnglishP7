using System.Threading.Tasks;
using AutoMapper;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;

namespace Dot.Net.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController, Authorize]
    public class RuleNameController : ControllerBase
    {
        private readonly IRuleRepository _repository;
        private readonly IMapper _mapper;

        public RuleNameController(IRuleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// List rules endpoint
        /// </summary>
        /// <returns></returns>
        [HttpGet("/ruleName/list")]
        public async Task<IActionResult> GetAllAsync()
        {
            var rules = await _repository.GetAll();
            return Ok(rules);
        }

        /// <summary>
        /// Add rule endpoint
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/ruleName/add")]
        public async Task<IActionResult> AddRuleNameAsync([FromBody] RuleNameModel model)
        {
            // Map to Doman object
            var rule = _mapper.Map<RuleName>(model);
            var rules = await _repository.Add(rule);
            return Ok(rules);
        }

        /// <summary>
        /// Get rule endpoint
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/ruleName/{id}")]
        public async Task<IActionResult> GetRuleAsync(int id)
        {
            var rule = await _repository.Get(id);
            return Ok(rule);
        }

        /// <summary>
        /// Update rule endpoint
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("/ruleName/update/{id}")]
        public async Task<IActionResult> UpdateRuleNameAsync(int id, [FromBody] RuleNameModel model)
        {
            // Map to Doman object
            var rule = _mapper.Map<RuleName>(model);
            var rules = await _repository.Update(id, rule);
            return Ok(rules);
        }

        /// <summary>
        /// Delete rule endpoint
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/ruleName/{id}")]
        public async Task<IActionResult> DeleteRuleNameAsync(int id)
        {
            var rules = await _repository.Delete(id);
            return Ok(rules);
        }
    }
}