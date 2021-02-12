using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RocnikovaPraceDrivka.Handles;
using RocnikovaPraceDrivka.Controls;

namespace RocnikovaPraceDrivka.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserInfo : ContentPage
	{
		// 

		public UserInfo()
		{
			InitializeComponent();
		}

		//

		private void ChangeLightMode()
		{
			if (DayNightHandle.DayNight.Day)
			{
				UserImage.Source = "UserDay.png";
				DayNightToolbarItem.IconImageSource = "Day.png";
			}
			else
			{
				UserImage.Source = "UserNight2.png";
				DayNightToolbarItem.IconImageSource = "Night.png";
			}
		}

		//

		protected override void OnAppearing()
		{
			DayNightHandle.DayNight.PropertyChanged += DayNight_PropertyChanged;

			MainStack.BindingContext = User.u;

			ChangeLightMode();
			Title = "About user";

			ClassesCount.Text = User.u.Classes.List.Count.ToString();

			int lessonsCount = 0;
			int studentsCount = 0;
			for(int i = 0; i < User.u.Classes.List.Count; i++)
			{
				lessonsCount += User.u.Classes.List[i].Lessons.List.Count;
				studentsCount += User.u.Classes.List[i].Students.List.Count;
			}

			LessonsCount.Text = lessonsCount.ToString();
			StudentsCount.Text = studentsCount.ToString();

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

		private void NameEntry_TextChanged(object sender, TextChangedEventArgs e)
		{
			User.u.Name = (sender as Entry).Text;
		}


		//

	}
}