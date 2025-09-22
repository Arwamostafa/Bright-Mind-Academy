using Domain.DTO;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Services.Contract;
using Service.Services.Implementation;

namespace E_LearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly AppDbContext context;
        private readonly ILessonService lessonService;

        public InstructorController(UserManager<ApplicationUser> _userManager, AppDbContext _context, ILessonService _lessonService)
        {
            this.userManager = _userManager;
            this.context = _context;
            lessonService = _lessonService;
        }

        [HttpPost("addingInstructor")]
        public async Task<IActionResult> AddInstructor([FromForm] InstructorAddingDTO instructorAddingDTO)
        {
            if (string.IsNullOrWhiteSpace(instructorAddingDTO.Password))
            {
                ModelState.AddModelError("Password", "Password is required");
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.Email = instructorAddingDTO.Email;
                user.PhoneNumber = instructorAddingDTO.PhoneNumber;
                user.Address = instructorAddingDTO.Address;
                user.FirstName = instructorAddingDTO.FirstName;
                user.LastName = instructorAddingDTO.LastName;
                user.Gender = instructorAddingDTO.Gender;
                user.UserName = instructorAddingDTO.FirstName + instructorAddingDTO.LastName;


                IdentityResult result = await userManager.CreateAsync(user, instructorAddingDTO.Password);



                if (result.Succeeded)
                {
                    InstructorProfile instructor = new InstructorProfile();


                    instructor.UserId = user.Id;
                    List<string> Image = new List<string>();
                    if (instructorAddingDTO.Image != null)
                    {
                        var files = await lessonService.SaveFileAsync(instructorAddingDTO.Image, "Uploads/Image");
                        Image.AddRange(files.Where(url => url.EndsWith(".png") || url.EndsWith(".jpg") || url.EndsWith(".jpeg")));
                    }
                    instructor.Image = Image.FirstOrDefault();

                    context.Add(instructor);
                    context.SaveChanges();

                    await userManager.AddToRoleAsync(user, "Instructor");


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
        public IActionResult GetAllInstructors()
        {

            List<InstructorAddingDTO> instructorAddingDTOs = new List<InstructorAddingDTO>();

            var users = userManager.Users.Include(u => u.InstructorProfile).Where(u => u.InstructorProfile != null).ToList();

            foreach (var user in users)
            {
                InstructorAddingDTO instructorAddingDTO = new InstructorAddingDTO();

                instructorAddingDTO.Id = user.Id;
                instructorAddingDTO.FirstName = user.FirstName;
                instructorAddingDTO.LastName = user.LastName;
                instructorAddingDTO.Address = user.Address;
                instructorAddingDTO.PhoneNumber = user.PhoneNumber;
                instructorAddingDTO.Email = user.Email;
                instructorAddingDTO.Gender = user.Gender;
                instructorAddingDTO.ImageURL = user.InstructorProfile.Image;

                instructorAddingDTOs.Add(instructorAddingDTO);
            }

            return Ok(instructorAddingDTOs);
        }

        [HttpGet("id/{id:int}")]
        public async Task<IActionResult> GetInstructorById(int id)
        {

            var user = await userManager.FindByIdAsync(id.ToString());

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound("Instructor Not Found");
        }

        [HttpGet("fullname/{name:alpha}")]
        public async Task<IActionResult> GetInstructorByFullName(string name)
        {

            var user = await userManager.FindByNameAsync(name);

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound("Instructor Not Found");
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateInstructor(int id, [FromForm] InstructorAddingDTO instructorAddingDTO)
        {

            var user = await userManager.Users.Include(u => u.InstructorProfile)
                .SingleOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                //await userManager.UpdateAsync(studentRegisterDTO);
                return NotFound("Instructor Not Found");
            }

            user.Email = instructorAddingDTO.Email;
            user.PhoneNumber = instructorAddingDTO.PhoneNumber;
            user.Address = instructorAddingDTO.Address;
            user.FirstName = instructorAddingDTO.FirstName;
            user.LastName = instructorAddingDTO.LastName;
            user.Gender = instructorAddingDTO.Gender;
            user.UserName = instructorAddingDTO.FirstName + instructorAddingDTO.LastName;

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            if (user.InstructorProfile != null)
            {
                if (instructorAddingDTO.Image != null)
                {
                    List<string> Image = new List<string>();
                    var files = await lessonService.SaveFileAsync(instructorAddingDTO.Image, "Uploads/Images");
                    Image.AddRange(files.Where(url => url.EndsWith(".png") || url.EndsWith(".jpg") || url.EndsWith(".jpeg")));

                    var newImage = Image.FirstOrDefault();
                    if (!string.IsNullOrEmpty(newImage))
                    {
                        user.InstructorProfile.Image = newImage;
                    }
                }
            }

            await context.SaveChangesAsync();

            return Ok("Instructor Updated Successfully");


        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteInstructor(int id)
        {
            var user = await userManager.Users.Include(u => u.InstructorProfile)
                                  .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound("Instructor Not Found");

            if (user.InstructorProfile != null)
                context.InstructorProfiles.Remove(user.InstructorProfile);

            var result = await userManager.DeleteAsync(user);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await context.SaveChangesAsync();

            return Ok("Instructor is deleted Successfully");
        }
    }
}