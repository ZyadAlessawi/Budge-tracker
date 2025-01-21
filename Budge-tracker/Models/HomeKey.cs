using Budge_tracker.Home;
using Essential_Lib.API;
using Essential_Lib.Icons;
using Microsoft.Maui.Animations;
using Microsoft.Maui.Controls;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budge_tracker.Models
{
    public class Home_Key
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string? Img { get; set; }
        public string? Title { get; set; }
        public decimal TotalExpense { get; set; }

    }
}
