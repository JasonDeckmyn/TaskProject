using TaskViewer.Services;
using TaskViewer.Models;
using System.Collections.ObjectModel;
using TaskViewer.Views;

namespace TaskViewer
{
    public partial class MainPage : ContentPage
    {
        private readonly TodoService _todoService;
        public ObservableCollection<TodoModel> Todos { get; set; }

        public MainPage()
        {
            InitializeComponent();
            _todoService = new TodoService();
            Todos = new ObservableCollection<TodoModel>();
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

        private async void OnAddTodoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTodoPage(this));
        }

        public void AddTodoToCollection(TodoModel newTodo)
        {
            Todos.Add(newTodo);
        }
    }
}
