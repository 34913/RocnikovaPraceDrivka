using RocnikovaPraceDrivka.Controls;
using RocnikovaPraceDrivka.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RocnikovaPraceDrivka.Tabs
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalendarClassesTabs : TabbedPage
	{
		public CalendarClassesTabs(User user)
		{
			InitializeComponent();

			Title = user.Name;

			Children.Add(new Calendar(user));
			Children.Add(new ClassesList(user));

		}

		private void InfoToolbarItem_Clicked(object sender, EventArgs e)
		{

		}

		private void DayNightToolbarItem_Clicked(object sender, EventArgs e)
		{
			if (Handles.DayNightHandle.Day)
				Handles.DayNightHandle.Night = true;
			else
				Handles.DayNightHandle.Day = true;
			ChangeLightMode();
		}

		private void ChangeLightMode()
		{
			if (Handles.DayNightHandle.Day)
			{
				var navigationPage = Application.Current.MainPage as NavigationPage;
				navigationPage.BarBackgroundColor = Color.White;

				BarBackgroundColor = Color.White;
				BarTextColor = Color.Black;
			}
			else
			{
				var navigationPage = Application.Current.MainPage as NavigationPage;
				navigationPage.BarBackgroundColor = Color.Black;

				BarBackgroundColor = Color.Black;
				BarTextColor = Color.White;
			}
		}
	}
}