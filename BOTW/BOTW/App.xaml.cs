using BOTW.Data;
using BOTW.ViewModels;
using BOTW.Views;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BOTW
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        private static MovieInfoDatabase database;


        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<MovieDetailPage>();
            Container.RegisterTypeForNavigation<EditMovieDetailPage>();
        }

        public static MovieInfoDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new MovieInfoDatabase(DependencyService.Get<IFileHelper>().GetPath("TodoSQLite.db3"));
                }
                return database;
            }
        }
    }
}
