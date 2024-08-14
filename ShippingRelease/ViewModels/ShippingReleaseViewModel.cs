using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShippingRelease.Models;
using ShippingRelease.Services;
using ShippingRelease.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShippingRelease.ViewModels;
//piukhkhj
//sdfsdf
public partial class ShippingReleaseViewModel : ObservableObject
{
    [ObservableProperty]
    private string _skidSN;

    [ObservableProperty]
    private string _shippingLabel;

    [ObservableProperty]
    private string _imageSource;

    [ObservableProperty]
    private string _shipCount;

    [ObservableProperty]
    private ObservableCollection<ShipSkid> _scannedSkids = new();

    // This method is automatically called when SkidSN or ShippingLabel changes
    partial void OnSkidSNChanged(string value) => CheckInputAndUpdateStatus();
    partial void OnShippingLabelChanged(string value) => CheckInputAndUpdateStatus();

    private void CheckInputAndUpdateStatus()
    {
        Debug.WriteLine(SkidSN);
        // You can replace this with your own logic to determine if inputs are valid
        if (!string.IsNullOrWhiteSpace(SkidSN) && !string.IsNullOrWhiteSpace(ShippingLabel))
        {
            //ImageSource = ImageSource.FromResource("ShippingRelease.Resources.Images.check.png", typeof(ShippingReleaseViewModel).GetTypeInfo().Assembly);

            ImageSource = "check.png"; // Assuming this updates the status icon
        }
        else
        {
            ImageSource = "xicon.png";
        }
    }

    [RelayCommand]
    public async void ShipSkids()
    {
        // Assuming ImageSource is used to determine if both SkidSN and ShippingLabel are valid
        if (ImageSource == "check.png")
        {
            // Add the scanned skid to the list
            ScannedSkids.Add(new ShipSkid { Skid_Serial_Number = SkidSN, Shipping_Label = ShippingLabel });

            // Update the ship count (assuming this is the count of scanned skids)
            ShipCount = ScannedSkids.Count.ToString();

            // Clear the input fields
            SkidSN = string.Empty;
            ShippingLabel = string.Empty;
        }
    }

    [RelayCommand]
    public async void GoToHomePage()
    {
        await Shell.Current.GoToAsync("///" + nameof(HomePage));
    }
}

