using Microsoft.AspNetCore.Mvc;
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
    public class TodoController : ControllerBase
    {
        private readonly IService<Todo> _service;

        public TodoController(IService<Todo> service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Todo>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Todo>> GetById(int id) 
        {
            return Ok(await _service.GetById(id));
        }

        [HttpPost]
        public ActionResult<User> CreateUser(Todo todo)
        {
            if (ModelState.IsValid)
            {
                _service.Create<ToDoValidator>(todo);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult<User> EditUser(int id, Todo todo)
        {
            if (id != todo.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                _service.Update<ToDoValidator>(todo);
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
            var todo = await _service.GetById(id);

            if (todo == null) return NotFound();

            await _service.Delete(id);
            return Ok();
        }
    }
}
