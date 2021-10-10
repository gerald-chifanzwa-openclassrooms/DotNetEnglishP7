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
        public IActionResult Home()
        {
            return View("Home");
        }

        [HttpPost("/bidList/validate")]
        [Authorize]
        public async Task<IActionResult> Validate([FromBody]CreateBidModel model)
        {
            var bidList = _mapper.Map<BidList>(model);
            var bids = await _bidRepository.Add(bidList);
            return Ok(bids);
        }

        [HttpGet("/bidList/update/{id}")]
        public IActionResult ShowUpdateForm(int id)
        {
            return View("bidList/update");
        }

        [HttpPost("/bidList/update/{id}")]
        public IActionResult UpdateBid(int id, [FromBody] BidList bidList)
        {
            // TODO: check required fields, if valid call service to update Bid and return list Bid
            return Redirect("/bidList/list");
        }

        [HttpDelete("/bidList/{id}")]
        public IActionResult DeleteBid(int id)
        {
            return Redirect("/bidList/list");
        }
    }
}