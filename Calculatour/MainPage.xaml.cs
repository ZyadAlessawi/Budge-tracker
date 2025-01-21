namespace Calculatour
{
    public partial class MainPage : ContentPage
    {

        public MainPage(Math_Model vm)
        {
            InitializeComponent();

            BindingContext = vm;


            //Math_Model math = new Math_Model();

//#if ANDROID || IOS 
//            var change = new ToolbarItem
//            {
//                Order = ToolbarItemOrder.Secondary,
//                Text = "Change Theme"
//            };
//            change.Clicked += Change_Themese_Clicked;
//            this.ToolbarItems.Add(change);
//#endif 

        }
//#if ANDROID || IOS

        private void Change_Themese_Clicked(object sender, EventArgs e)
        {
            //var currentTheme = Application.Current.UserAppTheme;
            if (Application.Current.UserAppTheme == AppTheme.Dark)
            {
                Application.Current.UserAppTheme = AppTheme.Light;
                //Change_theme.Text = "Dark Mode";

            }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
                // Change_theme.Text = "Light Mode";

            }

        }
//#endif

        private async void HistoryPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new HistoryPage());
        }
    }

}


