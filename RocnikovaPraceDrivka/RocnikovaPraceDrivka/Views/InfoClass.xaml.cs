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

		//

		public InfoClass(Class cls)
		{
			InitializeComponent();
			this.cls = cls;

		}

		private void DayNight_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			ChangeLightMode();
		}

		protected override void OnAppearing()
		{
			NameLabel.BindingContext = cls;
			DescLabel.BindingContext = cls;

			StudentsCountSpan.Text = cls.Students.List.Count.ToString();
			LessonsWeeklyCountSpan.Text = cls.Lessons.List.Count.ToString();

			if (cls.Students.List.Count != 1)
				EndStudentsSpan.Text = "s";
			else
				EndStudentsSpan.Text = string.Empty;

			if (cls.Lessons.List.Count != 1)
				EndHoursSpan.Text = "s";
			else
				EndHoursSpan.Text = string.Empty;

			Students.ItemsSource = cls.Students.List;
			Lessons.ItemsSource = cls.Lessons.List;

			if (cls.Students.List.Count == 0)
				EditStudentsButton.IsVisible = false;
			else
				EditStudentsButton.IsVisible = true;

			if (cls.Lessons.List.Count == 0)
				EditLessonsButton.IsVisible = false;
			else
				EditLessonsButton.IsVisible = true;

			Students.SelectionMode = SelectionMode.Single;
			Lessons.SelectionMode = SelectionMode.Single;

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
				Padding = new Thickness(80, 80, 80, 80),
				BackgroundColor = Color.Transparent,
			};

			AddLessonPopup l = new AddLessonPopup();
			detailsPage.Content = l.Content;
			l.FindByName<Button>("CancelButton").Clicked += ((o2, e2) =>
			{
				Navigation.PopModalAsync();
			});

			l.FindByName<Button>("OkButton").Clicked += (async (o2, e2) =>
			{
				try
				{
					cls.Lessons.Add(AddLesson(l));
				}
				catch (Exception exc)
				{
					await DisplayAlert("Error", exc.ToString(), "OK");
				}
				finally
				{
					await Navigation.PopModalAsync();
				}
			});

			l.FindByName<Entry>("NameEntry").Completed += (async (o2, e2) =>
			{
				try
				{
					cls.Lessons.Add(AddLesson(l));
				}
				catch(Exception exc)
				{
					await DisplayAlert("Error", exc.ToString(), "OK");
				}
				finally
				{
					await Navigation.PopModalAsync();
				}
			});

			await Navigation.PushModalAsync(detailsPage, false);
		}

		private async void AddStudentButton_Clicked(object sender, EventArgs e)
		{
			ContentPage detailsPage = new ContentPage
			{
				Padding = new Thickness(80, 80, 80, 80),
				BackgroundColor = Color.Transparent
			};

			AddStudentPopup l = new AddStudentPopup();
			detailsPage.Content = l.Content;
			l.FindByName<Button>("CancelButton").Clicked += ((o2, e2) =>
			{
				Navigation.PopModalAsync();
			});

			l.FindByName<Button>("OkButton").Clicked += (async (o2,e2) =>
			{
				try
				{
					cls.Students.List.Add(AddStudent(l));
				}
				catch (Exception exc)
				{
					await DisplayAlert("Error", exc.ToString(), "OK");
				}
				finally
				{
					await Navigation.PopModalAsync();
				}
			});

			l.FindByName<Entry>("NameEntry").Completed += (async (o2, e2) =>
			{
				try
				{
					cls.Students.List.Add(AddStudent(l));
				}
				catch (Exception exc)
				{
					await DisplayAlert("Error", exc.ToString(), "OK");
				}
				finally
				{
					await Navigation.PopModalAsync();
				}
			});

			await Navigation.PushModalAsync(detailsPage, false);
		}

		//

		private Lesson AddLesson(AddLessonPopup l)
		{
			Entry nameEntry = l.FindByName<Entry>("NameEntry");
			TimePicker startPicker = l.FindByName<TimePicker>("StartTimePicker");
			TimePicker endPicker = l.FindByName<TimePicker>("EndTimePicker");
			Picker dayPicker = l.FindByName<Picker>("DayPicker");

			if (string.IsNullOrWhiteSpace(nameEntry.Text))
				throw new Exception("Enter name");
			else if (startPicker.Time > endPicker.Time)
				throw new Exception("Beggining of lesson has to be before the end of lesson");
			else if (dayPicker.SelectedItem == null)
				throw new Exception("Choose day of week");
			else if (startPicker.Time < new TimeSpan(7, 0, 0))
				throw new Exception("Choose start of lesson after 7 AM");
			else if (endPicker.Time > new TimeSpan(20, 0, 0))
				throw new Exception("Choose end of lesson before 8 PM");
			else
			{
				int day = dayPicker.SelectedIndex;

				TimeSpan start = startPicker.Time;
				TimeSpan end = endPicker.Time;

				start = new TimeSpan(day, start.Hours, start.Minutes, start.Seconds);
				end = new TimeSpan(day, end.Hours, end.Minutes, end.Seconds);

				EditLessonsButton.IsVisible = true;
				return new Lesson(nameEntry.Text, start, end);
			}
		}

		private Student AddStudent(AddStudentPopup l)
		{
			Entry nameEntry = l.FindByName<Entry>("NameEntry");

			if (string.IsNullOrWhiteSpace(nameEntry.Text))
				throw new Exception("Enter name");
			else
			{ 
				EditStudentsButton.IsVisible = true;

				return new Student(nameEntry.Text);
			}

		}

		private void EditLessonsButton_Clicked(object sender, EventArgs e)
		{
			EditLessonsButton.IsVisible = false;
			EditLessonsCancel.IsVisible = true;
			EditLessonsOk.IsVisible = true;

			Lessons.SelectionMode = SelectionMode.Multiple;
		}

		private void EditStudentsButton_Clicked(object sender, EventArgs e)
		{
			EditStudentsButton.IsVisible = false;
			EditStudentsCancel.IsVisible = true;
			EditStudentsOk.IsVisible = true;

			Students.SelectionMode = SelectionMode.Multiple;
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
				DayNightToolbarItem.IconImageSource = "Day.png";
				ClassImage.Source = "ClassDay.png";

			}
			else
			{
				DayNightToolbarItem.IconImageSource = "Night.png";
				ClassImage.Source = "ClassNight.png";

			}
		}

		private  void EditStudentsOk_Clicked(object sender, EventArgs e)
		{
			EditStudentsCancel.IsVisible = false;
			EditStudentsOk.IsVisible = false;

			List<Student> choosen = new List<Student>();

			foreach (Student s in Students.SelectedItems)
				choosen.Add(s);

			foreach (Student s in choosen)
				cls.Students.Delete(s);

			Students.SelectionMode = SelectionMode.Single;
			Students.SelectedItems = null;

			if (cls.Students.List.Count == 0)
				EditStudentsButton.IsVisible = false;
			else
				EditStudentsButton.IsVisible = true;

		}

		private void EditStudentsCancel_Clicked(object sender, EventArgs e)
		{
			EditStudentsButton.IsVisible = true;
			EditStudentsCancel.IsVisible = false;
			EditStudentsOk.IsVisible = false;

			Students.SelectionMode = SelectionMode.Single;
			Students.SelectedItems = null;
		}

		private void EditLessonsOk_Clicked(object sender, EventArgs e)
		{
			EditLessonsCancel.IsVisible = false;
			EditLessonsOk.IsVisible = false;

			List<Lesson> choosen = new List<Lesson>();

			foreach (Lesson l in Lessons.SelectedItems)
				choosen.Add(l);

			foreach (Lesson l in choosen)
				cls.Lessons.Delete(l);

			Lessons.SelectionMode = SelectionMode.Single;
			Students.SelectedItems = null;

			if (cls.Lessons.List.Count == 0)
				EditLessonsButton.IsVisible = false;
			else
				EditLessonsButton.IsVisible = true;
		}

		private void EditLessonsCancel_Clicked(object sender, EventArgs e)
		{
			EditLessonsButton.IsVisible = true;
			EditLessonsCancel.IsVisible = false;
			EditLessonsOk.IsVisible = false;

			Lessons.SelectionMode = SelectionMode.Single;
			Students.SelectedItems = null;
		}

		private async void Students_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
		{
			if (Students.SelectionMode == SelectionMode.Single)
			{
				ContentPage detailsPage = new ContentPage
				{
					Padding = new Thickness(80, 80, 80, 80),
					BackgroundColor = Color.Transparent
				};

				AddStudentPopup l = new AddStudentPopup();

				l.FindByName<Entry>("NameEntry").Text = ((Student)(sender as CollectionView).SelectedItem).Name;

				detailsPage.Content = l.Content;
				l.FindByName<Button>("CancelButton").Clicked += ((o2, e2) =>
				{
					Navigation.PopModalAsync();
				});

				l.FindByName<Button>("OkButton").Clicked += (async (o2, e2) =>
				{
					try
					{
						cls.Students.List.Add(AddStudent(l));
					}
					catch (Exception exc)
					{
						await DisplayAlert("Error", exc.ToString(), "OK");
					}
					finally
					{
						await Navigation.PopModalAsync();
					}
				});

				l.FindByName<Entry>("NameEntry").Completed += (async (o2, e2) =>
				{
					try
					{
						cls.Students.List.Add(AddStudent(l));
					}
					catch (Exception exc)
					{
						await DisplayAlert("Error", exc.ToString(), "OK");
					}
					finally
					{
						await Navigation.PopModalAsync();
					}
				});

				await Navigation.PushModalAsync(detailsPage, false);
			}
		}

		private async void Lessons_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
		{
			if (Lessons.SelectionMode == SelectionMode.Single)
			{
				ContentPage detailsPage = new ContentPage
				{
					Padding = new Thickness(80, 80, 80, 80),
					BackgroundColor = Color.Transparent,
				};

				AddLessonPopup l = new AddLessonPopup();

				Lesson lesson = (Lesson)(sender as CollectionView).SelectedItem;
				l.FindByName<Entry>("NameEntry").Text = lesson.Name;
				l.FindByName<TimePicker>("StartTimePicker").Time = lesson.Start;
				l.FindByName<TimePicker>("EndTimePicker").Time = lesson.End;
				l.FindByName<Picker>("DayPicker").SelectedIndex = DOW.Names.IndexOf(lesson.Day);

				detailsPage.Content = l.Content;
				l.FindByName<Button>("CancelButton").Clicked += ((o2, e2) =>
				{
					Navigation.PopModalAsync();
				});

				l.FindByName<Button>("OkButton").Clicked += (async (o2, e2) =>
				{
					try
					{
						cls.Lessons.Add(AddLesson(l));
					}
					catch (Exception exc)
					{
						await DisplayAlert("Error", exc.ToString(), "OK");
					}
					finally
					{
						await Navigation.PopModalAsync();
					}
				});

				l.FindByName<Entry>("NameEntry").Completed += (async (o2, e2) =>
				{
					try
					{
						cls.Lessons.Add(AddLesson(l));
					}
					catch (Exception exc)
					{
						await DisplayAlert("Error", exc.ToString(), "OK");
					}
					finally
					{
						await Navigation.PopModalAsync();
					}
				});

				await Navigation.PushModalAsync(detailsPage, false);
			}
		}
	}
}