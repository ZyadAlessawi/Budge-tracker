using Budge_tracker.Welcome;

namespace Budge_tracker.Profile.Security;

public partial class TermsPage : ContentPage
{
	public TermsPage()
	{
		InitializeComponent();
	}

    private async void Go_To_SecurityPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(SecurityPage)}");

    }


    private async void Go_To_NotificationPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(NotificationPage)}");

    }
}