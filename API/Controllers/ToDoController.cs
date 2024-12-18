using aspnet.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskProject.dto;
using TaskProject.Repostitories;

namespace aspnet.Controllers
{
    [ApiController]
    [Route("api/todo")]
    public class ToDoController : ControllerBase
    {

        private readonly IRepo _repo;
        private readonly IMapper _map;

        public ToDoController(IRepo repo, IMapper map)
        { 
            _repo = repo;
            _map = map;
        }

        [HttpGet]
        public ActionResult GetAllTodo()
        {
            return Ok(_map.Map<IEnumerable<TodoReadDto>>(_repo.GetAllTodo()));
        }

        [HttpGet("{id}", Name ="GetTodoById")]
        public ActionResult GetTodoById(int id)
        {
            return Ok(_map.Map<TodoReadDto>(_repo.GetToDoById(id)));
        }

        [HttpPost]
        public ActionResult AddTodo(TodoWriteDto t)  
        {
            var todo = _map.Map<Todo>(t);

            _repo.AddTodo(todo);
            _repo.SaveChanges();

            // REST API for response 201
            return CreatedAtRoute(nameof(GetTodoById), new {Id = todo.Id}, todo);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTodo(int id, TodoUpdateDto t)
        {
            var existingTodo = _repo.GetToDoById(id);

            if(existingTodo == null)
            {
                return NotFound();
            }

            _map.Map(t, existingTodo);

            _repo.UpdateTodo(existingTodo);
            _repo.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTodo(int id)
        {
            var existingTodo = _repo.GetToDoById(id);

            if(existingTodo == null)
            {
                return NotFound();
            }

            _repo.DeleteTodo(existingTodo);
            _repo.SaveChanges();

            return Ok();
        }
    }
}