using Domain.DTO;
using Microsoft.AspNetCore.Http;
using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Contract;
using Service.Services.Implementation;
using Microsoft.AspNetCore.Authorization;

namespace E_LearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet("GetAll")]
        //[Authorize(Roles = ("Instructor, Admin"))]
        public async Task<IActionResult> GetAll()
        {
            var lessons = await _lessonService.GetAllAsync();
            if (lessons == null || !lessons.Any())
                return Ok("No lessons found.");
            return Ok(lessons);
        }

        [HttpGet("GetById/{id}")]
        //[Authorize(Roles = ("Instructor, Admin"))]
        public async Task<IActionResult> Get(int id)
        {

            var lesson = await _lessonService.GetByIdAsync(id);
            if (lesson == null)
                return NotFound($"Lesson with ID {id} not found.");

            return Ok(lesson);
        }

        [RequestSizeLimit(500_000_000)]
        [HttpPost("Add")]
        //[Authorize(Roles = ("Instructor, Admin"))]
        public async Task<IActionResult> Add([FromForm] LessonCreateDto lessonDto)
        {
            if (lessonDto == null)
                return BadRequest("Lesson data is null.");
            await _lessonService.AddAsync(lessonDto);
            return Ok("Lesson added successfully.");
        }

        [RequestSizeLimit(500_000_000)]
        [HttpPut("Update/{id}")]
        //[Authorize(Roles = ("Instructor, Admin"))]
        public async Task<IActionResult> Update(int id, [FromForm] LessonCreateDto lessonDto)
        {
            try
            {
                if (lessonDto == null || id == null)
                    return BadRequest("Lesson dataot id  is null.");
                await _lessonService.Update(lessonDto, id);
                return Ok("Lesson updated successfully.");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        //[Authorize(Roles = ("Instructor, Admin"))]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                await _lessonService.Delete(id);
                return Ok("Lesson deleted successfully.");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpGet("by-lesson-name/{unitName}")]
        //[Authorize(Roles = ("Instructor, Admin"))]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessonsByUnitName(string unitName)
        {
            try
            {
                var lessons = await _lessonService.GetLessonsByUnitName(unitName);
                return Ok(lessons);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("by-unit-id/{unitId}")]
        //[Authorize(Roles = ("Instructor, Admin"))]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessonsByUnitId(int unitId)
        {
            try
            {
                if (unitId <= 0)
                    return BadRequest("Invalid unit ID.");

                var lessons = await _lessonService.GetLessonsByUnitId(unitId);
                return Ok(lessons);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("download/by-url")]
        //[Authorize(Roles = ("Instructor, Admin"))]
        public IActionResult DownloadByUrl([FromQuery] string fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl))
                return BadRequest("File URL is required.");

            var uri = new Uri(fileUrl);
            var relativePath = uri.LocalPath.TrimStart('/');
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath.Replace('/', Path.DirectorySeparatorChar));

            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found.");

            var mimeType = "application/octet-stream";
            return PhysicalFile(filePath, mimeType, Path.GetFileName(filePath));
        }


    }
}