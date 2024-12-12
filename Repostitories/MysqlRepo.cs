using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnet.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using TaskProject.Contexts;

namespace TaskProject.Repostitories
{
    public class MysqlRepo : IRepo
    {

        private readonly ToDoContext _context;

        public MysqlRepo(ToDoContext context)
        {
            _context = context;
        }

        public IEnumerable<ToDo> GetAllTodo()
        {
            return _context.Todos;
        }

        public ToDo GetToDoById(int id)
        {
            return _context.Todos.FirstOrDefault<ToDo>(t => t.Id == id);
        }
    }
}