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
    public class BidListController : Controller
    {
        private readonly IBidRepository _bidRepository;
        private readonly IMapper _mapper;

        public BidListController(IBidRepository bidRepository, IMapper mapper)
        {
            _bidRepository = bidRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// List bidlists endpoint
        /// </summary>
        /// <returns></returns>
        [HttpGet("/")]
        public async Task<IActionResult> HomeAsync()
        {
            var bids = await _bidRepository.GetAll();
            return Ok(bids);
        }

        /// <summary>
        /// Get bidlist endpoint
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("/bidList/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var bid = await _bidRepository.Get(id);
            return Ok(bid);
        }

        /// <summary>
        /// Add Bidlist endpoint
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/bidList/add")]
        public async Task<IActionResult> Add([FromBody] BidListModel model)
        {
            // Map to Doman object
            var bidList = _mapper.Map<BidList>(model);
            var bids = await _bidRepository.Add(bidList);
            return Ok(bids);
        }

        /// <summary>
        /// Update Bidlist endpoint
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("/bidList/update/{id}")]
        public async Task<IActionResult> UpdateBidAsync(int id, [FromBody] BidListModel model)
        {
            // Map to Doman object
            var bidList = _mapper.Map<BidList>(model);
            var bids = await _bidRepository.Update(id, bidList);
            return Ok(bids);
        }

        /// <summary>
        /// Delete Bidlist endpoint
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/bidList/{id}")]
        public async Task<IActionResult> DeleteBidAsync(int id)
        {
            var bids = await _bidRepository.Delete(id);
            return Ok(bids);
        }
    }
}