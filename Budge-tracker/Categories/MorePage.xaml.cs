using Budge_tracker.Models;
using Budge_tracker.Welcome;
using Essential_Lib.API;
using Essential_Lib.Localization;

namespace Budge_tracker.Categories;

public partial class MorePage : ContentPage
{
    List<Add_Expenses_Key> Transactions = [];
    public MorePage()
	{
		InitializeComponent();

        Appearing += async (s, e) =>
        {
            await GetData();
        };
    }

    public async Task GetData()
    {

        var Car = await LocalDatabaseAPIs.GetAllItemsAsync<Add_Car_Key>();


        var House = await LocalDatabaseAPIs.GetAllItemsAsync<Add_NewHouse_Key>();

        var Trav = await LocalDatabaseAPIs.GetAllItemsAsync<Add_Travel_Key>();

        var Wedd = await LocalDatabaseAPIs.GetAllItemsAsync<Add_Wedding_Key>();

        var list = new List<Add_Expenses_Key>(Car ?? []);
        var totalCar = list?.Sum(a => a.AmountPrice);
        var sumCar = new Analysis_Chart_Key("Car".Localize(), totalCar ?? 0);

        list.AddRange(House ?? []);
        var totalHouse = House?.Sum(a => a.AmountPrice);
        var sumHouse = new Analysis_Chart_Key("House".Localize(), totalHouse ?? 0);

        list.AddRange(Trav ?? []);
        var totalTrav = Trav?.Sum(a => a.AmountPrice);
        var sumTrav = new Analysis_Chart_Key("Travel".Localize(), totalTrav ?? 0);

        list.AddRange(Wedd ?? []);
        var totalWedd = Wedd?.Sum(a => a.AmountPrice);
        var sumWedd = new Analysis_Chart_Key("Wedding".Localize(), totalWedd ?? 0);

        columnSeries.ItemsSource = new Analysis_Chart_Key[]
        { sumCar, sumHouse, sumTrav, sumWedd};
    }


    private async void Go_To_NotificationPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(NotificationPage)}");
    }
    private async void Go_To_ExpensePage(object sender, EventArgs e)
    {
        if (sender is View view)
        {
            var param = new Dictionary<string, object?>
        {
            { "ExpenseType",view.StyleId}
        };
            await Shell.Current.GoToAsync($"/{nameof(ExpensePage)}", param);
        }


    }

}