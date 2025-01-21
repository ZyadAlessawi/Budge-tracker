namespace Budge_tracker.Welcome;

public partial class WelcomePage : ContentPage
{
	public WelcomePage()
	{
		InitializeComponent();
	}

    private async void Go_To_NextWelcomePage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(NextWelcomePage)}");


    }
}