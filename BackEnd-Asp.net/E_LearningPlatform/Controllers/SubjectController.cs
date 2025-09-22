using Domain.DTO;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Service.Services.Contract;

namespace E_LearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService newSubject;
        private readonly AppDbContext context;
        public SubjectController(ISubjectService _subject, AppDbContext _context)
        {
            newSubject = _subject;
            context = _context;
        }

        [HttpGet]
        //[Authorize(Roles = ("Instructor, Admin"))]
        public IActionResult GetAllSubjects()
        {
            var response = newSubject.GetAllSubjects();
            return Ok(response);
        }


        [HttpGet("GetSubjectByClassIdAndTrackId/{classId:int}/{trackId:int}")]
        [AllowAnonymous]
        public IActionResult GetSubjectByClassIdAndTrackId(int classId, int trackId)
        {
            //if(classId == 0 || trackId == 0)
            //{
            //    var response = newSubject.GetAllSubjects();
            //    return Ok(response);
            //}
            //else
            //{
                var results = context.StudentClassSubjects.Where(c => c.ClassID == classId && c.TrackID == trackId)
                .Select(cts => new
                {
                    SubjectId = cts.SubjectID,
                    InstructorName = cts.Subject.Instructor.User.FirstName + " " + cts.Subject.Instructor.User.LastName, 
                    SubjectName = cts.Subject.SubjectName,
                    SubjectPrice = cts.Subject.Price,
                    SubjectDescription = cts.Subject.SubjectDescription,
                    imgUrl = cts.Subject.Instructor.Image,
                }).ToList();

                return Ok(results);
            //}
        }

        [HttpGet("GetHomeSubjectById/{Id:int}")]
        //[AllowAnonymous]
        public IActionResult GetHomeSubjectById(int id)
        {
            //if(classId == 0 || trackId == 0)
            //{
            //    var response = newSubject.GetAllSubjects();
            //    return Ok(response);
            //}
            //else
            //{
            var result = context.StudentClassSubjects.Where(c => c.SubjectID == id)
            .Select(cts => new
            {
                SubjectId = cts.SubjectID,
                InstructorName = cts.Subject.Instructor.User.FirstName + " " + cts.Subject.Instructor.User.LastName,
                SubjectName = cts.Subject.SubjectName,
                SubjectPrice = cts.Subject.Price,
                SubjectDescription = cts.Subject.SubjectDescription,
                imgUrl = cts.Subject.Instructor.Image,
            }).SingleOrDefault();

            return Ok(result);
            //}
        }

        [HttpGet("GetHomeSubjects")]
        //[AllowAnonymous]
        public IActionResult GetHomeSubjects()
        {
            var results = context.StudentClassSubjects
               .Select(cts => new
               {
                   SubjectId = cts.SubjectID,
                   InstructorName = cts.Subject.Instructor.User.FirstName + " " + cts.Subject.Instructor.User.LastName,
                   SubjectName = cts.Subject.SubjectName,
                   SubjectPrice = cts.Subject.Price,
                   SubjectDescription = cts.Subject.SubjectDescription,
                   imgUrl = cts.Subject.Instructor.Image,
               }).ToList();

            return Ok(results);
        }

        [HttpGet("id/{id:int}")]
        public IActionResult GetSubjectById(int id)
        {
            if (id != null)
            {
                var response = newSubject.GetSubjectById(id);
                return Ok(response);
            }
            return NotFound();
        }

        [HttpGet("name/{name:alpha}")]
        public IActionResult GetSubjectByName(string name)
        {
            if (name != null)
            {
                var response = newSubject.GetSubjectByName(name);
                return Ok(response);
            }
            return NotFound();
        }

        [HttpPost]
        //[Authorize(Roles = ("Instructor, Admin"))]
        public IActionResult PostSubject([FromBody] CreatedSubjectDTO subjectDTO)
        {
            if (subjectDTO != null)
            {
                var response = newSubject.AddSubject(subjectDTO);
                return Ok(response);
            }
            return NotFound();
        }

        [HttpPut("{id:int}")]
        //[Authorize(Roles = ("Instructor, Admin"))]
        public IActionResult UpdateSubject(int id, [FromBody] CreatedSubjectDTO upSubjectDTO)
        {
            if (upSubjectDTO != null && id != null)
            {
                var response = newSubject.UpdateSubjectById(id, upSubjectDTO);
                return Ok(response);
            }
            return NotFound();
        }

        [HttpDelete("{id:int}")]
        //[Authorize(Roles = ("Instructor, Admin"))]
        public IActionResult DeleteSubjectById(int id)
        {
            if (id != null)
            {
                var response = newSubject.RemoveSubjectById(id);
                return Ok(response);
            }
            return NotFound();
        }
    }
}