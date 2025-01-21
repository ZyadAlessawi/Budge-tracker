namespace Budge_tracker.Models
{
    public class Analysis_Chart_Key
    {
        public string Name { get; set; }
        public decimal Total{ get; set; }
        public Analysis_Chart_Key(string name, decimal total) 
        {
            Name = name;
            Total = total;
        }
    }

}
