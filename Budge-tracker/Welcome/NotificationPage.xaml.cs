using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using Plugin.LocalNotification.iOSOption;

namespace Budge_tracker.Welcome;

public partial class NotificationPage : ContentPage
{
	public NotificationPage()
	{
		InitializeComponent();

        LocalNotificationCenter.Current.NotificationActionTapped += Current_NotificationActionTapped;
	}

	private void Current_NotificationActionTapped(Plugin.LocalNotification.EventArgs.NotificationActionEventArgs e)
	{
		if (e.IsDismissed)
		{

		}
		else if (e.IsTapped)
		{

		}
	}

	private void Button_Clicked(object sender, EventArgs e)
    {
		var request = new NotificationRequest
		{
			NotificationId = 1234,
			Title = "Test Title",
			Subtitle = "Hello",
			Description = "Test",
			BadgeNumber = 42,
			Schedule = new NotificationRequestSchedule
			{
				NotifyTime = DateTime.Now,
				NotifyRepeatInterval = TimeSpan.FromDays(1),
			},
			//Android = new AndroidOptions
			//{
			//
			//},
			//iOS = new iOSOptions
			//{
			//
			//}
		};
		LocalNotificationCenter.Current.Show(request);
    }
}