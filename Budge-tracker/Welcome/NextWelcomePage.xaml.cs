
namespace Budge_tracker.Welcome;

public partial class NextWelcomePage : ContentPage
{
	public NextWelcomePage()
	{
		InitializeComponent();
	}

    private async void Go_To_HomePage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//tabbar");
    }
}