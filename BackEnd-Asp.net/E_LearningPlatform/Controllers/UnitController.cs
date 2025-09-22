using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Contract;

namespace E_LearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;

        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<UnitDto>>> GetAllUnits()
        {
            var units = await _unitService.GetAllAsync();
            return Ok(units);
        }

        // POST: api/Unit
        [HttpPost("Create")]
        public async Task<ActionResult<UnitDto>> CreateUnit([FromBody] UnitCreateDto unitCreateDto)
        {
            await _unitService.AddAsync(unitCreateDto);


            return CreatedAtAction(nameof(GetUnitById), new { id = unitCreateDto.Id }, unitCreateDto);
        }


        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<UnitDto>> GetUnitById(int id)
        {
            var unit = await _unitService.GetByIdAsync(id);
            if (unit == null)
                return NotFound($"No unit found with ID = {id}");

            return Ok(unit);
        }



        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateUnit(int id, [FromBody] UnitCreateDto unitCreateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _unitService.Update(unitCreateDto, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            try
            {
                await _unitService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("by-lesson/{lessonId}")]
        public async Task<ActionResult<UnitDto>> GetUnitByLessonId(int lessonId)
        {
            try
            {
                var unit = await _unitService.GetUnitByLessonId(lessonId);
                return Ok(unit);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("BySubjectId/{subjectId}")]
        public async Task<IActionResult> GetUnitsBySubjectId(int subjectId)
        {
            var units = await _unitService.GetUnitsBySubjectId(subjectId);

            if (units == null || !units.Any())
                return NotFound("No units found for this subject.");

            return Ok(units);
        }

        [HttpGet("BySubjectName/{subjectName}")]
        public async Task<IActionResult> GetUnitsBySubjectName(string subjectName)
        {
            var units = await _unitService.GetUnitsBySubjectName(subjectName);

            if (units == null || !units.Any())
                return NotFound("No units found for this subject name.");

            return Ok(units);
        }


    }
}