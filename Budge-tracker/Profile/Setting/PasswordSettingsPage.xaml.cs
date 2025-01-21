using Budge_tracker.Welcome;
using Essential_Lib.API;
using Essential_Lib.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Budge_tracker.Profile.Setting;

public partial class PasswordSettingsPage : ContentPage
{
	public PasswordSettingsPage()
	{
		InitializeComponent();
	}

    private async void Go_To_NotificationPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(NotificationPage)}");

    }

    private async void Change_Password(object sender, EventArgs e)
    {
        if(entry_Current_Password.Text != null && entry_New_Password.Text != null && entry_Confirm_New_Password.Text != null)
        {
            var Pass = await SecureStorage.GetAsync("UserPassword");
            if (Pass == entry_Current_Password.Text)
            {
                var strength = ValidatorAPI.ValidatePassword(entry_New_Password.Text);
                if (strength == "Very Strong")
                {

                    if (entry_New_Password.Text == entry_Confirm_New_Password.Text)
                    {
                        SecureStorage.Default.Remove("UserPassword");
                        var UserPassword = entry_New_Password.Text;

                        if (UserPassword != null)
                        {
                            await SecureStorage.SetAsync("UserPassword", UserPassword);
                            await DisplayExtensions.DisplayToastAsync("Changed password Done");
                            entry_New_Password.Text = string.Empty;
                            entry_Confirm_New_Password.Text = string.Empty;
                            entry_Current_Password.Text = string.Empty;
                        }
                        else
                        {
                            await DisplayAlert("Wrong", "Try Again", "Ok");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Wrong", "Confirm New Password Not Equal With New Password", "Ok");
                    }

                }
                else
                {
                    await DisplayAlert("Wrong", "New Password Is Weak", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Wrong", "Current Password Incorrect", "Ok");
            }
        }
        else
        {
            await DisplayAlert("Wrong", "Fill Empty Cells", "Ok");
        }
        
    }

}