using Budge_tracker.Log;
using Budge_tracker.Profile.HelpCenter;
using Budge_tracker.Profile.Security;
using Budge_tracker.Profile.Setting;
using Budge_tracker.Welcome;
using Essential_Lib.API;
using Essential_Lib.Icons;

namespace Budge_tracker.Profile;

public partial class ProfilePage : ContentPage
{
	public  ProfilePage()
	{
		InitializeComponent();

        ShowInformation();
    }

    public async void ShowInformation()
    {
        var name = await SecureStorage.GetAsync("UserName");
        lapel_Show_UserName.Text = name;

        var image = await SecureStorage.GetAsync("ProfilePicture");
        if (image != null)
        {
            ImageBox.Source = image;
            //imagefont.Glyph = IconFont.Account;
        }
        else
        {
            imagefont.Glyph = IconFont.Account;
            //ImageBox.Source = "account.png";
        }
    }


    private async void Go_To_NotificationPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(NotificationPage)}");

    }

    private async void Go_To_EditProfilePage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(EditProfilePage)}");
    }

    private async void Go_To_SecurityPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(SecurityPage)}");

    }

    private async void Go_To_SettingPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(SettingPage)}");

    }

    private async void Go_To_HelpPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(Help)}");

    }

    private async void Logout(object sender, EventArgs e)
    {
        var result = await DisplayAlert("End Session", "Are you sure you want to log out?", "yes, end session", "cancel");

        if (result == true)
        {
            SecureStorage.Default.Remove("UserCredintials");

            await Shell.Current.GoToAsync($"{nameof(FirstPage)}");

            //App.Current.Quit();
        }
    }

}