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
    public class TradeController : ControllerBase
    {
        private readonly ITradeRepository _repository;
        private readonly IMapper _mapper;

        public TradeController(ITradeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("/trade/list")]
        public async Task<IActionResult> GetAll()
        {
            var trades = await _repository.GetAll();
            return Ok(trades);
        }

        [HttpGet("/trade/add")]
        public async Task<IActionResult> AddTrade([FromBody] TradeModel tradeModel)
        {
            var trade = _mapper.Map<Trade>(tradeModel);
            var trades = await _repository.Add(trade);
            return Ok(trades);
        }

        [HttpGet("/trade/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var trade = await _repository.Get(id);
            return Ok(trade);
        }

        [HttpPost("/trade/update/{id}")]
        public async Task<IActionResult> updateTradeAsync(int id, [FromBody] TradeModel tradeModel)
        {
            var trade = _mapper.Map<Trade>(tradeModel);
            var trades = await _repository.Update(id, trade);
            return Ok(trades);
        }

        [HttpDelete("/trade/{id}")]
        public async Task<IActionResult> DeleteTradeAsync(int id)
        {
            var trades = await _repository.Delete(id);
            return Ok(trades);
        }
    }
}