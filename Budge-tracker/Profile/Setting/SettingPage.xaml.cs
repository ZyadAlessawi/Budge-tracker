using Budge_tracker.Welcome;

namespace Budge_tracker.Profile.Setting;

public partial class SettingPage : ContentPage
{
	public SettingPage()
	{
		InitializeComponent();
	}


    private async void Go_To_NotificationPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(NotificationPage)}");

    }

    private async void Go_To_NotificationSettingsPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(NotificationSettingsPage)}");

    }

    private async void Go_To_PasswordSettingsPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(PasswordSettingsPage)}");

    }

    private async void Go_To_DeleteAccountPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(DeleteAccountPage)}");

    }

}