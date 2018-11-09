using System.Linq;
using System.Threading.Tasks;
using ClientAPI.Core.Shared;
using ClientAPI.Models;
using ClientAPI.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ClientAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthServiceController : ControllerBase
    {
        private readonly IConfiguration _configuration;        
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

         public AuthServiceController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost ("login")]
        public async Task<IActionResult> Login ([FromBody] AuthVM authVM) 
        {     
            if (!ModelState.IsValid)
                return BadRequest (ModelState); //retruns model state error      

            var result = await _signInManager.PasswordSignInAsync(
                userName : authVM.Username, 
                password: authVM.Password, 
                isPersistent: true, 
                lockoutOnFailure : true);
            
            if (result.Succeeded)
            {             
                var user = _userManager.Users.SingleOrDefault(r => r.Email == authVM.Username);

                var role = _userManager.GetRolesAsync(user).Result[0];

                var tokenString = new JWTGenerator().GenerateJWT(user,_configuration, role);
                
                return Ok(new { token = tokenString ,username = authVM.Username });
            }
            
           return Unauthorized();
           
        }

        [Authorize, HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }
    }
}