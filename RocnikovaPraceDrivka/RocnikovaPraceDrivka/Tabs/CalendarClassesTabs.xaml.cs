using RocnikovaPraceDrivka.Controls;
using RocnikovaPraceDrivka.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RocnikovaPraceDrivka.Handles;

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

		//
		protected override void OnAppearing()
		{
			ChangeLightMode();

			DayNightHandle.DayNight.PropertyChanged += DayNight_PropertyChanged;

			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			DayNightHandle.DayNight.PropertyChanged -= DayNight_PropertyChanged;

			base.OnDisappearing();
		}

		//

		private void DayNight_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			ChangeLightMode();
		}

		private void InfoToolbarItem_Clicked(object sender, EventArgs e)
		{

		}

		private void DayNightToolbarItem_Clicked(object sender, EventArgs e)
		{
			DayNightHandle.DayNight.Swap();
		}



		private void ChangeLightMode()
		{
			if (DayNightHandle.DayNight.Day)
			{
				InfoToolbarItem.IconImageSource = "InfoDay.png";
				DayNightToolbarItem.IconImageSource = "Day.png";

				BarBackgroundColor = Color.White;
				BarTextColor = Color.Black;
			}
			else
			{
				InfoToolbarItem.IconImageSource = "InfoNight.png";
				DayNightToolbarItem.IconImageSource = "Night.png";

				BarBackgroundColor = Color.Black;
				BarTextColor = Color.White;
			}
		}
	}
}