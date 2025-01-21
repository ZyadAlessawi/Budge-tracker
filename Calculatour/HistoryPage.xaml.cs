using Essential_Lib.API;

namespace Calculatour;

public partial class HistoryPage : ContentPage
{
    public   HistoryPage()
	{
		InitializeComponent();

        //Server ser = new Server();
        //var sql = ser.GetItems();
        //Keys key = new Keys();

        GetData();

        
    }


    public   async void GetData()
    {
       // await LocalDatabaseAPIs.DeleteTableAsync<Keys>();

        var sql = await LocalDatabaseAPIs.GetAllItemsAsync<Keys>();


        ListHistory.ItemsSource = sql;

    }

    private async void Back_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    } 


    private async void Delete_Clicked(object sender, EventArgs e)
    {
        await LocalDatabaseAPIs.DeleteTableAsync<Keys>();
    }

}