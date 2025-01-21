using Budge_tracker.Welcome;

namespace Budge_tracker.Profile.HelpCenter;

public partial class Help : ContentPage
{
	public Help()
	{
		InitializeComponent();
	}
    private async void Go_To_NotificationPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(NotificationPage)}");

    }

    private async void Go_To_AutoSupportPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(AutoSupportPage)}");
    }

    private async void Go_To_Facebook(object sender, EventArgs e)
    {
        await Launcher.OpenAsync("https://web.facebook.com");
    }
}