using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnet.Models;

namespace TaskProject.Repostitories
{
    public interface IRepo
    {
        IEnumerable<ToDo> GetAllTodo();
        ToDo GetToDoById(int id);
    }
}