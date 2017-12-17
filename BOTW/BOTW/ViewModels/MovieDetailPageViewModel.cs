using BOTW.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BOTW.ViewModels
{
	public class MovieDetailPageViewModel : ViewModelBase
	{
        readonly INavigationService _navigationService;
        private MovieInfo _movie;
        public MovieInfo Movie
        {
            get { return _movie; }
            set { SetProperty(ref _movie, value); }
        }

        public MovieDetailPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
        }


        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("MovieInfo"))
                Movie = (MovieInfo)parameters["MovieInfo"];
        }
    }
}
