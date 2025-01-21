using Budge_tracker.Models;
using Budge_tracker.PagesModel;
using Essential_Lib.API;
using Essential_Lib.Localization;

namespace Budge_tracker.Analysis;

public partial class AnalysisPage : ContentPage
{
	public AnalysisPage()
	{
        InitializeComponent();

        Appearing += async (s, e) =>
        {
            await GetData();
        };

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

        var totalexpenses = list?.Sum(a => a.AmountPrice);

        var sum = new Analysis_Chart_Key("Expenses".Localize(), totalexpenses ?? 0);

        list.AddRange(salar ?? []);

        var totalsalary = salar?.Sum(a => a.AmountPrice);


        var sumsalary = new Analysis_Chart_Key("Salary".Localize(), totalsalary ?? 0);

        columnSeries.ItemsSource = new Analysis_Chart_Key[] { sum, sumsalary };
    }

    private async void Go_BackButton(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//homepage");

    }

    private async void Go_To_NotificationPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//notification");
    }




}