using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
//using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using ShippingRelease.Models;
using ShippingRelease.Services;
using ShippingRelease.Views;

namespace ShippingRelease.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    private string _email;
    [ObservableProperty]
    private string _password;

    readonly IRepository loginService = new IService();

    [RelayCommand]
    public async void Signin()
    {
        if(Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password))
                {
                    User user = await loginService.Login(Email, Password);
                    if(user != null)
                    {
                        if (Preferences.ContainsKey(nameof(App.user)))
                        {
                            Preferences.Remove(nameof(App.user));
                        }
                        string userDetails = JsonConvert.SerializeObject(user);
                        Preferences.Set(nameof(App.user), userDetails);
                        App.user = user;
                        Email = null;
                        Password = null;
                        await Shell.Current.GoToAsync(nameof(HomePage));
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error", "Email/Password Incorrect", "Ok");
                        return;
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "All Fields Required", "Ok");
                    return;
                }
            }
            catch (HttpRequestException httpEx)
            {
                await Shell.Current.DisplayAlert("Connection Error", "Unable to reach the server. Please check your internet connection and try again.", "Ok");
                return;
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
    [RelayCommand]
    public async void GoToRegisterPage()
    {
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }
}
