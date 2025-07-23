using internshipTechnicalProject.Application.PointerService;
using internshipTechnicalProject.Application.Common;
using internshipTechnicalProject.Domain.Point;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PointController : ControllerBase
    {
        private readonly IPointService _pointService;

        public PointController(IPointService pointService)
        {
            _pointService = pointService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PointDto dto)
        {
            var point = new Point { Name = dto.Name, X = dto.X, Y = dto.Y };
            var result = await _pointService.CreateAsync(point);
            return StatusCode(result.Success ? 200 : 400, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _pointService.GetAllAsync();
            return StatusCode(result.Success ? 200 : 400, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _pointService.GetByIdAsync(id);
            return StatusCode(result.Success ? 200 : 404, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PointDto dto)
        {
            var point = new Point { Id = id, Name = dto.Name, X = dto.X, Y = dto.Y };
            var result = await _pointService.UpdateAsync(point);
            return StatusCode(result.Success ? 200 : 404, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _pointService.DeleteAsync(id);
            return StatusCode(result.Success ? 200 : 404, result);
        }
    }
}
