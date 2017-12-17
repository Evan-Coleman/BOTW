using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOTW.Data;
using BOTW.Models;

namespace BOTW.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        #region Fields
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private int _date;
        public int Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }
        private bool _buttonEnabled;
        public bool ButtonEnabled
        {
            get { return _buttonEnabled; }
            set { SetProperty(ref _buttonEnabled, value); }
        }

        private MovieInfo _newMovie;

        public ObservableCollection<MovieInfo> MovieList { get; set; }


        public DelegateCommand AddMessageToListCommand { get; set; }
        public static MovieInfoManager MovieInfoManager { get; private set; }
        #endregion

        #region Constructor
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            ButtonEnabled = true;
            MovieInfoManager = new MovieInfoManager(new RestService());
            Title = "Main Page";
            MovieList = new ObservableCollection<MovieInfo>();
            AddMessageToListCommand = new DelegateCommand(AddMessageToList);
            //PopulateMovieList();
        }
        #endregion


        private async void AddMessageToList()
        {
            ButtonEnabled = false;
            _newMovie = new MovieInfo();
            _newMovie = await MovieInfoManager.GetTasksAsync(Name);
            //MovieList.Add(_newMovie);
            await App.Database.SaveMovieInfoAsync(_newMovie);
            PopulateMovieList();
            ButtonEnabled = true;
        }

        private async void PopulateMovieList()
        {
            List<MovieInfo> Movies = await App.Database.GetMoviesAsync();
            foreach (MovieInfo Movie in Movies)
            {
                MovieList.Add(Movie);
            }
        }



    }
}
