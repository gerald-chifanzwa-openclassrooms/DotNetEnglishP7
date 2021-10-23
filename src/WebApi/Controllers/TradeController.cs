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
    public class TradeController : ControllerBase
    {
        private readonly ITradeRepository _repository;
        private readonly IMapper _mapper;

        public TradeController(ITradeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// List trades endpoint
        /// </summary>
        /// <returns></returns>
        [HttpGet("/trade/list")]
        public async Task<IActionResult> GetAll()
        {
            var trades = await _repository.GetAll();
            return Ok(trades);
        }

        /// <summary>
        /// Add trade endpoint
        /// </summary>
        /// <param name="tradeModel"></param>
        /// <returns></returns>
        [HttpGet("/trade/add")]
        public async Task<IActionResult> AddTrade([FromBody] TradeModel tradeModel)
        {
            // Map to Doman object
            var trade = _mapper.Map<Trade>(tradeModel);
            var trades = await _repository.Add(trade);
            return Ok(trades);
        }

        /// <summary>
        /// Get single trade endpoint
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/trade/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var trade = await _repository.Get(id);
            return Ok(trade);
        }

        /// <summary>
        /// Update trade endpoint
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tradeModel"></param>
        /// <returns></returns>
        [HttpPut("/trade/update/{id}")]
        public async Task<IActionResult> UpdateTradeAsync(int id, [FromBody] TradeModel tradeModel)
        {
            // Map to Doman object
            var trade = _mapper.Map<Trade>(tradeModel);
            var trades = await _repository.Update(id, trade);
            return Ok(trades);
        }

        /// <summary>
        /// Delete trade endpoint
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/trade/{id}")]
        public async Task<IActionResult> DeleteTradeAsync(int id)
        {
            var trades = await _repository.Delete(id);
            return Ok(trades);
        }
    }
}