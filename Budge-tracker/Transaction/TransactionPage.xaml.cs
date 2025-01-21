using Budge_tracker.Models;
using Budge_tracker.Welcome;
using Essential_Lib.API;
using Essential_Lib.Extensions;
using Essential_Lib.Localization;
using System.Collections.ObjectModel;
using System.Linq;

namespace Budge_tracker.Transaction;

public partial class TransactionPage : ContentPage
{
    List<Add_Expenses_Key> Transactions = [];

    public TransactionPage()
	{
		InitializeComponent();

        shimmeractive.IsVisible = true;
        Appearing += async (s, e) =>
        {
            await GetData();
            //ListIncome.ItemsSource = Transactions;
        };
        shimmeractive.IsVisible = false;
    }

    private async void Go_To_NotificationPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(NotificationPage)}");

    }

    public async Task GetData()
    {
       
        var food = await LocalDatabaseAPIs.GetAllItemsAsync<Add_Food_Key>();

        var Entert = await LocalDatabaseAPIs.GetAllItemsAsync<Add_Entertainment_Key>();

        var Gift = await LocalDatabaseAPIs.GetAllItemsAsync<Add_Gifts_Key>();

        var Grocerie = await LocalDatabaseAPIs.GetAllItemsAsync<Add_Groceries_Key>();


        var Medici = await LocalDatabaseAPIs.GetAllItemsAsync<Add_Medicine_Key>();

        var Rents = await LocalDatabaseAPIs.GetAllItemsAsync<Add_Rent_Key>();

        var Trans = await LocalDatabaseAPIs.GetAllItemsAsync<Add_Transport_Key>();

        var Car = await LocalDatabaseAPIs.GetAllItemsAsync<Add_Car_Key>();


        var House = await LocalDatabaseAPIs.GetAllItemsAsync<Add_NewHouse_Key>();

        var Trav = await LocalDatabaseAPIs.GetAllItemsAsync<Add_Travel_Key>();

        var Wedd = await LocalDatabaseAPIs.GetAllItemsAsync<Add_Wedding_Key>();

        
        var salar = await LocalDatabaseAPIs.GetAllItemsAsync<Add_Salary>();

        var list = new List<Add_Expenses_Key>(food ?? []);
        list.AddRange(Entert ?? []);
        list.AddRange(Gift ?? []);
        list.AddRange(Grocerie ?? []);
        list.AddRange(Medici ?? []);
        list.AddRange(Rents ?? []);
        list.AddRange(Trans ?? []);
        list.AddRange(Car ?? []);
        list.AddRange(House ?? []);
        list.AddRange(Trav ?? []);
        list.AddRange(Wedd ?? []);

        var totalexpenses = list?.Sum(a=> a.AmountPrice);
        ListIncome.ItemsSource = list;
        Label_TotalSalary.Text = $"{totalexpenses :C}";


        //list.AddRange(salar ?? []);
        var listsalar = new List<Add_Expenses_Key>(salar ?? []);
        var totalsalary = listsalar?.Sum(a => a.AmountPrice);
        ListExpenses.ItemsSource = salar;
        Label_TotalExpenses.Text = $"-{totalsalary :C}";


        Transactions = [.. list?.OrderByDescending(a=>a.SelectedDate)];


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
            var itmes = ListIncome.SelectedItems?.Cast<Add_Expenses_Key>()?.ToArray();

            foreach (var item in itmes)
            {
                await item.DeleteFromLocalDataBase();
            }
            await GetData();
            await DisplayExtensions.DisplayToastAsync("All cleaned up!");
        }
    }

}