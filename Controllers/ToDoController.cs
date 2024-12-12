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

        [HttpGet("{id}")]
        public ActionResult GetTodoById(int id)
        {
            return Ok(_map.Map<TodoReadDto>(_repo.GetToDoById(id)));
        }
    }
}