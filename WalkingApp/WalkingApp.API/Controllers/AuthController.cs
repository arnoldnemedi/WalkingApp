using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WalkingApp.API.Models.DTO;
using WalkingApp.API.Repositories;

namespace WalkingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerDto)
        {
            var identityUser = new IdentityUser 
            { 
                UserName = registerDto.Username,
                Email = registerDto.Username
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerDto.Password);

            if(identityResult.Succeeded)
            {
                if (registerDto.Roles != null && registerDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerDto.Roles);

                    if(identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }
                }
            }

            return BadRequest("Something went wrong!");
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LogionRequestDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Username);

            if(user != null)
            {
                if(await userManager.CheckPasswordAsync(user, loginDto.Password))
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if(roles != null)
                    {
                        var token = tokenRepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDto
                        { 
                            JwtToken = token 
                        };
                        return Ok(response);
                    }
                   
                    return Ok();
                }
            }

            return BadRequest("Incoerrect username or password!");
        }
    }
}
