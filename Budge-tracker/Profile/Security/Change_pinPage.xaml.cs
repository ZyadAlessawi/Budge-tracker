using Budge_tracker.Welcome;

namespace Budge_tracker.Profile.Security;

public partial class Change_pinPage : ContentPage
{
	public Change_pinPage()
	{
		InitializeComponent();
	}

    private async void Change_Pin(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(ProfilePage)}");

    }

    private async void Go_To_NotificationPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(NotificationPage)}");

    }

}