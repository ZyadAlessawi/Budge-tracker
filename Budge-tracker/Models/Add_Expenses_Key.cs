using Essential_Lib.API;
using Essential_Lib.Extensions;
using Essential_Lib.Icons;
using SQLite;
namespace Budge_tracker.Models
{
    public abstract class Add_Expenses_Key 
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public virtual long SelectedDate { get; set; }
        public string SelectedPicker { get; set; } = string.Empty;
        public decimal AmountPrice { get; set; }// = string.Empty;
        public virtual string Title { get; set; } = string.Empty;

        public abstract string Image { get; }
        public abstract Task SaveToLocalDataBase();
        public abstract Task DeleteFromLocalDataBase();

        public Add_Expenses_Key? ConvertToChild(string? Key)
        {
            if(Key == null) 
                return null;
            return Key switch
            {
                "Food" => this.Serialize().Deserialize<Add_Food_Key>(),
                "Transport" => this.Serialize().Deserialize<Add_Transport_Key>(),
                "Medicine" => this.Serialize().Deserialize<Add_Medicine_Key>(),
                "Groceries" => this.Serialize().Deserialize<Add_Groceries_Key>(),
                "Rent" => this.Serialize().Deserialize<Add_Rent_Key>(),
                "Gifts" => this.Serialize().Deserialize<Add_Gifts_Key>(),
                "Entertainment" => this.Serialize().Deserialize<Add_Entertainment_Key>(),
                "Wedding" => this.Serialize().Deserialize<Add_Wedding_Key>(),
                "Car" => this.Serialize().Deserialize<Add_Car_Key>(),
                "House" => this.Serialize().Deserialize<Add_NewHouse_Key>(),
                "Travel" => this.Serialize().Deserialize<Add_Travel_Key>(),

                _ => this.Serialize().Deserialize < Add_Salary>()
            };

        }
        public static async Task<IEnumerable<Add_Expenses_Key>?> GetAllItemsFromLocalDB(string? Key)
        {
            if (Key == null)
                return null;
            System.Collections.IEnumerable? list = Key switch
            {
                "Food" => await LocalDatabaseAPIs.GetAllItemsAsync<Add_Food_Key>(),
                "Transport" => await LocalDatabaseAPIs.GetAllItemsAsync<Add_Transport_Key>(),
                "Medicine" => await LocalDatabaseAPIs.GetAllItemsAsync<Add_Medicine_Key>(),
                "Groceries" => await LocalDatabaseAPIs.GetAllItemsAsync<Add_Groceries_Key>(),
                "Rent" => await LocalDatabaseAPIs.GetAllItemsAsync<Add_Rent_Key>(),
                "Gifts" => await LocalDatabaseAPIs.GetAllItemsAsync<Add_Gifts_Key>(),
                "Entertainment" => await LocalDatabaseAPIs.GetAllItemsAsync<Add_Entertainment_Key>(),
                "Wedding" => await LocalDatabaseAPIs.GetAllItemsAsync<Add_Wedding_Key>(),
                "Car" => await LocalDatabaseAPIs.GetAllItemsAsync<Add_Car_Key>(),
                "House" => await LocalDatabaseAPIs.GetAllItemsAsync<Add_NewHouse_Key>(),
                "Travel" => await LocalDatabaseAPIs.GetAllItemsAsync<Add_Travel_Key>(),

                _ => await LocalDatabaseAPIs.GetAllItemsAsync<Add_Salary>()
            };
            return list?.Cast<Add_Expenses_Key>();
        }

    }

    public class Add_Food_Key : Add_Expenses_Key
    {
        public override string Image => IconFont.Food;

        public override async Task DeleteFromLocalDataBase()
        {
            await this.DeleteItemAsync();
        }

        public override async Task SaveToLocalDataBase()
        {
            await this.AddItemAsync();
        }
    }

    public class Add_Transport_Key : Add_Expenses_Key
    {
        public override string Image => IconFont.Bus;
        public override async Task SaveToLocalDataBase()
        {
            await this.AddItemAsync();
        }
        public override async Task DeleteFromLocalDataBase()
        {
            await this.DeleteItemAsync();
        }
    }
    public class Add_Medicine_Key : Add_Expenses_Key
    {
        public override string Image => IconFont.Medication;
        public override async Task SaveToLocalDataBase()
        {
            await this.AddItemAsync();
        }
        public override async Task DeleteFromLocalDataBase()
        {
            await this.DeleteItemAsync();
        }
    }

    public class Add_Groceries_Key : Add_Expenses_Key
    {
        public override string Image => IconFont.Carrot;
        public override async Task SaveToLocalDataBase()
        {
            await this.AddItemAsync();
        }
        public override async Task DeleteFromLocalDataBase()
        {
            await this.DeleteItemAsync();
        }
    }

    public class Add_Rent_Key : Add_Expenses_Key
    {
        public override string Image => IconFont.Key;
        public override async Task SaveToLocalDataBase()
        {
            await this.AddItemAsync();
        }
        public override async Task DeleteFromLocalDataBase()
        {
            await this.DeleteItemAsync();
        }
    }

    public class Add_Gifts_Key : Add_Expenses_Key
    {
        public override string Image => IconFont.Gift;
        public override async Task SaveToLocalDataBase()
        {
            await this.AddItemAsync();
        }
        public override async Task DeleteFromLocalDataBase()
        {
            await this.DeleteItemAsync();
        }
    }
    
    public class Add_Entertainment_Key : Add_Expenses_Key
    {
        public override string Image => IconFont.Ticket;
        public override async Task SaveToLocalDataBase()
        {
            await this.AddItemAsync();
        }
        public override async Task DeleteFromLocalDataBase()
        {
            await this.DeleteItemAsync();
        }
    }
    
    public class Add_Wedding_Key : Add_Expenses_Key
    {
        public override string Image => IconFont.PartyPopper;
        public override async Task SaveToLocalDataBase()
        {
            await this.AddItemAsync();
        }
        public override async Task DeleteFromLocalDataBase()
        {
            await this.DeleteItemAsync();
        }
    }
    
    public class Add_Car_Key : Add_Expenses_Key
    {
        public override string Image => IconFont.Car;
        public override async Task SaveToLocalDataBase()
        {
            await this.AddItemAsync();
        }
        public override async Task DeleteFromLocalDataBase()
        {
            await this.DeleteItemAsync();
        }
    }
    
    public class Add_NewHouse_Key : Add_Expenses_Key
    {
        public override string Image => IconFont.Home;
        public override async Task SaveToLocalDataBase()
        {
            await this.AddItemAsync();
        }
        public override async Task DeleteFromLocalDataBase()
        {
            await this.DeleteItemAsync();
        }
    }
    
    public class Add_Travel_Key : Add_Expenses_Key
    {
        public override string Image => IconFont.Airplane;
        public override async Task SaveToLocalDataBase()
        {
            await this.AddItemAsync();
        }
        public override async Task DeleteFromLocalDataBase()
        {
            await this.DeleteItemAsync();
        }
    }
    [Table("Salaries")]
    public class Add_Salary : Add_Expenses_Key//: ITransaction
    {
        public override string Image => IconFont.Cash;
        public override string Title { get; set; } = "Salary";
        private long selectedDate;

        public override async Task SaveToLocalDataBase()
        {
            await this.AddItemAsync();
        }
        public override async Task DeleteFromLocalDataBase()
        {
            await this.DeleteItemAsync();
        }
        public override long SelectedDate 
        { 
            get
            {
                if (selectedDate == 0)
                    selectedDate = DateTime.Now.Ticks;
                return selectedDate;
            }
            set
            {
                selectedDate = value;
            }
        }
    }
}
