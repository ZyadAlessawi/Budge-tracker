using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Model
{
    public class Notes
    {
       // [PrimaryKey, AutoIncrement]
       // public int Id { get; set; }
       //
       // public string Title { get; set; }
       // public string Description { get; set; }


        public string Filename { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
