using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Models;
using MyToDo.Domain.Entities;
using MyToDo.Domain.Interfaces;
using MyToDo.Services.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IService<User> _service;
        private readonly IMapper _mapper;

        public UserController(IService<User> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetAll()
        {
            var users = _mapper.Map<List<UserDto>>(_service.GetAll());
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public ActionResult<UserDto> GetById(int id)
        {
            var userFound = _mapper.Map<UserDto>(_service.GetById(id).Result);
            return Ok(userFound);
        }

        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                _service.Create<UserValidator>(user);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult EditUser(int id, User user)
        {
            if (id != user.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                _service.Update<UserValidator>(user);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            var user = await _service.GetById(id);

            if (user == null) return NotFound();

            await _service.Delete(id);
            return Ok();
        }
    }
}
