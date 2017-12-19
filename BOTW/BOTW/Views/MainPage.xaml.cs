using BOTW.Models;
using BOTW.ViewModels;
using Xamarin.Forms;

namespace BOTW.Views
{
    public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent ();
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs args)
        {
            ((MainPageViewModel)this.BindingContext).MovieSelectedCommand.Execute((MovieInfo)args.Item);
        }
    }
}