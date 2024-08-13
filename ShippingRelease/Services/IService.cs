using ShippingRelease.Models;
using System.Diagnostics;
using System.Net.Http.Json;

namespace ShippingRelease.Services;

public class IService : IRepository
{
    public async Task<User> Login(string email, string password)
    {
        try
        {
            var client = new HttpClient();
            string url = "http://10.170.50.109:5223/api/users/login/" + email + "/" + password;
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            if (response.IsSuccessStatusCode)
            {
                User user = await response.Content.ReadFromJsonAsync<User>();
                return await Task.FromResult(user);
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", $"HTTP Error: {response.StatusCode}, {response.ReasonPhrase}", "Ok");
            }
            return null;
        }
        catch (HttpRequestException httpEx)
        {
            Debug.WriteLine($"Request Exception: {httpEx.Message}");
            Debug.WriteLine($"Request Exception (InnerException): {httpEx.InnerException?.Message}");
            await Shell.Current.DisplayAlert("Error", "Unable to reach the server. Please check your internet connection and try again.", "Ok");
            return null;
        }
        catch (TaskCanceledException taskEx)
        {
            Debug.WriteLine($"Request Timed Out: {taskEx.Message}");
            await Shell.Current.DisplayAlert("Error", "The request timed out. Please try again", "Ok");
            return null;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            return null;
        }
    }

    public async Task<User> Register(User user)
    {
        try
        {
            var client = new HttpClient();
            string url = "http://10.170.50.109:5223/api/users/Register/" + user.Name + "/" + user.Email + "/" + user.Password;
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress, user);
            response.EnsureSuccessStatusCode();
            return await Task.FromResult(user);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            return null;
        }
    }
    public async Task<ShippingReleaseModel> SearchSkidData(string skidSN)
    {
        try
        {
            var client = new HttpClient();
            string url = "http://10.170.50.109:5223/api/ShippingRelease/" + skidSN;
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            if (response.IsSuccessStatusCode)
            {
                ShippingReleaseModel shipRelease = await response.Content.ReadFromJsonAsync<ShippingReleaseModel>();
                return await Task.FromResult(shipRelease);
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", $"HTTP Error: {response.StatusCode}, {response.ReasonPhrase}", "Ok");
            }
            return null;
        }
        catch (HttpRequestException httpEx)
        {
            Debug.WriteLine($"Request Exception: {httpEx.Message}");
            Debug.WriteLine($"Request Exception (InnerException): {httpEx.InnerException?.Message}");
            await Shell.Current.DisplayAlert("Error", "Unable to reach the server. Please check your internet connection and try again.", "Ok");
            return null;
        }
        catch (TaskCanceledException taskEx)
        {
            Debug.WriteLine($"Request Timed Out: {taskEx.Message}");
            await Shell.Current.DisplayAlert("Error", "The request timed out. Please try again", "Ok");
            return null;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            return null;
        }
    }
    public async Task<ShipSkid> PostShipData(ShipSkid shipSkid)
    {
        try
        {
            var client = new HttpClient();
            string url = "http://10.170.50.109:5223/api/ShippingRelease/Ship/" + shipSkid.Part_Status + "/" + shipSkid.Skid_Status + "/" + shipSkid.Skid_Serial_Number + "/" + shipSkid.Shipping_Label + "/" + shipSkid.Full_Shipping_Label;
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress, shipSkid);
            response.EnsureSuccessStatusCode();
            return await Task.FromResult(shipSkid);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            return null;
        }
    }
}
