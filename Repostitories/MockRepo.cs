using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnet.Models;

namespace TaskProject.Repostitories
{
    public class MockRepo : IRepo
    {
        List<ToDo> todolist = new List<ToDo>();

        public MockRepo()
        {
            todolist.Add(new ToDo(){ Id = 1, Title = "Learn C#", Urgency = "High" });
            todolist.Add(new ToDo(){ Id = 2, Title = "Learn ASP.NET", Urgency = "Medium" });
            todolist.Add(new ToDo(){ Id = 3, Title = "Learn EF Core", Urgency = "High" });
            todolist.Add(new ToDo(){ Id = 4, Title = "Learn Azure", Urgency = "Low" });
        }

        public IEnumerable<ToDo> GetAllTodo()
        {
            return todolist;
        }

        public ToDo GetToDoById(int id)
        {
           ToDo _todo = todolist.FirstOrDefault<ToDo>(t => t.Id == id);
           return _todo;
        }
    }
}