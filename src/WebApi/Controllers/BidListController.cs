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
    [ApiController]
    public class BidListController : Controller
    {
        private readonly IBidRepository _bidRepository;
        private readonly IMapper _mapper;

        public BidListController(IBidRepository bidRepository, IMapper mapper)
        {
            _bidRepository = bidRepository;
            _mapper = mapper;
        }

        [HttpGet("/")]
        public async Task<IActionResult> HomeAsync()
        {
            var bids = await _bidRepository.GetAll();
            return Ok(bids);
        }


        [HttpGet("/bidList/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var bid = await _bidRepository.Get(id);
            return Ok(bid);
        }

        [HttpPost("/bidList/validate")]
        [Authorize]
        public async Task<IActionResult> Validate([FromBody] BidListModel model)
        {
            var bidList = _mapper.Map<BidList>(model);
            var bids = await _bidRepository.Add(bidList);
            return Ok(bids);
        }


        [HttpPost("/bidList/update/{id}")]
        public async Task<IActionResult> UpdateBidAsync(int id, [FromBody] BidListModel model)
        {
            var bidList = _mapper.Map<BidList>(model);
            var bids = await _bidRepository.Update(id, bidList);
            return Ok(bids);
        }

        [HttpDelete("/bidList/{id}")]
        public async Task<IActionResult> DeleteBidAsync(int id)
        {
            var bids = await _bidRepository.Delete(id);
            return Ok(bids);
        }
    }
}