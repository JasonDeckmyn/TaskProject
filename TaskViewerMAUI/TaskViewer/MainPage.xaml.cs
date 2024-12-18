using TaskViewer.Services;
using TaskViewer.Models;
using System.Collections.ObjectModel;
using TaskViewer.Views;
using System.Diagnostics;
using System.Windows.Input;

namespace TaskViewer
{
    public partial class MainPage : ContentPage
    {
        private readonly TodoService _todoService;
        public ObservableCollection<TodoModel> Todos { get; set; }
        public ICommand RemoveTodoCommand { get; }

        public MainPage()
        {
            InitializeComponent();
            _todoService = new TodoService();
            Todos = new ObservableCollection<TodoModel>();
            RemoveTodoCommand = new Command<int>(OnRemoveTodo);
            BindingContext = this;
            LoadTodos();
        }

        private async void LoadTodos()
        {
            var todos = await _todoService.GetTodosAsync();
            foreach (var todo in todos)
            {
                Todos.Add(todo);
            }
        }

        public async void RefreshTodoList()
        {
            Todos.Clear();
            var todos = await _todoService.GetTodosAsync();
            foreach (var todo in todos)
            {
                Todos.Add(todo);
            }
        }

        private async void OnAddTodoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTodoPage(this));
        }

        private async void OnRemoveTodo(int todoId)
        {
            bool success = await _todoService.DeleteTodoAsync(todoId);
            if (success)
            {
                var todo = Todos.FirstOrDefault(t => t.Id == todoId);
                if (todo != null)
                {
                    Todos.Remove(todo);
                }
            }
            else
            {
                await DisplayAlert("Error", "Failed to remove the todo.", "OK");
            }
        }

        private async void OnToggleStatusClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.CommandParameter is int todoId)
            {
                var todo = Todos.FirstOrDefault(t => t.Id == todoId);
                if (todo != null)
                {
                    todo.IsComplete = !todo.IsComplete;
                    Debug.WriteLine($"Toggling status of todo: {todo.Id}, {todo.Description}, {todo.IsComplete}");
                    bool success = await _todoService.UpdateTodoAsync(todo);
                    if (!success)
                    {
                        todo.IsComplete = !todo.IsComplete;
                        await DisplayAlert("Error", $"Failed to update the todo: {todo.Id}, {todo.Description}, {todo.IsComplete}", "OK");
                    }
                }
            }
        }

        private async void OnEditTodoClicked(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            if (button != null && button.CommandParameter is int todoId)
            {
                var todo = Todos.FirstOrDefault(t => t.Id == todoId);
                if (todo != null)
                {
                    await Navigation.PushAsync(new EditTodoPage(todo, this));
                }
            }
        }
    }
}
