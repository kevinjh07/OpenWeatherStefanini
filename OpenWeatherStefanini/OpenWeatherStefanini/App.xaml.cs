﻿using Prism;
using Prism.Ioc;
using OpenWeatherStefanini.ViewModels;
using OpenWeatherStefanini.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OpenWeatherStefanini.Utils;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace OpenWeatherStefanini
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        public double NamedFontSize {
            get { return Device.GetNamedSize(NamedSize.Small, typeof(Label)); }
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync($"NavigationPage/{PageName.MainPage}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<CitiesPage, CitiesPageViewModel>();
            containerRegistry.RegisterForNavigation<CityDetailsPage, CityDetailsPageViewModel>();
        }
    }
}
