using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RocnikovaPraceDrivka.Controls;
using RocnikovaPraceDrivka.Popup;
using RocnikovaPraceDrivka.Handles;

namespace RocnikovaPraceDrivka.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InfoClass : ContentPage
	{
		private Class cls;

		public InfoClass(Class cls)
		{
			InitializeComponent();
			this.cls = cls;

		}

		~InfoClass()  // finalizer
		{
		}

		private void DayNight_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			ChangeLightMode();
		}

		protected override void OnAppearing()
		{
			NameLabel.BindingContext = cls;
			DescLabel.BindingContext = cls;

			StudentsCountSpan.Text = cls.StudentsList.Count.ToString();
			LessonsWeeklyCountSpan.Text = cls.LessonsList.Count.ToString();

			Students.ItemsSource = cls.StudentsList;
			Lessons.ItemsSource = cls.LessonsList;

			ChangeLightMode();

			DayNightHandle.DayNight.PropertyChanged += DayNight_PropertyChanged;

			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			DayNightHandle.DayNight.PropertyChanged -= DayNight_PropertyChanged;

			base.OnDisappearing();
		}

		private async void AddLessonButton_Clicked(object sender, EventArgs e)
		{
			ContentPage detailsPage = new ContentPage
			{
				Padding = new Thickness(80, 80, 80, 80)
			};

			AddLessonPopup l = new AddLessonPopup();
			detailsPage.Content = l.Content;
			l.FindByName<Button>("CancelButton").Clicked += ((o2, e2) =>
			{
				Navigation.PopModalAsync();
			});


			l.FindByName<Button>("OkButton").Clicked += ((o2, e2) =>
			{
				AddLesson(l);
			});

			l.FindByName<Entry>("NameEntry").Completed += ((o2, e2) =>
			{
				AddLesson(l);
			});

			await Navigation.PushModalAsync(detailsPage, false);
		}

		private async void AddStudentButton_Clicked(object sender, EventArgs e)
		{
			ContentPage detailsPage = new ContentPage
			{
				Padding = new Thickness(80, 80, 80, 80)
			};

			AddStudentPopup l = new AddStudentPopup();
			detailsPage.Content = l.Content;
			l.FindByName<Button>("CancelButton").Clicked += ((o2, e2) =>
			{
				Navigation.PopModalAsync();
			});

			l.FindByName<Button>("OkButton").Clicked += ((o2,e2) =>
			{
				AddStudent(l);
			});

			l.FindByName<Entry>("NameEntry").Completed += ((o2, e2) =>
			{
				AddStudent(l);
			});

			await Navigation.PushModalAsync(detailsPage, false);
		}

		//

		private async void AddLesson(AddLessonPopup l)
		{
			Entry nameEntry = l.FindByName<Entry>("NameEntry");
			TimePicker startPicker = l.FindByName<TimePicker>("StartTimePicker");
			TimePicker endPicker = l.FindByName<TimePicker>("EndTimePicker");
			Picker dayPicker = l.FindByName<Picker>("DayPicker");

			if (string.IsNullOrWhiteSpace(nameEntry.Text))
				await DisplayAlert("Error", "Enter name", "OK");
			else if (startPicker.Time > endPicker.Time)
				await DisplayAlert("Error", "Beggining of lesson has to be before the end of lesson", "OK");
			else if (dayPicker.SelectedItem == null)
				await DisplayAlert("Error", "Choose day of week", "OK");
			else
			{
				DateTime start = new DateTime();
				DateTime end = new DateTime();

				int day = dayPicker.SelectedIndex;

				start.AddSeconds(startPicker.Time.TotalSeconds);
				end.AddSeconds(endPicker.Time.TotalSeconds);

				start.AddDays(day);
				end.AddDays(day);

				cls.LessonsList.Add(new Lesson(nameEntry.Text, start, end));

				await Navigation.PopModalAsync();
			}
		}

		private async void AddStudent(AddStudentPopup l)
		{
			Entry nameEntry = l.FindByName<Entry>("NameEntry");

			if (string.IsNullOrWhiteSpace(nameEntry.Text))
				await DisplayAlert("Error", "Enter name", "OK");
			else
			{
				cls.StudentsList.Add(new Student(nameEntry.Text));
				await Navigation.PopModalAsync();
			}
		}

		//

		private void DayNightToolbarItem_Clicked(object sender, EventArgs e)
		{
			DayNightHandle.DayNight.Swap();
		}

		private void ChangeLightMode()
		{
			if (DayNightHandle.DayNight.Day)
			{
				DayNightToolbarItem.IconImageSource = "Day.png";
				ClassImage.Source = "ClassDay.png";

			}
			else
			{
				DayNightToolbarItem.IconImageSource = "Night.png";
				ClassImage.Source = "ClassNight.png";


			}
		}

	}
}