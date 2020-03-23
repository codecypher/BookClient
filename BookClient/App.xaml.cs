using System;
using BookClient.PageModels;
using BookClient.Services;
using BookClient.Validators;
using FluentValidation;
using FreshMvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BookClient
{
    public partial class App : Application
    {
        public static Settings Settings { get; set; }

        public App()
        {
            InitializeComponent();

            // Load resources
            App.Settings = SettingsLoader.ImprovedLoad();

            // Register dependencies.
            FreshIOC.Container.Register<IBookManager, BookManager>();
            FreshIOC.Container.Register<IValidator, BookValidator>();

            // The root page of your application
            var mainPage = new FreshTabbedNavigationContainer();

            mainPage.AddTab<SquareRootPageModel>("SquareRoot", "Calculator.png");
            mainPage.AddTab<FluentPageModel>("Fluent", "Edit.png");
            mainPage.AddTab<BookListPageModel>("BookList", "Contacts.png");

            MainPage = mainPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
