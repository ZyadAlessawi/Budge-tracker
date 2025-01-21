using Microsoft.Extensions.Logging;
using Note.View;
using Note.ViewModel;

namespace Note
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

           // builder.Services.AddTransient<InsertNote>();
           // builder.Services.AddTransient<NoteView>();
           // builder.Services.AddTransient<NoteViewModel>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
