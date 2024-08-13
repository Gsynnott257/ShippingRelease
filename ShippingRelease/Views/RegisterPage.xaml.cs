using ShippingRelease.ViewModels;

namespace ShippingRelease.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
    protected override void OnAppearing()
    {
        EmailEntry.Focus();
    }
}