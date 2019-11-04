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

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : AbstractController
    {
        private readonly IRoleRepository _repository;

        private readonly IMapper _mapper;

        public RoleController(IRoleRepository repo, IMapper mapper, ILoggerFactory loggerFactory)
            : base(mapper, loggerFactory, nameof(RoleController))
        {
            _repository = repo;
            _mapper = mapper;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAll()
        {
            async Task<IEnumerable<Role>> Action()
            {
                return await _repository.GetAll();
            }

            return await ManageError<IEnumerable<RoleDto>, IEnumerable<Role>>(Action);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoleDto>> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id is null or empty");
            }

            async Task<Role> Action()
            {
                return await _repository.Get(id);
            }

            return await ManageError<RoleDto, Role>(Action);
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

            async Task<Role> Action()
            {
                return await _repository.Add(_mapper.Map<Role>(user));
            }

            return await ManageError<RoleDto, Role>(Action);
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

            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id is null");
            }

            Role role = await _repository.Get(id);
            if (role == null)
            {
                return NotFound("Object is not found");
            }

            int ret = await _repository.Delete(role);
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
    }
}