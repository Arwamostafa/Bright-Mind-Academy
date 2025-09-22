using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Contract;
using Service.Services.Implementation;

namespace E_LearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ClassController : ControllerBase
    {
        private readonly IClassService newClass;
        public ClassController(IClassService  _class)
        {
            newClass = _class;
        }

        [HttpGet]
        //[Authorize(Roles = ("Instructor, Admin"))]
        public IActionResult GetAllClasses()
        {
            var response = newClass.GetAllClasses();
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        //[Authorize(Roles = ("Instructor, Admin"))]
        public IActionResult GetClassById(int id)
        {
            if(id != null)
            {
                var response = newClass.GetClassById(id);
                return Ok(response);
            }
            return NotFound();  
        }

        [HttpGet("{name:alpha}")]
        //[Authorize(Roles = ("Instructor, Admin"))]
        public IActionResult GetClassByName(string name)
        {
            if (name != null)
            {
                var response = newClass.GetClassByName(name);
                return Ok(response);
            }
            return NotFound();
        }

        [HttpPost]
        //[Authorize(Roles = ("Admin"))]
        public IActionResult PostClass([FromBody] Class addedClass)
        {
            if(addedClass != null)
            {
                var response = newClass.AddClass(addedClass);
                return Ok(response);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = ("Admin"))]
        public IActionResult UpdateClass(int id,[FromBody] Class upClass)
        {
            if (upClass != null  && id != null)
            {
                var response = newClass.UpdateClassById(id, upClass);
                return Ok(response);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = ("Admin"))]
        public IActionResult DeleteClassById(int id)
        {
            if (id != null)
            {
                var response = newClass.RemoveClassById(id);
                return Ok(response);
            }
            return NotFound();
        }

    }
}
