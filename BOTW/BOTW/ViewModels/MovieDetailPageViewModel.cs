using BOTW.Models;
using Prism.Commands;
using Prism.Navigation;

namespace BOTW.ViewModels
{
    public class MovieDetailPageViewModel : ViewModelBase
	{
        #region Fields
        protected readonly INavigationService _navigationService;

        private MovieInfo _movie;
        public MovieInfo Movie
        {
            get { return _movie; }
            set { SetProperty(ref _movie, value); }
        }

        public DelegateCommand DeleteMovieCommand { get; set; }
        public DelegateCommand EditMovieCommand { get; set; }
        #endregion

        #region Constructor
        public MovieDetailPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            DeleteMovieCommand = new DelegateCommand(DeleteMovie);
            EditMovieCommand = new DelegateCommand(EditMovie);
        }
        #endregion

        #region Methods
        private async void DeleteMovie()
        {
            await App.Database.DeleteMovieInfoAsync(Movie);
            var p = new NavigationParameters();
            p.Add("DeletedMovieInfo", Movie);
            await _navigationService.GoBackToRootAsync(p);


        }

        private async void EditMovie()
        {
            var p = new NavigationParameters();
            p.Add("MovieInfo", Movie);
            await _navigationService.NavigateAsync("EditMovieDetailPage", p);
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("MovieInfo"))
                Movie = (MovieInfo)parameters["MovieInfo"];
        }
        #endregion
    }
}
