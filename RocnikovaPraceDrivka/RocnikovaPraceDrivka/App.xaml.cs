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

			Handles.DayNightHandle.DayNight = new Handles.DayNightHandle();

			Handles.DayNightHandle.DayNight.PropertyChanged += DayNight_PropertyChanged;

			MainPage = new NavigationPage(new Views.SignIn());

			if (Handles.DayNightHandle.DayNight.Day)
				SwapTheme();
			else
				NaviBarTheme();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}

		// 

		private static void DayNight_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			SwapTheme();
		}

		public static void SwapTheme()
		{
			if (App.Current.Resources is Themes.LightTheme)
				App.Current.Resources = new Themes.DarkTheme(); // needs using DarkMode.Styles;
			else
				App.Current.Resources = new Themes.LightTheme();

			NaviBarTheme();
		}

		public static void NaviBarTheme()
		{
			var navigationPage = Application.Current.MainPage as NavigationPage;
			if (Handles.DayNightHandle.DayNight.Day)
			{
				navigationPage.BarBackgroundColor = Color.White;
				navigationPage.BarTextColor = Color.Black;
			}
			else
			{
				navigationPage.BarBackgroundColor = Color.Black;
				navigationPage.BarTextColor = Color.White;
			}
		}
	}
}
