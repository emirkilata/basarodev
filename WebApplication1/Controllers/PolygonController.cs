using Microsoft.AspNetCore.Mvc;
using internshipTechnicalProject.Application.PointerService;
using internshipTechnicalProject.Application.Common;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PolygonController : ControllerBase
    {
        private readonly IPolygonService _polygonService;

        public PolygonController(IPolygonService polygonService)
        {
            _polygonService = polygonService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PolygonDto dto)
        {
            var result = await _polygonService.CreateAsync(dto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _polygonService.GetAllAsync();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _polygonService.DeleteAsync(id);
            return StatusCode(result.Success ? 200 : 404, result);
        }
    }
} 