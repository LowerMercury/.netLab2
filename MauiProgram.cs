using Microsoft.Extensions.Logging;

/*
Name: Dominick Hagedorn - Luke Kastern
Description: Lab 2
Date: 9/23/2024
Bugs: Trash can might not be perfect. A lot of checking for valid data is done in MainPage at the cost of having businessLogic recieve the right data types.
      Sometimes loading the airports from the .txt works perfectly and other times it doesn't?
Reflection: It wasn't too complicated to get the front end to look fine, but hooking it up to the backend took a lot of questionale solutions to get it to work. Also, it
took a long time to debug the program fully and weed out all the little errors.
*/

namespace Lab2
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
