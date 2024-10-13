using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MealPlannerClient.App.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private string curentPage = string.Empty;

        public MainViewModel()
        {
        }

        [RelayCommand]
        private async Task NavigationItemClicked(string page)
        {

            await Shell.Current.GoToAsync($"//{page}");
        }
    }
}
