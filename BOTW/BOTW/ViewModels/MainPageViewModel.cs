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
using BOTW.Extensions;

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
        readonly INavigationService _navigationService;
        DelegateCommand<MovieInfo> _movieSelectedCommand;
        public DelegateCommand<MovieInfo> MovieSelectedCommand => _movieSelectedCommand != null ? _movieSelectedCommand : (_movieSelectedCommand = new DelegateCommand<MovieInfo>(MovieSelected));

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
            PopulateMovieList();
            _navigationService = navigationService;
        }
        #endregion


        private async void AddMessageToList()
        {
            ButtonEnabled = false;
            _newMovie = new MovieInfo();
            _newMovie = await MovieInfoManager.GetTasksAsync(Name);
            // Add movie to memory collection
            //MovieList.Add(_newMovie);
            // Add movie to SQLite database
            await App.Database.SaveMovieInfoAsync(_newMovie);
            PopulateMovieList();
            ButtonEnabled = true;
        }

        private async void MovieSelected(MovieInfo movie)
        {
            var p = new NavigationParameters();
            p.Add("MovieInfo", movie);
            await _navigationService.NavigateAsync("MovieDetailPage", p);
        }

        private async void PopulateMovieList()
        {
            var data = await App.Database.GetMoviesAsync();

            foreach(var item in data)
            {
                MovieList.Add(item);
            }
            //MovieList = data.ToObservableCollection();
        }
    }
}
