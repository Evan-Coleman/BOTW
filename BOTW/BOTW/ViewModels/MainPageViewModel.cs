using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using BOTW.Data;
using BOTW.Models;
using BOTW.Extensions;

namespace BOTW.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        #region Fields
        protected readonly INavigationService _navigationService;

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
        private ObservableCollection<MovieInfo> _movieList;
        public ObservableCollection<MovieInfo> MovieList
        {
            get { return _movieList; }
            set { SetProperty(ref _movieList, value); }
        }
        private MovieInfo _newMovie;
        public static MovieInfoManager MovieInfoManager { get; private set; }

        public DelegateCommand AddMessageToListCommand { get; set; }
        public DelegateCommand<MovieInfo> _movieSelectedCommand;
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

        #region Methods
        private async void AddMessageToList()
        {
            ButtonEnabled = false;
            _newMovie = new MovieInfo();
            _newMovie = await MovieInfoManager.GetTasksAsync(Name);
            // Add movie to SQLite database
            await App.Database.SaveMovieInfoAsync(_newMovie);
            // Add movie to memory collection
            MovieList.Add(_newMovie);
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
            MovieList = data.ToObservableCollection();
        }

        public async override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("DeletedMovieInfo"))
            {
                MovieList.Remove((MovieInfo)parameters["DeletedMovieInfo"]);
            }
            if (parameters.ContainsKey("EditedMovieInfo"))
            {
                MovieList.Remove((MovieInfo)parameters["UpdatedMovieInfo"]);
                MovieList.Add((MovieInfo)parameters["UpdatedMovieInfo"]);
            }
        }
        #endregion
    }
}
