using ShippingRelease.ViewModels;

namespace ShippingRelease.Views;

public partial class ShippingReleasePage : ContentPage
{
	public ShippingReleasePage(ShippingReleaseViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}