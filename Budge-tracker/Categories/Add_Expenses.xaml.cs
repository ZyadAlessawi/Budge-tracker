using Budge_tracker.Models;
using Budge_tracker.Welcome;
using Essential_Lib.Extensions;

namespace Budge_tracker.Categories;

public partial class Add_Expenses : ContentPage
{

    public Add_Expenses()
	{
		InitializeComponent();
    }

    public async void HandelAddExpenses()
    {
        Add_Expenses_Key expenses_Key = new Add_Food_Key()
        {
            SelectedDate = Selected_Date_Picker.Date.Ticks,
            SelectedPicker = (string)Picker_Selected_Category.SelectedItem,
            AmountPrice = decimal.TryParse(entry_Amount_Price.Text, out decimal p) ? p : 0,
            Title = entry_Expense_Title.Text,
        };
        var ex = expenses_Key.ConvertToChild(Picker_Selected_Category.SelectedItem?.ToString());
        if(ex != null)
        {
            await ex.SaveToLocalDataBase();
            await Shell.Current.GoToAsync("..");
        }
    }

    private async void Go_To_NotificationPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(NotificationPage)}");

    }

    private async void Save_Expenses(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(entry_Amount_Price.Text))
        {
            if(!string.IsNullOrWhiteSpace(entry_Expense_Title.Text))
            {
                HandelAddExpenses();
                await DisplayExtensions.DisplayToastAsync("Saved Expense");

                entry_Amount_Price.Text = string.Empty;
                entry_Expense_Title.Text = string.Empty;
                Picker_Selected_Category.SelectedItem = "Select the category";
                Selected_Date_Picker.Date = DateTime.Now;
            }
            else
            {
                await DisplayAlert("Wrong", "Enter Title", "ok");
            }
        }
        else
        {
            await DisplayAlert("Wrong", "Enter Price", "ok");
        }
    }

}