using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using MealPlannerClient.App.Pages;

namespace MealPlannerClient.App.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private ContentPage _currentPage;
        public ContentPage CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

        public ICommand ChangeContentCommand { get; }

        public MainViewModel()
        {
            ChangeContentCommand = new Command<string>(OnChangeContent);
            CurrentPage = new InventoryPage();
        }

        private void OnChangeContent(string page)
        {
            if (page == "Page1")
                Shell.Current.GoToAsync("//page1");
            else if (page == "Page2")
                Shell.Current.GoToAsync("//page2");
        }
    }
}
