using Budget_Tracker.Models;
using Budget_Tracker.PageModels;

namespace Budget_Tracker.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}