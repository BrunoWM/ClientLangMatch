using ClientLangMatch.Services;
using ClientLangMatch.Views;
using Microsoft.Extensions.Logging;

namespace ClientLangMatch;

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

        builder.Services.AddSingleton<IUserDataService, UserDataService>();
		//builder.Services.AddHttpClient<IUserDataService, UserDataService>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<ManageUserView>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
