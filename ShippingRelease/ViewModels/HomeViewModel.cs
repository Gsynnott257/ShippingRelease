using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShippingRelease.Views;

namespace ShippingRelease.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    [RelayCommand]
    public async void Logout()
    {
        Preferences.Remove(nameof(App.user));
        await Shell.Current.GoToAsync("///" + nameof(LoginPage));
    }

    [RelayCommand]
    public async void ShippingPage()
    {
        await Shell.Current.GoToAsync("///" + nameof(ShippingReleasePage));
    }
}
