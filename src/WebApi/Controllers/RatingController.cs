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
    public class RatingController : ControllerBase
    {
        private readonly IRatingRepository _repository;
        private readonly IMapper _mapper;

        public RatingController(IRatingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("/rating/list")]
        public async Task<IActionResult> Home()
        {
            var ratings = await _repository.GetAll();
            return Ok(ratings);
        }

        [HttpPost("/rating/add")]
        public async Task<IActionResult> AddRating([FromBody] RatingModel model)
        {
            var rating = _mapper.Map<Rating>(model);
            var ratings = await _repository.Add(rating);
            return Ok(rating);
        }

        [HttpGet("/rating/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var rating = await _repository.Get(id);
            return Ok(rating);
        }

        [HttpPost("/rating/update/{id}")]
        public async Task<IActionResult> updateRatingAsync(int id, [FromBody] RatingModel model)
        {
            var rating = _mapper.Map<Rating>(model);
            var ratings = await _repository.Update(id, rating);
            return Ok(ratings);
        }

        [HttpDelete("/rating/{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            var ratings = await _repository.Delete(id);
            return Ok(ratings);
        }
    }
}