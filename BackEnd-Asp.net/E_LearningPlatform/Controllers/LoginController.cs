using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.DTO;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace E_LearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;

        public LoginController(UserManager<ApplicationUser> _userManager, IConfiguration _config)
        {
            this.userManager = _userManager;
            config = _config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser userFromDB = await userManager.FindByEmailAsync(userLoginDTO.Email);
                if (userFromDB != null)
                {
                    bool userFounded = await userManager.CheckPasswordAsync(userFromDB, userLoginDTO.Password);

                    if(userFounded == true)
                    {

                        //generate token<==
                        List<Claim> UserClaims = new List<Claim>();
                        //Token Genrated id change (JWT Predefind Claims)
                        UserClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        UserClaims.Add(new Claim("id", userFromDB.Id.ToString()));
                        UserClaims.Add(new Claim("userName", userFromDB.UserName));
                        UserClaims.Add(new Claim("fullName", userFromDB.FirstName + " " + userFromDB.LastName));
                        var UserRoles = await userManager.GetRolesAsync(userFromDB);
                        foreach (var roleName in UserRoles)
                        {
                            UserClaims.Add(new Claim(ClaimTypes.Role, roleName));
                            //UserClaims.Add(new Claim("role", roleName));
                        }

                        var SignInkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SCRKey"]));

                        SigningCredentials signingCred = new SigningCredentials(SignInkey, SecurityAlgorithms.HmacSha256);
                        //design token
                        JwtSecurityToken mytoken = new JwtSecurityToken(
                            audience: config["JWT:AudienceIP"],
                            issuer: config["JWT:IssuerIP"],
                            expires: DateTime.Now.AddHours(1),
                            claims: UserClaims,
                            signingCredentials: signingCred
                        );
                        //generate token response
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                            //UserRoles,
                            //UserId = userFromDB.Id,
                            //FullName = userFromDB.FirstName + " " + userFromDB.LastName,
                        });
                    }
                }
                ModelState.AddModelError("Email", "Email or Password is invalid");

            }
            return BadRequest(ModelState);
        }
    }
}
