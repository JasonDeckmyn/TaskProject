using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnet.Models;

namespace TaskProject.Repostitories
{
    public interface IRepo
    {
        IEnumerable<Todo> GetAllTodo();
        Todo GetToDoById(int id);

        void AddTodo(Todo t);
        void UpdateTodo(Todo t);
        void SaveChanges();
        void DeleteTodo(Todo t);
    }
}