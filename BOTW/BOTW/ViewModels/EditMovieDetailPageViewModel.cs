using BOTW.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BOTW.ViewModels
{
	public class EditMovieDetailPageViewModel : ViewModelBase
	{
        readonly INavigationService _navigationService;

        private MovieInfo _movie;
        public MovieInfo Movie
        {
            get { return _movie; }
            set { SetProperty(ref _movie, value); }
        }
        private MovieInfo _preEditMovie;
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public EditMovieDetailPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            SaveCommand = new DelegateCommand(SaveEdit);
            CancelCommand = new DelegateCommand(CancelEdit);
        }


        private async void SaveEdit()
        {
            await App.Database.SaveMovieInfoAsync(Movie);
            var p = new NavigationParameters();
            p.Add("EditedMovieInfo", _preEditMovie);
            p.Add("UpdatedMovieInfo", Movie);
            await _navigationService.GoBackToRootAsync(p);
        }

        private async void CancelEdit()
        {
            await _navigationService.GoBackAsync();
        }


        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("MovieInfo"))
            {
                Movie = new MovieInfo();
                Movie = (MovieInfo)parameters["MovieInfo"];
                _preEditMovie = new MovieInfo();
                _preEditMovie.ID = Movie.ID;
                _preEditMovie.Name = Movie.Name;
                _preEditMovie.ReleaseDate = Movie.ReleaseDate;
                _preEditMovie.MovieDescription = Movie.MovieDescription;
                _preEditMovie.PosterPath = Movie.PosterPath;
                _preEditMovie.VoteCount = Movie.VoteCount;
                _preEditMovie.VoteAverage = Movie.VoteAverage;
            }
        }
    }
}
