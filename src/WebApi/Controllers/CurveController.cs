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

        // TODO: Inject Curve Point service

        [HttpGet("/curvePoint/list")]
        public async Task<IActionResult> HomeAsync()
        {
            var curvePoints = await _repository.GetAll();
            return Ok(curvePoints);
        }

        [HttpPost("/curvePoint/add")]
        public async Task<IActionResult> AddCurvePoint([FromBody] CurvePointModel model)
        {
            var curvePoint = _mapper.Map<CurvePoint>(model);
            var curvePoints = await _repository.Add(curvePoint);
            return Ok(curvePoints);
        }

        [HttpGet("/curvePoint/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var curvePoint = await _repository.Get(id);
            return Ok(curvePoint);
        }

        [HttpPost("/curvepoint/update/{id}")]
        public async Task<IActionResult> UpdateCurvePointAsync(int id, [FromBody] CurvePointModel model)
        {
            var curvePoint = _mapper.Map<CurvePoint>(model);
            var curvePoints = await _repository.Update(id, curvePoint);
            return Ok(curvePoints);
        }

        [HttpDelete("/curvepoint/{id}")]
        public async Task<IActionResult> DeleteBid(int id)
        {
            var curvePoints = await _repository.Delete(id);
            return Ok(curvePoints);
        }
    }
}