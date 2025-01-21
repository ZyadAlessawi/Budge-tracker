using Budge_tracker.Models;
using Essential_Lib.API;
using Plugin.Maui.Biometric;

namespace Budge_tracker
{
    public partial class App : Application
    {
        public App()
        {            
            InitializeComponent();

            Preferences.Set("DBName", "FineWise_App");

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NgoBigBOggjHTQxAR8/V1NAaF1cWWhkYVtpR2Nbe05xdl9CZFZVQGYuP1ZhSXxXdkdjXn9edHNRQGNZVUQ=");
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

    }
}