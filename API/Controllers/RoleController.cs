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

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRoleRepository _repository;

        private readonly IMapper _mapper;

        public RoleController(IRoleRepository repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAll()
        {
            var list = _mapper.Map<IEnumerable<RoleDto>>(await _repository.GetAll());
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
        public async Task<ActionResult<RoleDto>> Get(string id)
        {
            var obj = _mapper.Map<RoleDto>(await _repository.Get(id));
            if (obj == null)
            {
                return NotFound();
            }

            return Ok(obj);
        }

        // POST api/values
        [HttpPost]
        [Role(RoleEnum.Admin)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<RoleDto>> Post([FromBody] RoleDto user)
        {
            if (user == null)
            {
                return BadRequest("Role is Null");
            }

            RoleDto obj = _mapper.Map<RoleDto>(await _repository.Add(_mapper.Map<Role>(user)));

            return Ok(obj);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Role(RoleEnum.Admin)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Delete(string id)
        {
            ActionResult<int> action;
            Role role = await _repository.Get(id);
            if (role == null)
            {
                action = NotFound();
            }
            else
            {
                int obj = await _repository.Delete(role);
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