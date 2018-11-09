using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClientAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClientAPI.Controllers
{
    [Authorize(Policy = "ApiPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]")]    
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ValuesController(UserManager<ApplicationUser> usermanager)
        {
            _userManager = usermanager;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {    
            var user = User.Identity;
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            var role = claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault().Value;
            
            // var roles = claims.Where(c => c.Type == roleClaimType).ToList();
            if(User.IsInRole("Admin"))
             return new string[] { "Pasok", "Ako" };


            return new string[] { "value1", "value2" };
        }
        
        [AllowAnonymous]
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
