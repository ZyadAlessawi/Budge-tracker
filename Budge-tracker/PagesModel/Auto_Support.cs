using Essential_Lib.Icons;
using Syncfusion.Maui.Chat;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Budge_tracker.PagesModel
{

    public  class Auto_Support //:ObservableObject , INotifyPropertyChanged
    {
        public ObservableCollection<object> Messages {  get; set; }

        public Author CurrentUser {  get; set; }

        public Author AutoSupp {  get; set; }


        public Auto_Support()
        {
            Messages = new ObservableCollection<object>();
            CurrentUser = new Author() { Name = "Me", Avatar = "account.png" };
            AutoSupp = new Author() { Name = "Support", Avatar = IconFont.FaceAgent };
            HandelMessage();
        }
        public void HandelMessage()
        {
            
            Messages.Add(new TextMessage()
            {
                Author = AutoSupp,
                Text = "How Can We Help You ?"
            });
            Messages.Add(new TextMessage()
            {
                Author = AutoSupp,
                Text = "Write Question Number"
            });
            Messages.Add(new TextMessage()
            {
                Author = AutoSupp,
                Text = "1- Question 1 ?"
            });
            Messages.Add(new TextMessage()
            {
                Author = AutoSupp,
                Text = "2- Question 2 ?"
            });
            Messages.Add(new TextMessage()
            {
                Author = AutoSupp,
                Text = "3- Question 3 ?"
            });
            Messages.Add(new TextMessage()
            {
                Author = AutoSupp,
                Text = "4- Question 4 ?"
            });
        }


    }
}
