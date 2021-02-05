using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RocnikovaPraceDrivka.Controls;

namespace RocnikovaPraceDrivka.Popup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddStudentPopup : ContentPage
	{
		public AddStudentPopup()
		{
			InitializeComponent();

			ChangeLightMode();
		}

		public AddStudentPopup(Student student)
		{
			InitializeComponent();

			NameEntry.Text = student.Name;

			ChangeLightMode();
		}

		//
		protected override void OnAppearing()
		{
			ChangeLightMode();

			Handles.DayNightHandle.DayNight.PropertyChanged += DayNight_PropertyChanged;

			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			Handles.DayNightHandle.DayNight.PropertyChanged -= DayNight_PropertyChanged;

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
			if (Handles.DayNightHandle.DayNight.Day)
			{
				StudentImage.Source = "StudentDay.png";
			}
			else
			{
				StudentImage.Source = "StudentNight.png";
			}
		}
	}
}