using Microsoft.AspNetCore.Mvc;
using internshipTechnicalProject.Application.PointerService;
using internshipTechnicalProject.Application.Common;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LineController : ControllerBase
    {
        private readonly ILineService _lineService;

        public LineController(ILineService lineService)
        {
            _lineService = lineService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LineDto dto)
        {
            var result = await _lineService.CreateAsync(dto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _lineService.GetAllAsync();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _lineService.DeleteAsync(id);
            return StatusCode(result.Success ? 200 : 404, result);
        }
    }
} 