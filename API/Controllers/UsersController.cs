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
            var list = Mapper.Map<IEnumerable<UserDto>>(await _repository.GetAll());
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
            var obj = Mapper.Map<UserDto>(await _repository.Get(id));
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
            var obj = Mapper.Map<UserDto>(await _repository.Add(Mapper.Map<User>(user)));
            if (obj != null)
            {
                return BadRequest();
            }

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
    }
}