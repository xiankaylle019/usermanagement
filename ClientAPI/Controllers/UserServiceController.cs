using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ClientAPI.DataAccess.Contracts;
using ClientAPI.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ClientAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserServiceController : ControllerBase
    {
        private readonly IPersonRepo _personRepo;
        private readonly IMapper _mapper;
        public UserServiceController( IPersonRepo perosnRepo, IMapper mapper)
        {
            _personRepo = perosnRepo;

            _mapper = mapper;
            
        }

        [HttpGet("getusers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _personRepo.GetAllAsync();

            var usersToReturn = _mapper.Map<IEnumerable<UsersDTO>>(users);

            return Ok(usersToReturn);
        }

    }
}