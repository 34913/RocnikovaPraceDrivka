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
		private bool changed = false;

		//

		public AddLessonPopup()
		{
			InitializeComponent();

			DayPicker.ItemsSource = DOW.Names;

			StartTimePicker.Time = new TimeSpan(7, 0, 0);
			EndTimePicker.Time = new TimeSpan(20, 0, 0);

			ChangeLightMode();
		}

		public AddLessonPopup(Lesson lesson)
		{
			InitializeComponent();

			DayPicker.ItemsSource = DOW.Names;

			NameEntry.Text = lesson.Name;
			StartTimePicker.Time = lesson.Start - TimeSpan.FromDays(lesson.Start.Days);
			EndTimePicker.Time = lesson.End - TimeSpan.FromDays(lesson.End.Days);
			DayPicker.SelectedIndex = DOW.Names.IndexOf(lesson.Day);

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

		//

		private void ClassPicker_SelectedIndexChanged(object sender, EventArgs e)
		{
			Class cls = ClassPicker.SelectedItem as Class;
			LessonPicker.SelectedItem = null;

			if (cls == null)
				return;
			LessonPicker.ItemsSource = (ClassPicker.SelectedItem as Class).Lessons.List;
		}

		private void LessonPicker_SelectedIndexChanged(object sender, EventArgs e)
		{
			Lesson lesson = LessonPicker.SelectedItem as Lesson;

			if (lesson == null)
				return;
			NameEntry.Text = lesson.Name;
			StartTimePicker.Time = lesson.Start - TimeSpan.FromDays(lesson.Start.Days);
			EndTimePicker.Time = lesson.End - TimeSpan.FromDays(lesson.End.Days);
			DayPicker.SelectedIndex = DOW.Names.IndexOf(lesson.Day);
		}

		private void DayNight_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			ChangeLightMode();
		}

		private void CreateNewCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
		{
			if (changed)
			{
				changed = false;
				return;
			}

			changed = true;

			MergeCheckBox.IsChecked = !MergeCheckBox.IsChecked;

			if (MergeCheckBox.IsChecked)
			{
				New.IsVisible = false;
				Merge.IsVisible = true;
			}
			else
			{
				New.IsVisible = true;
				Merge.IsVisible = false;
			}
		}

		private void MergeCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
		{
			if (changed)
			{
				changed = false;
				return;
			}

			changed = true;

			CreateNewCheckBox.IsChecked = !CreateNewCheckBox.IsChecked;

			if (MergeCheckBox.IsChecked)
			{
				New.IsVisible = false;
				Merge.IsVisible = true;
			}
			else
			{
				New.IsVisible = true;
				Merge.IsVisible = false;
			}
		}
	
	}
}