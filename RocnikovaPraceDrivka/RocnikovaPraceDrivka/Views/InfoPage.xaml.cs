using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RocnikovaPraceDrivka.Handles;

namespace RocnikovaPraceDrivka.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InfoPage : ContentPage
	{
		public InfoPage()
		{
			InitializeComponent();
		}

		//

		protected override void OnAppearing()
		{ 
			var born = new DateTime(2002, 4, 19);

			int age = DateTime.Now.Year - born.Year;
			if (DateTime.Now < born.AddYears(age))
				age--;

			AgeSpan.Text = age.ToString();

			ImageAuthor.Source = "InfoAuthor.png";

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

		private void DayNightToolbarItem_Clicked(object sender, EventArgs e)
		{
			DayNightHandle.DayNight.Swap();
		}

		private void ChangeLightMode()
		{
			if (DayNightHandle.DayNight.Day)
			{
				DayNightToolbarItem.IconImageSource = "Day.png";
			}
			else
			{
				DayNightToolbarItem.IconImageSource = "Night.png";
			}
		}
	}
}