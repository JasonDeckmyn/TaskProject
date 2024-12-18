using Microsoft.Maui.Controls.Platform.Compatibility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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
            var response = await _httpClient.GetFromJsonAsync<List<TodoModel>>("http://10.0.2.2:5111/api/todo/");
            return response ?? new List<TodoModel>();
        }

        public async Task<bool> AddTodoAsync(TodoModel newTodo)
        {
            var todoToSend = new
            {
                Id = newTodo.Id,
                Title = newTodo.Title,
                Description = newTodo.Description,
                Urgency = newTodo.Urgency.ToString()
            };

            var response = await _httpClient.PostAsJsonAsync("http://10.0.2.2:5111/api/todo/", todoToSend);
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

        public async Task<bool> DeleteTodoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"http://10.0.2.2:5111/api/todo/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }else
            {
                var content = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Error deleting todo: {response.StatusCode}, {content}");
                return false;
            }
        }

        public async Task<bool> UpdateTodoAsync(TodoModel todo)
        {
            var todoUpdate = new
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Urgency = todo.Urgency.ToString(),
                IsComplete = todo.IsComplete
            };

            var jsonContent = JsonSerializer.Serialize(todoUpdate);
            Debug.WriteLine($"Sending JSON: {jsonContent}");

            var response = await _httpClient.PutAsJsonAsync($"http://10.0.2.2:5111/api/todo/{todoUpdate.Id}", todoUpdate);
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"Service: reponse -> {response} " +
                    $"\nTodo: {jsonContent}");
                return true;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Error updating todo: {response.StatusCode}, {content}");
                return false;
            }
        }
    }
}
