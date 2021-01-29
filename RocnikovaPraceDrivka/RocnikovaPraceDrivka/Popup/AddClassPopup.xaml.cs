using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RocnikovaPraceDrivka.Handles;

namespace RocnikovaPraceDrivka.Popup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddClassPopup : ContentPage
	{
		public AddClassPopup()
		{
			InitializeComponent();

			ChangeLightMode();
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

		//

		private void ChangeLightMode()
		{
			if (DayNightHandle.DayNight.Day)
			{
				ClassImage.Source = "ClassDay.png";
			}
			else
			{
				ClassImage.Source = "ClassNight.png";
			}
		}

	}
}