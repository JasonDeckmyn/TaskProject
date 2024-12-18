namespace TaskViewer.Views;

using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskViewer.Models;
using TaskViewer.Services;
using Microsoft.Maui.Controls;
using System.Diagnostics;

public partial class AddTodoPage : ContentPage
{
    private readonly TodoService _todoService;
    private readonly MainPage _mainPage;
    public TodoModel NewTodo { get; set; }
    public ObservableCollection<UrgencyLevel> UrgencyLevels { get; set; }
    public ICommand SaveTodoCommand { get; set; }

    public AddTodoPage(MainPage mainPage)
    {
        InitializeComponent();
        _todoService = new TodoService();
        _mainPage = mainPage;
        NewTodo = new TodoModel();
        UrgencyLevels = new ObservableCollection<UrgencyLevel> { UrgencyLevel.Low, UrgencyLevel.Medium, UrgencyLevel.High };
        SaveTodoCommand = new Command(OnSaveTodo);
        BindingContext = this;
    }

    private async void OnSaveTodo()
    {
        bool success = await _todoService.AddTodoAsync(NewTodo);
        if (success)
        {
            _mainPage.RefreshTodoList();
            await Navigation.PopAsync();
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Failed to add the todo: Title={NewTodo.Title}, Description={NewTodo.Description}, Urgency={NewTodo.Urgency}", "OK");
        }
}
}

