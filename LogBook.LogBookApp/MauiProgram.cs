﻿using LogBook.Lib;
using LogBook.LogBookApp.ViewModels;
using Microsoft.Extensions.Logging;

namespace LogBook.LogBookApp
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
            
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();

            string path = FileSystem.Current.AppDataDirectory;
            string filename = "data.xml";

            string fullpath = System.IO.Path.Combine(path, filename);

            System.Diagnostics.Debug.WriteLine(fullpath);

            builder.Services.AddSingleton<IRepository>(new XmlRepository(fullpath));

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
