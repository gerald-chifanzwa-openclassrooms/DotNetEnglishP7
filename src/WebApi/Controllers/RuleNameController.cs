using System.Threading.Tasks;
using AutoMapper;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;

namespace Dot.Net.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RuleNameController : ControllerBase
    {
        private readonly IRuleRepository _repository;
        private readonly IMapper _mapper;

        public RuleNameController(IRuleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // TODO: Inject RuleName service

        [HttpGet("/ruleName/list")]
        public async Task<IActionResult> GetAllAsync()
        {
            var rules = await _repository.GetAll();
            return Ok(rules);
        }

        [HttpPost("/ruleName/add")]
        public async Task<IActionResult> AddRuleNameAsync([FromBody] RuleNameModel model)
        {
            var rule = _mapper.Map<RuleName>(model);
            var rules = await _repository.Add(rule);
            return Ok(rules);
        }


        [HttpGet("/ruleName/{id}")]
        public async Task<IActionResult> GetRuleAsync(int id)
        {
            var rule = await _repository.Get(id);
            return Ok(rule);
        }

        [HttpPost("/ruleName/update/{id}")]
        public async Task<IActionResult> UpdateRuleNameAsync(int id, [FromBody] RuleNameModel model)
        {
            var rule = _mapper.Map<RuleName>(model);
            var rules = await _repository.Update(id, rule);
            return Ok(rules);
        }

        [HttpDelete("/ruleName/{id}")]
        public async Task<IActionResult> DeleteRuleNameAsync(int id)
        {
            var rules = await _repository.Delete(id);
            return Ok(rules);
        }
    }
}