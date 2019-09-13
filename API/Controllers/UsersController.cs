using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Repository.Interfaces;
using AutoMapper;
using Core.Dto;
using Entities.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository _repository;

        private readonly IMapper _mapper;

        public UsersController(IUserRepository repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
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
            if (obj != null)
            {
                return NotFound();
            }

            return Ok(obj);
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDto>> Post([FromBody] UserDto user)
        {
            if (user == null)
            {
                return BadRequest("User is Null");
            }

            var obj = _mapper.Map<UserDto>(await _repository.Add(Mapper.Map<User>(user)));

            return Ok(obj);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        public async Task<ActionResult<UserDto>> UpdateRole(string roleId, string id)
        {
            User user = await _repository.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            user = await _repository.UpdatePassword(user, roleId);

            if (user == null)
            {
                return StatusCode(500);
            }

            return Ok(user);
        }
    }
}