using aspnet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskProject.Repostitories;

namespace aspnet.Controllers
{
    [ApiController]
    [Route("api/todo")]
    public class ToDoController : ControllerBase
    {

        private readonly IRepo _repo;

        public ToDoController(IRepo repo)
        { 
            _repo = repo;
        }

        [HttpGet]
        public ActionResult GetAllTodo()
        {
            return Ok(_repo.GetAllTodo());
        }

        [HttpGet("{id}")]
        public ActionResult GetTodoById(int id)
        {
            return Ok(_repo.GetToDoById(id));
        }
    }
}