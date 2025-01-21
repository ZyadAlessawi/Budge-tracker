namespace Note
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();


            Routing.RegisterRoute(nameof(View.InsertNote), typeof(View.InsertNote));
        }
    }
}
