using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RocnikovaPraceDrivka.Handles;
using RocnikovaPraceDrivka.Controls;

namespace RocnikovaPraceDrivka.Popup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddLessonPopup : ContentPage
	{
		public AddLessonPopup()
		{
			InitializeComponent();

			DayPicker.ItemsSource = DOW.Names;

			StartTimePicker.Time = new TimeSpan(7, 0, 0);
			EndTimePicker.Time = new TimeSpan(20, 0, 0);

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
				LessonImage.Source = "LessonDay.png";
			}
			else
			{
				LessonImage.Source = "LessonNight.png";
			}
		}

		private void ClassPicker_SelectedIndexChanged(object sender, EventArgs e)
		{
			LessonPicker.ItemsSource = (sender as Class).Lessons.List;
		}

		private void LessonPicker_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}