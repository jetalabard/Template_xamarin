using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using API.Repository.Interfaces;
using AutoMapper;
using Core;
using Core.Dto;
using Entities.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository _repository;

        private readonly IMapper _mapper;

        private readonly AppSettings _appSettings;

        public UsersController(IUserRepository repo, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("authenticate")]
        public async Task<ActionResult<UserDto>> Authenticate([FromBody]LoginAuth userlogin)
        {
            User user = await _repository.Authenticate(userlogin.User, userlogin.Password, _appSettings.Secret);

            if (user == null)
            {
                return BadRequest(new LoginResult(LoginStatus.InvalidCredential, null));
            }

            return Ok(_mapper.Map<UserDto>(user));
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("register")]
        [Role(RoleEnum.Admin)]
        public async Task<ActionResult<UserDto>> Register([FromBody]UserDto user, string userPassword)
        {
            if (user == null)
            {
                return BadRequest("User is Null");
            }

            if (string.IsNullOrWhiteSpace(userPassword))
            {
                return BadRequest("Passsword is Null");
            }

            if (await _repository.CheckPersonalIdExist(user.PersonalId))
            {
                return BadRequest("Personnal Id already exist");
            }

            User result = await _repository.Create(_mapper.Map<User>(user), userPassword);
            if (result != null)
            {
                return Ok(_mapper.Map<UserDto>(result));
            }

            return BadRequest();
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var list = _mapper.Map<IEnumerable<UserDto>>(await _repository.GetAll());
            if (!list.Any())
            {
                return NoContent();
            }

            return Ok(list);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> Get(string id)
        {
            var obj = _mapper.Map<UserDto>(await _repository.Get(id));
            if (obj == null)
            {
                return NotFound();
            }

            return Ok(obj);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Role(RoleEnum.Admin)]
        public async Task<ActionResult<int>> Delete(string id)
        {
            ActionResult<int> action;
            User user = await _repository.Get(id);
            if (user == null)
            {
                action = NotFound();
            }
            else
            {
                int obj = await _repository.Delete(user);
                if (obj != 1)
                {
                    action = BadRequest();
                }
                else
                {
                    action = Ok(obj);
                }
            }

            return action;
        }

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Role(RoleEnum.Admin | RoleEnum.Default)]
        [HttpPut("password/{password}/{id}")]
        public async Task<ActionResult<UserDto>> UpdatePassword(string password, string id)
        {
            User user = await _repository.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            user = await _repository.UpdatePassword(user, password);

            if (user == null)
            {
                return StatusCode(500);
            }

            return Ok(user);
        }

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("email/{email}/{id}")]
        [Role(RoleEnum.Admin | RoleEnum.Default)]
        public async Task<ActionResult<UserDto>> UpdateEmail(string email, string id)
        {
            User user = await _repository.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            user = await _repository.UpdateEmail(user, email);

            if (user == null)
            {
                return StatusCode(500);
            }

            return Ok(user);
        }

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("role/{roleId}/{id}")]
        [Role(RoleEnum.Admin)]
        public async Task<ActionResult<UserDto>> UpdateRole(string roleId, string id)
        {
            User user = await _repository.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            user = await _repository.UpdateRole(user, roleId);

            if (user == null)
            {
                return StatusCode(500);
            }

            return Ok(user);
        }
    }
}