using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Contract;

namespace E_LearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TrackController : ControllerBase
    {
        private readonly ITrackService newTrack;
        public TrackController(ITrackService _track)
        {
            newTrack = _track;
        }

        [HttpGet]
        //[Authorize(Roles = ("Instructor, Admin"))]
        public IActionResult GetAllTracks()
        {
            var response = newTrack.GetAllTracks();
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        //[Authorize(Roles = ("Instructor, Admin"))]
        public IActionResult GetTrackById(int id)
        {
            if (id != null)
            {
                var response = newTrack.GetTrackById(id);
                return Ok(response);
            }
            return NotFound();
        }

        [HttpGet("{name:alpha}")]
        //[Authorize(Roles = ("Instructor, Admin"))]
        public IActionResult GetTrackByName(string name)
        {
            if (name != null)
            {
                var response = newTrack.GetTrackByName(name);
                return Ok(response);
            }
            return NotFound();
        }

        [HttpPost]
        //[Authorize(Roles = ("Admin"))]
        public IActionResult PostTrack([FromBody] Track addedTrack)
        {
            if (addedTrack != null)
            {
                var response = newTrack.AddTrack(addedTrack);
                return Ok(response);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = ("Admin"))]
        public IActionResult UpdateTrack(int id, [FromBody] Track upTrack)
        {
            if (upTrack != null && id != null)
            {
                var response = newTrack.UpdateTrackById(id, upTrack);
                return Ok(response);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = ("Admin"))]
        public IActionResult DeleteTrackById(int id)
        {
            if (id != null)
            {
                var response = newTrack.RemoveTrackById(id);
                return Ok(response);
            }
            return NotFound();
        }
    }
}
