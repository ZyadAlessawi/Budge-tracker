using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculatour
{
    public class Keys
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string calchistory { get; set; }

        public decimal resulthistory { get; set; }
    }
}
