using System;
using System.Threading.Tasks;
using AutoMapper;
using ClientAPI.Core.Shared;
using ClientAPI.DataAccess.Contracts;
using ClientAPI.Models;
using ClientAPI.Shared.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ClientAPI.Controllers
{
    [Route("api/[controller]")]
    // [EnableCors("AllowAll")]
    [ApiController]    
    public class RegistrationServiceController : ControllerBase
    {
        private readonly IPersonRepo _personRepo;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public RegistrationServiceController(
            IPersonRepo perosnRepo, 
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            IConfiguration configuration, 
            IMapper mapper)
        {
            _personRepo = perosnRepo;

            _userManager = userManager;

            _signInManager = signInManager;

            _configuration = configuration;

            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationVM regVM){
            
            if (!ModelState.IsValid)
                return BadRequest (ModelState); //retruns model state error         

            if (await _personRepo.IsUserExist(regVM.Username))            
                ModelState.AddModelError("Username", "Username already exist");    

            regVM.Username = regVM.Username.ToLower();

            var user = new ApplicationUser { UserName = regVM.Username, Email = regVM.Username };

            var result = await _userManager.CreateAsync(user, regVM.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");

                regVM.IdentityId = user.Id;

                var personModel = _mapper.Map<Person>(regVM);

                var newUser = await _personRepo.Register(personModel);

                // var newUser = await _personRepo.Register(new Models.Person {
                //     IdentityId = user.Id,
                //     FirstName = regVM.FirstName,
                //     LastName = regVM.LastName,
                //     Username = regVM.Username
                // });
                
                var tokenString = new JWTGenerator().GenerateJWT(user, _configuration);

                await _signInManager.SignInAsync(user, false);

                return Ok(new { 

                    token = tokenString ,username = newUser.Username 

                });
              
            }
        
            return BadRequest (result.Errors); 

        }

    }
}