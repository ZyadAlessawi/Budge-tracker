using Budge_tracker.Welcome;
using Essential_Lib.Extensions;
using Plugin.Maui.Biometric;

namespace Budge_tracker.Profile.Security;

public partial class SecurityPage : ContentPage
{
	public SecurityPage()
	{
		InitializeComponent();
	}


    private async void Go_To_NotificationPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(NotificationPage)}");

    }

    private async void Go_To_Change_PinPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(Change_pinPage)}");

    }

    private void Go_To_FingerPage(object sender, EventArgs e)
    {
       // await Shell.Current.GoToAsync($"/{nameof(FingerPage)}");
       bottomSheet.Show();

    }

    private async void Go_To_TermsPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(TermsPage)}");


    }

    private async void Remove_Your_Fingerprint(object sender, EventArgs e)
    {
        var finger = await SecureStorage.GetAsync("UserFingerprint");
        if (finger != null)
        {
            var remove = await DisplayAlert("Remove", "Are You Sure You Want To Delete The Fingerprint ?", "Yes", "No");

            if (remove == true)
            {
                var result = await BiometricAuthenticationService.Default.AuthenticateAsync(new AuthenticationRequest()
                {
                    Title = "Please Authenticate To Increment",
                    NegativeText = "Cancel Authentication"
                }, CancellationToken.None);

                if (result.Status == BiometricResponseStatus.Success)
                {
                    SecureStorage.Default.Remove("UserFingerprint");
                    await DisplayExtensions.DisplayToastAsync("Removed successfully");
                }
                else
                {
                    await DisplayExtensions.DisplayToastAsync("Failed Registration");
                    App.Current.Quit();
                }

            }
        }
        else
        {
            await DisplayAlert("Invalid Fingerprint", "There Is No Fingerprint", "Ok");
        }

    }

    private async void Add_Fingerprint(object sender, EventArgs e)
    {
        // await Shell.Current.GoToAsync($"/{nameof(FingerprintPage)}");
        var finger = await SecureStorage.GetAsync("UserFingerprint");
        if (finger == null)
        {
            var result = await BiometricAuthenticationService.Default.AuthenticateAsync(new AuthenticationRequest()
            {
                Title = "Please Authenticate To Increment",
                NegativeText = "Cancel Authentication"
            }, CancellationToken.None);

            if (result.Status == BiometricResponseStatus.Success)
            {
                var UserFingerprint = $"Fingerprint:{result}";
                await SecureStorage.SetAsync("UserFingerprint", UserFingerprint);

                await DisplayExtensions.DisplayToastAsync("Successful Registration");

            }
            else
            {
                await DisplayAlert("Wrong", "Failed Registration", "Ok");
            }
        }
        else
        {
            await DisplayAlert("Wrong", "There Is a Registered Fingerprint", "Ok");
        }


    }

}