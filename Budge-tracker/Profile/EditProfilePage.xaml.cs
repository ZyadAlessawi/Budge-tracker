using Budge_tracker.Welcome;
using Essential_Lib.API;
using Essential_Lib.Extensions;
using Essential_Lib.Icons;

namespace Budge_tracker.Profile;

public partial class EditProfilePage : ContentPage
{
    public EditProfilePage()
    {
        InitializeComponent();
        Showinformation();
    }

    public async void Showinformation()                                                    
    {
        var image = await SecureStorage.GetAsync("ProfilePicture");
        if(image != null)
        {
            ImageBox.Source = image;
        }
        else
        {
            imagefont.Glyph = IconFont.Account;
            //ImageBox.Source = "account.png";
        }


        entry_Show_UserName.Text = string.Empty;
        var username = await SecureStorage.GetAsync("UserName");
        entry_Show_UserName.Text = username;


        entry_Show_Email.Text = string.Empty;
        var useremail = await SecureStorage.GetAsync("UserEmail");
        entry_Show_Email.Text = useremail;


        entry_Show_PhoneNumber.Text = string.Empty;
        var phone = await SecureStorage.GetAsync("PhoneNumber");
        entry_Show_PhoneNumber.Text = phone;


        if (Application.Current.UserAppTheme == AppTheme.Dark)
        {
            switch_toggled.IsToggled = true;
        }
        else
        {
            switch_toggled.IsToggled = false;

        }
    }

    private async void Go_To_NotificationPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(NotificationPage)}");
    }

    private async void Change_Image(object sender, EventArgs e)
    {
        var file = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.macOS, new[] {"public.image"} },
                { DevicePlatform.iOS, new[] {"public.image"} },
                { DevicePlatform.Android, new[] {"image/*"} },
                { DevicePlatform.Tizen, new[] {"*/*"} },
                { DevicePlatform.WinUI, new[] {".jpg", ".png", ".gif", ".bmp"} },
            })
        });
        if (file != null)
        {
            var image = await SecureStorage.GetAsync("ProfilePicture");
            if (image != null)
            {
                if (file.FullPath != image)
                {
                    SecureStorage.Default.Remove("ProfilePicture");
                    await SecureStorage.SetAsync("ProfilePicture", file.FullPath);
                    ImageBox.Source = file.FullPath;
                }
                else
                {
                    ImageBox.Source = image;
                }
            }
            else
            {
                await SecureStorage.SetAsync("ProfilePicture", file.FullPath);
                ImageBox.Source = file.FullPath;
            }

        }
    }

    private async void Edit_Profile_information(object sender, EventArgs e)
    {
        shimmeractive.IsVisible = true;
        var remove = await DisplayAlert("Update", "Are You Sure You Want To Edit Information ?", "Yes", "No");

        if (remove == true)
        {
            if (!string.IsNullOrWhiteSpace(entry_Show_UserName.Text) && !string.IsNullOrWhiteSpace(entry_Show_Email.Text) && !string.IsNullOrWhiteSpace(entry_Show_PhoneNumber.Text))
            {
                var result = ValidatorAPI.ValidateEmail(entry_Show_Email.Text);
                if (result == "Email Accepted")
                {
                    lapel_Email_Address.HasError = false;

                    var phone = ValidatorAPI.ValidatePhoneNumber(entry_Show_PhoneNumber.Text);

                    if (phone == "Phone Number Accepted")
                    {
                        lapel_Phone.HasError = false;

                        SecureStorage.Default.Remove("UserName");
                        await SecureStorage.SetAsync("UserName", entry_Show_UserName.Text);

                        SecureStorage.Default.Remove("UserEmail");
                        await SecureStorage.SetAsync("UserEmail", entry_Show_Email.Text);

                        SecureStorage.Default.Remove("PhoneNumber");
                        await SecureStorage.SetAsync("PhoneNumber", entry_Show_PhoneNumber.Text);

                        await DisplayExtensions.DisplayToastAsync("Updated Information");

                        Showinformation();
                    }
                    else
                    {
                        lapel_Phone.HasError = true;
                        lapel_Phone.ErrorText = $"{result}";
                    }
                }
                else
                {
                    lapel_Email_Address.HasError = true;
                    lapel_Email_Address.ErrorText = $"{result}";
                }
            }
            else
            {
                await DisplayAlert("Invalid information", "There Is No Information", "Ok");
            }
        }
        shimmeractive.IsVisible = false;

    }

    private void Switch_push_notifications(object sender, ToggledEventArgs e)
    {

    }

    private void Switch_Turn_dark_Theme(object sender, ToggledEventArgs e)
    {
        if (Application.Current.UserAppTheme == AppTheme.Dark)
        {
            Application.Current.UserAppTheme = AppTheme.Light;
        }
        else
        {
            Application.Current.UserAppTheme = AppTheme.Dark;

        }
    }

}