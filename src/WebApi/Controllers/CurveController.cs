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
    public class CurveController : ControllerBase
    {
        private readonly ICurvePointRepository _repository;
        private readonly IMapper _mapper;

        public CurveController(ICurvePointRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// List curve points endpoint
        /// </summary>
        /// <returns></returns>
        [HttpGet("/curvePoint/list")]
        public async Task<IActionResult> HomeAsync()
        {
            var curvePoints = await _repository.GetAll();
            return Ok(curvePoints);
        }

        /// <summary>
        /// Add curvepoint endpoint
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/curvePoint/add")]
        public async Task<IActionResult> AddCurvePoint([FromBody] CurvePointModel model)
        {
            // Map to Doman object
            var curvePoint = _mapper.Map<CurvePoint>(model);
            var curvePoints = await _repository.Add(curvePoint);
            return Ok(curvePoints);
        }

        /// <summary>
        /// Get curvepoint endpoint
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/curvePoint/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var curvePoint = await _repository.Get(id);
            return Ok(curvePoint);
        }

        /// <summary>
        /// Update curvepoint endpoint
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("/curvepoint/update/{id}")]
        public async Task<IActionResult> UpdateCurvePointAsync(int id, [FromBody] CurvePointModel model)
        {
            // Map to Doman object
            var curvePoint = _mapper.Map<CurvePoint>(model);
            var curvePoints = await _repository.Update(id, curvePoint);
            return Ok(curvePoints);
        }

        /// <summary>
        /// Delete curvepoint
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/curvepoint/{id}")]
        public async Task<IActionResult> DeleteBid(int id)
        {
            var curvePoints = await _repository.Delete(id);
            return Ok(curvePoints);
        }
    }
}