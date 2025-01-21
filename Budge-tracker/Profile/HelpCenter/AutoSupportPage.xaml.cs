using Budge_tracker.PagesModel;
using Budge_tracker.Welcome;
using Syncfusion.Maui.Chat;

namespace Budge_tracker.Profile.HelpCenter;

public partial class AutoSupportPage : ContentPage
{
    Auto_Support Support;
    public AutoSupportPage()
	{
		InitializeComponent();

        Support = BindingContext as Auto_Support;
    }

    private async void Go_To_NotificationPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(NotificationPage)}");

    }


    private void Send(object sender, EventArgs e)
    {

        if (chatText.Text != null)
        {
            Support.Messages.Add(new TextMessage()
            {
                Author = Support.CurrentUser,
                Text = chatText.Text,
            });

            if (chatText.Text == "1")
            {
                Support.Messages.Add(new TextMessage()
                {
                    Author = Support.AutoSupp,
                    Text = "1 - Answer 1.",
                });
            }
            else if (chatText.Text == "2")
            {
                Support.Messages.Add(new TextMessage()
                {
                    Author = Support.AutoSupp,
                    Text = "2 - Answer 2.",
                });
            }
            else if (chatText.Text == "3")
            {
                Support.Messages.Add(new TextMessage()
                {
                    Author = Support.AutoSupp,
                    Text = "3 - Answer 3.",
                });
            }
            else if (chatText.Text == "4")
            {
                Support.Messages.Add(new TextMessage()
                {
                    Author = Support.AutoSupp,
                    Text = "4 - Answer 4.",
                });
            }
            else
            {
                Support.Messages.Add(new TextMessage()
                {
                    Author = Support.AutoSupp,
                    Text = "Write Question Number"
                });
            }
            this.chatText.ClearValue(Entry.TextProperty);
        }
        else
        {
            Support.Messages.Add(new TextMessage()
            {
                Author = Support.AutoSupp,
                Text = "Write Question Number "
            });
        }

    }
}

