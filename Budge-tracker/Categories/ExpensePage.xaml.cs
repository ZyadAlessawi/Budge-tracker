using Budge_tracker.Models;
using Budge_tracker.Welcome;
using Essential_Lib.Extensions;
using Essential_Lib.Localization;
using System.Data;

namespace Budge_tracker.Categories;


public partial class ExpensePage : ContentPage, IQueryAttributable
{
    private string expenseType;
    public ExpensePage()
	{
		InitializeComponent();

        shimmeractive.IsVisible = true;
        Appearing += async (s, e) =>
        {
            await GetData();

        };
        shimmeractive.IsVisible = false;
    }

    public string ExpenseType
    {
        get => expenseType;
        set
        {
            expenseType = value;
            OnPropertyChanged();
        }
        
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("ExpenseType", out object value) && value is string type)
        {
            ExpenseType = type;
            Title = type.Localize();
        }

    }
    public async Task GetData()
    {
        var Cars = await Add_Expenses_Key.GetAllItemsFromLocalDB(ExpenseType);
        ListCarData.ItemsSource = Cars;
        Label_TotalPrice.Text = $"-{Cars?.Sum(a => a.AmountPrice):C}";
    }

    private async void Go_To_NotificationPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(NotificationPage)}");

    }

    private async void Go_To_Expenses(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(Add_Expenses)}");

    }

    private async void RefreshView_Refreshing(object sender, EventArgs e)
    {
        ((RefreshView)sender).IsRefreshing = true;
        shimmeractive.IsVisible = true;
        await GetData();
        shimmeractive.IsVisible = false;
        ((RefreshView)sender).IsRefreshing = false;
    }


    private async void Clean_Button(object sender, EventArgs e)
    {
        if (IsVisible == true)
        {
            var itmes = ListCarData.SelectedItems?.Cast<Add_Expenses_Key>()?.ToArray();

            foreach (var item in itmes)
            {
                await item.DeleteFromLocalDataBase();
            }
            await GetData();
            await DisplayExtensions.DisplayToastAsync("All cleaned up!");
        }
    }
}