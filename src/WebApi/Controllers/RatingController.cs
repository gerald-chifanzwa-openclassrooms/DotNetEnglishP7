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
    public class RatingController : ControllerBase
    {
        private readonly IRatingRepository _repository;
        private readonly IMapper _mapper;

        public RatingController(IRatingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// List ratings endpoint
        /// </summary>
        /// <returns></returns>
        [HttpGet("/rating/list")]
        public async Task<IActionResult> Home()
        {
            var ratings = await _repository.GetAll();
            return Ok(ratings);
        }

        /// <summary>
        /// Add rating endpoint
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/rating/add")]
        public async Task<IActionResult> AddRating([FromBody] RatingModel model)
        {
            // Map to Doman object
            var rating = _mapper.Map<Rating>(model);
            var ratings = await _repository.Add(rating);
            return Ok(rating);
        }

        /// <summary>
        /// Get rating endpoint
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/rating/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var rating = await _repository.Get(id);
            return Ok(rating);
        }

        /// <summary>
        /// Update rating endpoint
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("/rating/update/{id}")]
        public async Task<IActionResult> UpdateRatingAsync(int id, [FromBody] RatingModel model)
        {
            // Map to Doman object
            var rating = _mapper.Map<Rating>(model);
            var ratings = await _repository.Update(id, rating);
            return Ok(ratings);
        }

        /// <summary>
        /// Delete endpoint
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/rating/{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            var ratings = await _repository.Delete(id);
            return Ok(ratings);
        }
    }
}