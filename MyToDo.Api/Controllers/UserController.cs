using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Models;
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
        private readonly IService<Domain.Entities.User> _service;

        public UserController(IService<Domain.Entities.User> service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id:int}")]
        public ActionResult<User> GetById(int id)
        {
            return Ok(_service.GetById(id));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            var user = await _service.GetById(id);

            if (user == null) return NotFound();

            await _service.Delete(id);
            return Ok();
        }

        [HttpPost]
        public ActionResult<User> CreateUser(Domain.Entities.User user)
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
        public ActionResult<User> EditUser(int id, Domain.Entities.User user)
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
    }
}
