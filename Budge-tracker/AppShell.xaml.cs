using Budge_tracker.Categories;
using Budge_tracker.Home;
using Budge_tracker.Log;
using Budge_tracker.Profile;
using Budge_tracker.Profile.HelpCenter;
using Budge_tracker.Profile.Security;
using Budge_tracker.Profile.Setting;
using Budge_tracker.Transaction;
using Budge_tracker.Welcome;

namespace Budge_tracker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();


            Routing.RegisterRoute(nameof(LoadingPage), typeof(LoadingPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(EditProfilePage), typeof(EditProfilePage));
            Routing.RegisterRoute(nameof(FirstPage), typeof(FirstPage));
            Routing.RegisterRoute(nameof(ForgotPassword), typeof(ForgotPassword));
            Routing.RegisterRoute(nameof(NewPassword), typeof(NewPassword));
            Routing.RegisterRoute(nameof(SecurityPin), typeof(SecurityPin));
            Routing.RegisterRoute(nameof(SignupPage), typeof(SignupPage));
            Routing.RegisterRoute(nameof(WelcomePage), typeof(WelcomePage));
            Routing.RegisterRoute(nameof(NextWelcomePage), typeof(NextWelcomePage));
            Routing.RegisterRoute(nameof(NotificationPage), typeof(NotificationPage));
            Routing.RegisterRoute(nameof(SecurityPage), typeof(SecurityPage));
            Routing.RegisterRoute(nameof(TermsPage), typeof(TermsPage));
            Routing.RegisterRoute(nameof(Change_pinPage), typeof(Change_pinPage));
            Routing.RegisterRoute(nameof(SettingPage), typeof(SettingPage));
            Routing.RegisterRoute(nameof(DeleteAccountPage), typeof(DeleteAccountPage));
            Routing.RegisterRoute(nameof(NotificationSettingsPage), typeof(NotificationSettingsPage));
            Routing.RegisterRoute(nameof(PasswordSettingsPage), typeof(PasswordSettingsPage));
            Routing.RegisterRoute(nameof(Help), typeof(Help));
            Routing.RegisterRoute(nameof(AutoSupportPage), typeof(AutoSupportPage));
            Routing.RegisterRoute(nameof(Add_Expenses), typeof(Add_Expenses));
            Routing.RegisterRoute(nameof(MorePage), typeof(MorePage));
            Routing.RegisterRoute(nameof(ExpensePage), typeof(ExpensePage));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await GoToAsync($"{nameof(LoadingPage)}");
        }


    }
}
