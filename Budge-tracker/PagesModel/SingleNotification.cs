
using Essential_Lib.API;
using Plugin.LocalNotification;

namespace Budge_tracker.PagesModel
{
    internal class SingleNotification
    {
        //public SingleNotification(){ }


        public string Title { get; set; }
        public string Message { get; set; }
        public string ActionPage { get; set; }
        public bool IsLocalOnly { get; set; }
        public int NotificationType { get; set; }
        public long NotificationDateTime { get; set; }
        public string ImageSourcePath { get; set; }
        public string IconGylph { get; set; }
        public string SubTitle { get; set; }
        public Dictionary<string, object> ActionPageParamaters { get; set; }
   
}
    public class Notification
    {
        public static async Task Notify(string title, string subtitle = "", string descreption = "", string Actionpage = @"///ProductsRoute", Dictionary<string, object>? ActionPageParamaters = null, string? ImageSourcepath = null, string? Icongylph = null, bool IsLocalonly = true)
        {
#if ANDROID || IOS

            if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
            {
                await LocalNotificationCenter.Current.RequestNotificationPermission();
            }
            var request = new NotificationRequest
            {
                NotificationId = LocalNotificationCenter.Current.GetDeliveredNotificationList().Result.Count,
                Title = title,
                Subtitle = subtitle,
                Description = descreption,
                BadgeNumber = LocalNotificationCenter.Current.GetPendingNotificationList().Result.Count,
                CategoryType = NotificationCategoryType.Service,
                Image = new NotificationImage() { FilePath = Path.GetFullPath("vectorgreen.png") },

                //Group = "ProductNotifications",
                //Schedule = new NotificationRequestSchedule
                //{
                //    NotifyTime = DateTime.Now,
                //} 

            };

            if (await LocalNotificationCenter.Current.AreNotificationsEnabled())
            {
                await LocalNotificationCenter.Current.Show(request);
            }

#endif
            var notification = new SingleNotification()
            {
                Title = title,
                Message = descreption,
                ActionPage = Actionpage,
                IsLocalOnly = IsLocalonly,
                //NotificationType = (int)types,
                NotificationDateTime = DateTime.UtcNow.Ticks,
                ImageSourcePath = ImageSourcepath,
                IconGylph = Icongylph,
                SubTitle = subtitle,
                ActionPageParamaters = ActionPageParamaters,
            };
            await notification.AddItemAsync();
        }

    }
}