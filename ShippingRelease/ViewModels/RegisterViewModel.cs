using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShippingRelease.Models;
using ShippingRelease.Services;
using ShippingRelease.Views;

namespace ShippingRelease.ViewModels;

public partial class RegisterViewModel : ObservableObject
{
    [ObservableProperty]
    private string _name;
    [ObservableProperty]
    private string _email;
    [ObservableProperty]
    private string _password;

    readonly IRepository loginService = new IService();

    [RelayCommand]
    public async void Register()
    {
        if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Name))
                {
                    User user = new User
                    {
                        Name = Name,
                        Email = Email,
                        Password = Password,
                    };
                    await loginService.Register(user);
                    Email = null;
                    Password = null;
                    Name = null;
                    await Shell.Current.GoToAsync("///" + nameof(LoginPage));
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "All Fields Required", "Ok");
                    return;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
                return;
            }
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "No Network Access", "Ok");
            return;
        }
    }
}
