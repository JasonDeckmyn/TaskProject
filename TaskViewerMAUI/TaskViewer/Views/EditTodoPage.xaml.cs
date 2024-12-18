using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskViewer.Models;
using TaskViewer.Services;


namespace TaskViewer.Views;

public partial class EditTodoPage : ContentPage
{
    private readonly TodoService _todoService;
    private readonly MainPage _mainPage;
    public TodoModel Todo { get; set; }
    public ObservableCollection<UrgencyLevel> UrgencyLevels { get; set; }
    public ICommand SaveCommand { get; }

    public EditTodoPage(TodoModel todo, MainPage mainPage)
    {
        InitializeComponent();
        _todoService = new TodoService();
        _mainPage = mainPage;
        Todo = todo;
        UrgencyLevels = new ObservableCollection<UrgencyLevel> { UrgencyLevel.Low, UrgencyLevel.Medium, UrgencyLevel.High };
        SaveCommand = new Command(OnSaveTodo);
        BindingContext = this;
    }

    private async void OnSaveTodo()
    {
        bool success = await _todoService.UpdateTodoAsync(Todo);
        if (success)
        {
            _mainPage.RefreshTodoList();
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Failed to save the todo.", "OK");
        }
    }
}
