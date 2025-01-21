using Budge_tracker.Models;
using Budge_tracker.Welcome;
using CommunityToolkit.Mvvm.ComponentModel;
using Essential_Lib.API;
using Essential_Lib.Icons;
using Essential_Lib.Localization;
using LiveChartsCore.Drawing;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;

namespace Budge_tracker.Home;

public partial class HomePage : ContentPage
{
    public HomePage()
	{
        InitializeComponent();

        shimmeractive.IsVisible = true;
        Appearing += async (s, e) =>
        {
            await GetData();
        };
        shimmeractive.IsVisible = false;

    }
    public async void SetType<T>(List<T> name, string title, string img, decimal amount)
    {
        if (name?.Count != 0)
        {
            for (int i = name.Count - 1; i < name?.Count; i++)
            {
                var item = name[i];
                if (item != null)
                {
                    Home_Key homeKey = new()
                    {
                        Img = img,
                        Title = title,
                        TotalExpense = amount
                    };

                    var Home = await LocalDatabaseAPIs.GetAllItemsAsync<Home_Key>();

                    var filter = Home?.Where(x => x.Title.StartsWith(title)).ToList();
                    
                    if (filter?.Count == 0 )
                        await homeKey.AddItemAsync();
                    else
                    {
                        var itmes = filter?.Cast<Home_Key>()?.ToArray();
                       

                        foreach (var S in itmes)
                        {
                            await S.DeleteItemAsync();
                        }
                        await homeKey.AddItemAsync();
                    }
                     
                    cv.ItemsSource = Home;
                }
            }
        }
    }
    private async void Go_To_NotificationPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(NotificationPage)}");
    }

    public async Task GetData()
    {

        var food = await LocalDatabaseAPIs.GetAllItemsAsync<Add_Food_Key>();
        SetType(food, "Food", IconFont.Food, food.Sum(a => a.AmountPrice));

        var Entert = await LocalDatabaseAPIs.GetAllItemsAsync<Add_Entertainment_Key>();
        SetType(Entert, "Entertainment", IconFont.Ticket, Entert.Sum(a => a.AmountPrice));

        var Gift = await LocalDatabaseAPIs.GetAllItemsAsync<Add_Gifts_Key>();
        SetType(Gift, "Gifts", IconFont.Gift, Gift.Sum(a => a.AmountPrice));

        var Grocerie = await LocalDatabaseAPIs.GetAllItemsAsync<Add_Groceries_Key>();
        SetType(Grocerie, "Groceries", IconFont.Carrot, Grocerie.Sum(a => a.AmountPrice));

        var Medici = await LocalDatabaseAPIs.GetAllItemsAsync<Add_Medicine_Key>();
        SetType(Medici, "Medicine", IconFont.Medication, Medici.Sum(a => a.AmountPrice));

        var Rents = await LocalDatabaseAPIs.GetAllItemsAsync<Add_Rent_Key>();
        SetType(Rents, "Rent", IconFont.Key, Rents.Sum(a => a.AmountPrice));

        var Trans = await LocalDatabaseAPIs.GetAllItemsAsync<Add_Transport_Key>();
        SetType(Trans, "Transport", IconFont.Bus, Trans.Sum(a => a.AmountPrice));

        var Car = await LocalDatabaseAPIs.GetAllItemsAsync<Add_Car_Key>();
        SetType(Car, "Car", IconFont.Car, Car.Sum(a => a.AmountPrice));

        var House = await LocalDatabaseAPIs.GetAllItemsAsync<Add_NewHouse_Key>();
        SetType(House, "House", IconFont.Home, House.Sum(a => a.AmountPrice));

        var Trav = await LocalDatabaseAPIs.GetAllItemsAsync<Add_Travel_Key>();
        SetType(Trav, "Travel", IconFont.Airplane, Trav.Sum(a => a.AmountPrice));

        var Wedd = await LocalDatabaseAPIs.GetAllItemsAsync<Add_Wedding_Key>();
        SetType(Wedd, "Wedding", IconFont.PartyPopper, Wedd.Sum(a => a.AmountPrice));

        var salar = await LocalDatabaseAPIs.GetAllItemsAsync<Add_Salary>();
        SetType(salar, "Salary", IconFont.Cash, salar.Sum(a => a.AmountPrice));

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
        list.AddRange(salar ?? []);

        var totalexpenses = list?.Sum(a => a.AmountPrice);
        Label_TotalExpenses.Text = $"-{totalexpenses:C}";


        //list.AddRange(salar ?? []);
        var totalsalary = salar?.Sum(a => a.AmountPrice);

        Label_TotalSalary.Text = $"{totalsalary:C}";

    }



    private async void RefreshView_Refreshing(object sender, EventArgs e)
    {
        ((RefreshView)sender).IsRefreshing = true;
        shimmeractive.IsVisible = true;
        await GetData();
        shimmeractive.IsVisible = false;
        ((RefreshView)sender).IsRefreshing = false;
    }
}