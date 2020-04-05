using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTO.Security;
using Core.Entities.Security;
using Core.Interfaces.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NGuard;

namespace WhiteLabelAPI.Controllers
{
    [Route("api/[controller]")]
    public class SecurityController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISecurityService _securityService;

        public SecurityController(IMapper mapper,
            ISecurityService securityService)
        {
            Guard.Requires(mapper, nameof(mapper)).IsNotNull();
            Guard.Requires(securityService, nameof(securityService)).IsNotNull();

            _mapper = mapper;
            _securityService = securityService;
        }

        [HttpGet, Route("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var res = await _securityService.GetAllUsers();
            if (res.Count == 0)
            {
                return NotFound("No coincidences.");
            }

            return Ok(_mapper.Map<List<UserDTO>>(res));
        }

        [HttpPost, Route("users")]
        public async Task<IActionResult> AddUser([FromBody] UserDTO user)
        {
            Guard.Requires(user, nameof(user)).IsNotNull();
            return Ok(await _securityService.AddUser(_mapper.Map<User>(user)));
        }

        [HttpPut, Route("users")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO user)
        {
            Guard.Requires(user, nameof(user)).IsNotNull();
            return Ok(await _securityService.UpdateUser(_mapper.Map<User>(user)));
        }

        [HttpDelete, Route("users")]
        public async Task<IActionResult> DeleteUser(string userReference)
        {
            return Ok(await _securityService.DeleteUser(userReference));
        }

    }
}