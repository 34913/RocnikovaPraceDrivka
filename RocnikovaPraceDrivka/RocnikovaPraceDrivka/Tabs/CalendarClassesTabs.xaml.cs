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
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace RocnikovaPraceDrivka.Tabs
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalendarClassesTabs : TabbedPage
	{
		public CalendarClassesTabs()
		{
			InitializeComponent();

			Title = User.u.Name;

			Children.Add(new Calendar());
			Children.Add(new ClassesList());
		}

		//
		
		protected override void OnAppearing()
		{
			ChangeLightMode();

			DayNightHandle.DayNight.PropertyChanged += DayNight_PropertyChanged;
			Title = User.u.Name;

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

		private async void InfoToolbarItem_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new UserInfo());
		}

		private void DayNightToolbarItem_Clicked(object sender, EventArgs e)
		{
			DayNightHandle.DayNight.Swap();
		}

		//

		private void ChangeLightMode()
		{
			if (DayNightHandle.DayNight.Day)
			{
				InfoToolbarItem.IconImageSource = "InfoDay.png";
				DayNightToolbarItem.IconImageSource = "Day.png";
			}
			else
			{
				InfoToolbarItem.IconImageSource = "InfoNight.png";
				DayNightToolbarItem.IconImageSource = "Night.png";
			}
		}
	}
}