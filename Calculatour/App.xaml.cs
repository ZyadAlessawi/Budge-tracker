namespace Calculatour
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            Preferences.Set("DBName", "CalculatourApp");
        }
    }
}
