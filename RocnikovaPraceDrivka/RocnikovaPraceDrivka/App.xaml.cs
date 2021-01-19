using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RocnikovaPraceDrivka
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new RocnikovaPraceDrivka.Views.SignForms.SignIn());



			//((NavigationPage)MainPage).BarBackgroundColor = Color.DarkGray;
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		//	Shell.Current.Navigation.PopToRootAsync();
		//	MainPage = new Views.SignForms.SignIn();
		}
	}
}
