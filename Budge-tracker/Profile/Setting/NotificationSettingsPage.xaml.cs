using Budge_tracker.Welcome;

namespace Budge_tracker.Profile.Setting;

public partial class NotificationSettingsPage : ContentPage
{
	public NotificationSettingsPage()
	{
		InitializeComponent();
	}

    private async void Go_To_NotificationPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(NotificationPage)}");

    }

}