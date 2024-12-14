using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnet.Models;

namespace TaskProject.Repostitories
{
    public class MockRepo : IRepo
    {
        List<Todo> todolist = new List<Todo>();

        public MockRepo()
        {
            // todolist.Add(new Todo(){ Id = 1, Title = "Learn C#", Urgency = "High" });
            // todolist.Add(new Todo(){ Id = 2, Title = "Learn ASP.NET", Urgency = "Medium" });
            // todolist.Add(new Todo(){ Id = 3, Title = "Learn EF Core", Urgency = "High" });
            // todolist.Add(new Todo(){ Id = 4, Title = "Learn Azure", Urgency = "Low" });
        }

        public void AddTodo(Todo t)
        {
            todolist.Add(t);
        }

        public void DeleteTodo(Todo t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Todo> GetAllTodo()
        {
            return todolist;
        }

        public Todo GetToDoById(int id)
        {
           Todo _todo = todolist.FirstOrDefault<Todo>(t => t.Id == id);
           return _todo;
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateTodo(Todo t)
        {
            throw new NotImplementedException();
        }
    }
}