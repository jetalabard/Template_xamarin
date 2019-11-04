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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : AbstractController
    {
        private readonly IUserRepository _repository;

        private readonly IMapper _mapper;

        private readonly AppSettings _appSettings;

        public UsersController(IUserRepository repo, IOptions<AppSettings> appSettings, IMapper mapper, ILoggerFactory loggerFactory)
            : base(mapper, loggerFactory, nameof(UsersController))
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
            if (userlogin == null || string.IsNullOrEmpty(userlogin.User) || string.IsNullOrEmpty(userlogin.Password))
            {
                return BadRequest(new ApiError(400, "parameters null", Logger));
            }

            async Task<User> Action()
            {
                return await _repository.Authenticate(userlogin.User, userlogin.Password, _appSettings.Secret).ConfigureAwait(false);
            }

            return await ManageError<UserDto, User>(Action).ConfigureAwait(false);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("register")]
        [Role(RoleEnum.Admin)]
        public async Task<ActionResult<UserDto>> Register([FromBody]SecureUserDto user)
        {
            if (user == null)
            {
                return BadRequest("User is Null");
            }

            if (string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest("Passsword is Null");
            }

            if (await _repository.CheckPersonalIdExist(user.PersonalId))
            {
                return BadRequest("Personnal Id already exist");
            }

            async Task<User> Action()
            {
                return await _repository.Create(Mapper.Map<User>(user), user.Password);
            }

            return await ManageError<UserDto, User>(Action);
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            async Task<IEnumerable<User>> Action()
            {
                return await _repository.GetAll();
            }

            return await ManageError<IEnumerable<UserDto>, IEnumerable<User>>(Action);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id is null or empty");
            }

            async Task<User> Action()
            {
                return await _repository.Get(id);
            }

            return await ManageError<UserDto, User>(Action);
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

            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id is null");
            }

            User user = await _repository.Get(id);
            if (user == null)
            {
                return NotFound("Object is not found");
            }

            int ret = await _repository.Delete(user);
            if (ret != 1)
            {
                action = BadRequest();
            }
            else
            {
                action = Ok(ret);
            }

            return action;
        }

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Role(RoleEnum.Admin | RoleEnum.Default)]
        [HttpPut("password")]
        public async Task<ActionResult<UserDto>> UpdatePassword([FromBody]LoginAuth userlogin)
        {
            async Task<User> Action()
            {
                bool isUpdate = await _repository.UpdatePassword(userlogin.User, userlogin.Password).ConfigureAwait(false);
                if (isUpdate)
                {
                    return await _repository.Get(userlogin.User).ConfigureAwait(false);
                }
                else
                {
                    throw new Exception("Update not executed");
                }
            }

            return await ManageError<UserDto, User>(Action).ConfigureAwait(false);
        }

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("email/{email}/{id}")]
        [Role(RoleEnum.Admin | RoleEnum.Default)]
        public async Task<ActionResult<UserDto>> UpdateEmail(string email, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id is null or empty");
            }

            async Task<User> Action()
            {
                return await _repository.UpdateEmail(id, email);
            }

            return await ManageError<UserDto, User>(Action);
        }

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("role/{roleId}/{id}")]
        [Role(RoleEnum.Admin)]
        public async Task<ActionResult<UserDto>> UpdateRole(string roleId, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id is null or empty");
            }

            async Task<User> Action()
            {
                return await _repository.UpdateRole(id, roleId);
            }

            return await ManageError<UserDto, User>(Action);
        }
    }
}