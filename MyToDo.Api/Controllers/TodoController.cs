using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly IServiceAuth<Todo> _service;
        private readonly IMapper _mapper;

        public TodoController(IServiceAuth<Todo> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoDto>> GetAll()
        {
            var todos = _mapper.Map<List<TodoDto>>(_service.GetAll(GetUserId()));
            return Ok(todos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TodoDto>> GetById(int id) 
        {
            var todoFound = _mapper.Map<TodoDto>(await _service.GetById(id, GetUserId()));
            if (todoFound == null) return NotFound();
            return Ok(todoFound);
        }

        [HttpPost]
        public ActionResult<User> CreateToDo(Todo todo)
        {
            if (ModelState.IsValid)
            {
                todo.Id = GetUserId();
                _service.Create<ToDoValidator>(todo, GetUserId());
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult<User> EditToDo(int id, Todo todo)
        {
            if (ModelState.IsValid)
            {
                todo.Id = GetUserId();
                _service.Update<ToDoValidator>(todo, id, GetUserId());
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
            var todo = await _service.GetById(id, GetUserId());

            if (todo == null) return NotFound();

            await _service.Delete(id, GetUserId());
            return Ok();
        }
        private int GetUserId()
        {
            return int.Parse(this.User.Claims.First(i => i.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value);
        }
    }
}
