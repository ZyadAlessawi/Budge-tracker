using Budge_tracker.Models;
using Budge_tracker.Welcome;
using Essential_Lib.API;

namespace Budge_tracker.Profile.Setting;

public partial class DeleteAccountPage : ContentPage
{
	public DeleteAccountPage()
	{
		InitializeComponent();
	}

    private async void Go_To_NotificationPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(NotificationPage)}");

    }

    private async void Delete_account(object sender, EventArgs e)
    {
        var pass = await SecureStorage.GetAsync("UserPassword");
        if (entry_delete_account.Text == pass)
        {
           var result = await DisplayAlert("Delete account", "By deleting your account, you agree that you understand the consequences of this action and that you agree to permanently delete your account and all associated data. ", "yes, Delete account", "cancel");
          
           if (result == true)
           {
               await LocalDatabaseAPIs.DeleteTableAsync<SignupKeys>();
               await LocalDatabaseAPIs.DeleteTableAsync<Add_Food_Key>();
               await LocalDatabaseAPIs.DeleteTableAsync<Add_Car_Key>();
               await LocalDatabaseAPIs.DeleteTableAsync<Add_Entertainment_Key>();
               await LocalDatabaseAPIs.DeleteTableAsync<Add_Gifts_Key>();
               await LocalDatabaseAPIs.DeleteTableAsync<Add_Groceries_Key>();
               await LocalDatabaseAPIs.DeleteTableAsync<Add_Medicine_Key>();
               await LocalDatabaseAPIs.DeleteTableAsync<Add_NewHouse_Key>();
               await LocalDatabaseAPIs.DeleteTableAsync<Add_Rent_Key>();
               await LocalDatabaseAPIs.DeleteTableAsync<Add_Salary>();
               await LocalDatabaseAPIs.DeleteTableAsync<Add_Transport_Key>();
               await LocalDatabaseAPIs.DeleteTableAsync<Add_Travel_Key>();
               await LocalDatabaseAPIs.DeleteTableAsync<Add_Wedding_Key>();
           
               SecureStorage.Default.Remove("UserCredintials");
               SecureStorage.Default.Remove("UserPassword");
               SecureStorage.Default.Remove("UserEmail");
               SecureStorage.Default.Remove("UserName");
               SecureStorage.Default.Remove("UserFingerprint");
               SecureStorage.Default.Remove("ProfilePicture");

                await Shell.Current.GoToAsync("//first");
           }
        }
        else
        {
            await DisplayAlert("Wrong", "Your Password Incorrect", "Ok");
        }
    }

    private async void Go_To_Settings(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(SettingPage)}");

    }

}