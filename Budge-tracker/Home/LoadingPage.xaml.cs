using Budge_tracker.Log;
using Essential_Lib.Extensions;
using Plugin.Maui.Biometric;

namespace Budge_tracker.Home;

public partial class LoadingPage : ContentPage
{

    public LoadingPage()
    {
		InitializeComponent();        
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await HandelLogin();
    }
    public async Task HandelLogin()
    {
        activityindicator.IsRunning = true;

        var UserCredintials = await SecureStorage.GetAsync("UserCredintials");

        await Task.Delay(2500);
        if (UserCredintials != null)
        {
#if ANDROID || IOS
            var UserFingerprint = await SecureStorage.GetAsync("UserFingerprint");
            if (UserFingerprint != null)
            {

                var result = await BiometricAuthenticationService.Default.AuthenticateAsync(new AuthenticationRequest()
                {
                    Title = "Please Authenticate To Increment",
                    NegativeText = "Cancel"
                }, CancellationToken.None);

                if (result.Status == BiometricResponseStatus.Success)
                {
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await DisplayExtensions.DisplayToastAsync("Failed Registration");
                }
            }
            else
            {
                await Shell.Current.GoToAsync("..");
            }
#else

                await Shell.Current.GoToAsync("..");
#endif
        }
        else
        {
            await Shell.Current.GoToAsync("..");

            await Shell.Current.GoToAsync($"/{nameof(FirstPage)}");

        }
        activityindicator.IsRunning = false;
    }
   
}