using Domain.DTO;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Services.Contract;
namespace E_LearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly AppDbContext context;

        public StudentController(UserManager<ApplicationUser> _userManager, AppDbContext _context)
        {
            this.userManager = _userManager;
            this.context = _context;
        }

        [HttpPost("studentRegister")]
        public async Task<IActionResult> Register([FromBody] StudentRegisterDTO studentRegisterDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();

                user.Email = studentRegisterDTO.Email;
                user.PhoneNumber = studentRegisterDTO.PhoneNumber;
                user.Address = studentRegisterDTO.Address;
                user.FirstName = studentRegisterDTO.FirstName;
                user.LastName = studentRegisterDTO.LastName;
                user.Gender = studentRegisterDTO.Gender;
                user.UserName = studentRegisterDTO.FirstName + studentRegisterDTO.LastName;

                //var r = userManager.GetRolesAsync(user)
                //    userManager.UpdateAsync(user);

                IdentityResult result = await userManager.CreateAsync(user, studentRegisterDTO.Password);



                if (result.Succeeded)
                {
                    StudentProfile student = new StudentProfile();


                    student.UserId = user.Id;
                    student.Age = studentRegisterDTO.Age;

                    context.Add(student);
                    context.SaveChanges();

                    await userManager.AddToRoleAsync(user, "Student");


                    return Ok("Created");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("Password", item.Description);
                }

            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        public IActionResult GetStudents()
        {

            List<StudentRegisterDTO> studentRegisterDTOs = new List<StudentRegisterDTO>();

            var users = userManager.Users.Include(u => u.StudentProfile).Where(u => u.StudentProfile != null).ToList();

            foreach (var user in users)
            {
                StudentRegisterDTO studentRegisterDTO = new StudentRegisterDTO();



                studentRegisterDTO.Id = user.Id;
                studentRegisterDTO.FirstName = user.FirstName;
                studentRegisterDTO.LastName = user.LastName;
                studentRegisterDTO.Address = user.Address;
                studentRegisterDTO.PhoneNumber = user.PhoneNumber;
                studentRegisterDTO.Email = user.Email;
                studentRegisterDTO.Gender = user.Gender;
                studentRegisterDTO.Age = user.StudentProfile.Age;

                studentRegisterDTOs.Add(studentRegisterDTO);
            }

            return Ok(studentRegisterDTOs);
        }

        [HttpGet("id/{id:int}")]
        public async Task<IActionResult> GetStudentById(int id)
        {

            var user = await userManager.FindByIdAsync(id.ToString());

            if (user != null)
            {

                UserDTO userDTO = new UserDTO()
                {
                    FullName = user.FirstName + " " + user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    
                };



                return Ok(userDTO);
            }

            return NotFound("Student Not Found");
        }

        [HttpGet("fullname/{name:alpha}")]
        public async Task<IActionResult> GetStudentByFullName(string name)
        {

            var user = await userManager.FindByNameAsync(name);

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound("Student Not Found");
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentRegisterDTO studentRegisterDTO)
        {

            var user = await userManager.Users.Include(u => u.StudentProfile)
                .SingleOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                //await userManager.UpdateAsync(studentRegisterDTO);
                return NotFound("Student Not Found");
            }

            user.Email = studentRegisterDTO.Email;
            user.PhoneNumber = studentRegisterDTO.PhoneNumber;
            user.Address = studentRegisterDTO.Address;
            user.FirstName = studentRegisterDTO.FirstName;
            user.LastName = studentRegisterDTO.LastName;
            user.Gender = studentRegisterDTO.Gender;
            user.UserName = studentRegisterDTO.FirstName + studentRegisterDTO.LastName;

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            if (user.StudentProfile != null)
            {
                user.StudentProfile.Age = studentRegisterDTO.Age;
            }

            await context.SaveChangesAsync();

            return Ok("Student Updated Successfully");


        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var user = await userManager.Users.Include(u => u.StudentProfile)
                                  .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound("Student Not Found");

            if (user.StudentProfile != null)
                context.StudentProfiles.Remove(user.StudentProfile);

            var result = await userManager.DeleteAsync(user);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await context.SaveChangesAsync();

            return Ok("Student is deleted Successfully");
        }




        //private readonly IStudentService _service;

        //public StudentController(IStudentService service)
        //{
        //    _service = service;
        //}

        //[HttpGet("GetAll")]
        //public async Task<ActionResult<IEnumerable<Student>>> GetAll()
        //{
        //    return Ok(await _service.GetAllAsync());
        //}

        //[HttpGet("GetById/{id}")]
        //public async Task<ActionResult<Student>> GetById(int id)
        //{
        //    var student = await _service.GetByIdAsync(id);
        //    if (student == null)
        //        return NotFound();

        //    return Ok(student);
        //}

        //[HttpPost("Create")]
        //public async Task<ActionResult> Create([FromBody] Student student)
        //{
        //    await _service.AddAsync(student);
        //    return Ok();
        //}

        //[HttpPut("Update/{id}")]
        //public async Task<ActionResult> Update(int id, [FromBody] Student student)
        //{
        //    if (id != student.StudentID)
        //        return BadRequest();

        //    await _service.UpdateAsync(student);
        //    return NoContent();
        //}

        //[HttpDelete("Delete/{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    await _service.DeleteAsync(id);
        //    return NoContent();
        //}

        //[HttpPost("enroll")]
        //public async Task<IActionResult> EnrollStudent([FromForm] int studentId, [FromForm] int subjectId)
        //{
        //    try
        //    {
        //        await _service.EnrollStudentInSubjectAsync(studentId, subjectId);
        //        return Ok("Student enrolled successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error: {ex.Message}");
        //    }
        //}
        //[HttpGet("payment")]
        //public async Task<IActionResult> GetPaymentBySubjectAndStudent(int subjectId, int studentId)
        //{
        //    try
        //    {
        //        var payment = await _service.GetPaymentBySubjectAndStudent(subjectId, studentId);
        //        if (payment == null)
        //            return NotFound("Payment not found.");
        //        return Ok(payment);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error: {ex.Message}");
        //    }
        //}
    }
}
