using Budge_tracker.Models;
using Budge_tracker.Welcome;
using Essential_Lib.API;
using Essential_Lib.Extensions;
using Essential_Lib.Localization;

namespace Budge_tracker.Categories;

public partial class CategoriesPage : ContentPage
{
    List<Add_Expenses_Key> Transactions = [];
    public CategoriesPage()
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

        var list = new List<Add_Expenses_Key>(food ?? []);
        var totalfood = list?.Sum(a => a.AmountPrice);
        var sumfood = new Analysis_Chart_Key("Food".Localize(), totalfood ?? 0);

        list.AddRange(Entert ?? []);
        var totalentert = Entert?.Sum(a => a.AmountPrice);
        var sumentert = new Analysis_Chart_Key("Entertainment".Localize(), totalentert ?? 0);

        list.AddRange(Gift ?? []);
        var totalgift = Gift?.Sum(a => a.AmountPrice);
        var sumgift = new Analysis_Chart_Key("Gifts".Localize(), totalgift ?? 0);

        list.AddRange(Grocerie ?? []);
        var totalgrocerie = Grocerie?.Sum(a => a.AmountPrice);
        var sumgrocerie = new Analysis_Chart_Key("Groceries".Localize(), totalgrocerie ?? 0);

        list.AddRange(Medici ?? []);
        var totalMedici = Medici?.Sum(a => a.AmountPrice);
        var sumMedici = new Analysis_Chart_Key("Medicine".Localize(), totalMedici ?? 0);

        list.AddRange(Rents ?? []);
        var totalRents = Rents?.Sum(a => a.AmountPrice);
        var sumRents = new Analysis_Chart_Key("Rent".Localize(), totalRents ?? 0);

        list.AddRange(Trans ?? []);
        var totalTrans = Trans?.Sum(a => a.AmountPrice);
        var sumTrans = new Analysis_Chart_Key("Transport".Localize(), totalTrans ?? 0);


        columnSeries.ItemsSource = new Analysis_Chart_Key[] 
        { sumfood, sumentert, sumgift, sumgrocerie, sumMedici, sumRents, sumTrans };
    }

    private async void Go_To_NotificationPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(NotificationPage)}");
    }

    private async void Go_To_ExpensePage(object sender, EventArgs e)
    {
        if(sender is View view)
        {
            var param = new Dictionary<string, object?>
        {
            { "ExpenseType",view.StyleId}
        };
            await Shell.Current.GoToAsync($"/{nameof(ExpensePage)}",param);
        }
        

    }

    private async void Savings_PopUp(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Salary", "What's your Salary?", "Ok", "Cancel", keyboard: Keyboard.Numeric);
       if(result != null)
        {
            Add_Salary add_Salary = new() {  AmountPrice = decimal.Parse(result) };
            if (add_Salary != null)
            {
                await add_Salary.SaveToLocalDataBase();
                await DisplayExtensions.DisplayToastAsync("Saved Salary");
            }
            else
            {
                await DisplayAlert("Wrong", "Enter Your Salary", "Ok");
            }
        }
        
    }

    private async void Go_To_MorePage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(MorePage)}");

    }
}