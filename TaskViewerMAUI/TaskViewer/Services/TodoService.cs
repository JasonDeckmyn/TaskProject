using Microsoft.Maui.Controls.Platform.Compatibility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TaskViewer.Models;

namespace TaskViewer.Services
{
    class TodoService
    {
        private readonly HttpClient _httpClient;

        public TodoService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<TodoModel>> GetTodosAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<TodoModel>>("http://localhost:5111/api/todo/");
            return response ?? new List<TodoModel>();
        }

        public async Task<bool> AddTodoAsync(TodoModel newTodo)
        {
            try
            {
                // Convert UrgencyLevel enum to string
                var todoToSend = new
                {
                    Id = newTodo.Id,
                    Title = newTodo.Title,
                    Description = newTodo.Description,
                    Urgency = newTodo.Urgency.ToString() // Convert enum to string
                };

                var response = await _httpClient.PostAsJsonAsync("http://localhost:5111/api/todo/", todoToSend);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Error adding todo: {response.StatusCode}, {content}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error adding todo: {ex.Message}");
                return false;
            }
        }
    }
}
