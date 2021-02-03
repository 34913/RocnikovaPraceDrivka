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
		//private User user;

		private Class cls;

		private Draw draw;

		//

		public InfoClass(Class cls)
		{
			InitializeComponent();
			this.cls = cls;

			draw = new Draw();
		}

		private void DayNight_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			ChangeLightMode();
		}

		protected override void OnAppearing()
		{
			NameLabel.BindingContext = cls;
			DescLabel.BindingContext = cls;

			RaloadCountLabel();

			Students.ItemsSource = cls.Students.List;
			Lessons.ItemsSource = cls.Lessons.List;
			
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

		private void AddLessonButton_Clicked(object sender, EventArgs e)
		{
			LessonPop(true);
		}

		private void AddStudentButton_Clicked(object sender, EventArgs e)
		{
			StudentPop(true);
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

			if (DrawStack.IsVisible)
				DrawBox(true);
		}

		private void RaloadCountLabel()
		{
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

			if (cls.Students.List.Count == 0)
				EditStudentsButton.IsVisible = false;
			else
				EditStudentsButton.IsVisible = true;

			if (cls.Lessons.List.Count == 0)
				EditLessonsButton.IsVisible = false;
			else
				EditLessonsButton.IsVisible = true;

			EditStudentsOk.IsVisible = false;
			EditLessonsOk.IsVisible = false;
			EditStudentsCancel.IsVisible = false;
			EditLessonsCancel.IsVisible = false;
		}

		private void DrawBox(bool visible)
		{
			Students.ItemTemplate = new DataTemplate(() =>
			{
				Frame f = new Frame
				{
					Padding = 0,
					Margin = 0,
					HasShadow = false,
				};

				TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer
				{
					NumberOfTapsRequired = 1,
				};
				tapGestureRecognizer.Tapped += (s, e) =>
				{
					TapStudents_Tapped(s, e);
				};
				f.GestureRecognizers.Add(tapGestureRecognizer);

				Grid g;
				if (visible)
				{
					g = new Grid
					{
						ColumnDefinitions = new ColumnDefinitionCollection
						{
							new ColumnDefinition
							{
								Width = new GridLength(2, GridUnitType.Star),
							},
							new ColumnDefinition
							{
								Width = new GridLength(1, GridUnitType.Star),
							},
						},
					};
				}
				else
				{
					g = new Grid
					{
						ColumnDefinitions = new ColumnDefinitionCollection
						{
							new ColumnDefinition
							{
								Width = new GridLength(2, GridUnitType.Star),
							},
						},
					};
				}

				Label l = new Label
				{
					HorizontalTextAlignment = TextAlignment.Center,
				};
				l.SetBinding(Label.TextProperty, "Name");
				l.SetDynamicResource(Label.TextColorProperty, "Label");

				g.Children.Add(l);
				Grid.SetColumn(l, 0);

				if (visible)
				{
					Frame square = new Frame
					{
						WidthRequest = 20,
						HeightRequest = 20,
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions = LayoutOptions.Center,
						CornerRadius = 1,
						Padding = 0,
						Margin = 0,
						HasShadow = false,
					};
					square.SetDynamicResource(Frame.BorderColorProperty, "Border");

					DataTrigger dataTrigger = new DataTrigger(typeof(Frame))
					{
						Binding = new Binding("OneDraw"),
						Value = true
					};
					dataTrigger.Setters.Add(new Setter
					{
						Property = Frame.BackgroundColorProperty,
						Value = App.Current.Resources["Border"],
					});
					square.Triggers.Add(dataTrigger);

					dataTrigger = new DataTrigger(typeof(Frame))
					{
						Binding = new Binding("OneDraw"),
						Value = false
					};
					dataTrigger.Setters.Add(new Setter
					{
						Property = Frame.BackgroundColorProperty,
						Value = App.Current.Resources["Background"],
					});
					square.Triggers.Add(dataTrigger);

					g.Children.Add(square);
					Grid.SetColumn(square, 1);
				}

				f.Content = g;

				return f;
			});

			Students.ItemsSource = null;
			Students.ItemsSource = cls.Students.List;
		}

		private void StudentViewReset()
		{
			EditStudentsCancel.IsVisible = false;
			EditStudentsOk.IsVisible = false;
			if (cls.Students.List.Count == 0)
				EditStudentsButton.IsVisible = false;
			else
				EditStudentsButton.IsVisible = true;

			Students.SelectionMode = SelectionMode.Single;
			Students.SelectedItems = null;

			StudentsCountSpan.Text = cls.Students.List.Count.ToString();
			if (cls.Students.List.Count != 1)
				EndStudentsSpan.Text = "s";
			else
				EndStudentsSpan.Text = string.Empty;
		}

		private void LessonViewReset()
		{
			EditLessonsCancel.IsVisible = false;
			EditLessonsOk.IsVisible = false;
			if (cls.Lessons.List.Count == 0)
				EditLessonsButton.IsVisible = false;
			else
				EditLessonsButton.IsVisible = true;

			Lessons.SelectionMode = SelectionMode.Single;
			Lessons.SelectedItems = null;

			LessonsWeeklyCountSpan.Text = cls.Lessons.List.Count.ToString();
			if (cls.Lessons.List.Count != 1)
				EndHoursSpan.Text = "s";
			else
				EndHoursSpan.Text = string.Empty;
		}

		private async void StudentPop(bool nStudent)
		{
			ContentPage detailsPage = new ContentPage
			{
				Padding = new Thickness(80, 80, 80, 80),
				BackgroundColor = Color.Transparent
			};

			AddStudentPopup l = new AddStudentPopup();
			detailsPage.Content = l.Content;

			Student item = null;
			if (!nStudent)
			{
				item = Students.SelectedItem as Student;

				l.FindByName<Entry>("NameEntry").Text = (Students.SelectedItem as Student).Name;
			}

			l.FindByName<Button>("CancelButton").Clicked += ((o2, e2) =>
			{
				Navigation.PopModalAsync();
			});

			async void action(object o2, EventArgs e2)
			{
				Student s = null;
				try
				{
					s = AddStudent(l);
				}
				catch (Exception exc)
				{
					await DisplayAlert("Error", exc.Message, "OK");
					return;
				}

				if (nStudent)
					cls.Students.Add(s);
				else
					cls.Students.Update(item, s);

				await Navigation.PopModalAsync();
			}

			l.FindByName<Button>("OkButton").Clicked += ((o2, e2) =>
			{
				action(o2, e2);
			});

			l.FindByName<Entry>("NameEntry").Completed += ((o2, e2) =>
			{
				action(o2, e2);
			});

			StudentViewReset();

			await Navigation.PushModalAsync(detailsPage, false);
		}

		private async void LessonPop(bool nLesson)
		{
			ContentPage detailsPage = new ContentPage
			{
				Padding = new Thickness(80, 80, 80, 80),
				BackgroundColor = Color.Transparent,
			};

			AddLessonPopup l = new AddLessonPopup();

			Lesson item = null;
			if (!nLesson)
			{
				item = Lessons.SelectedItem as Lesson;

				l.FindByName<Entry>("NameEntry").Text = item.Name;
				l.FindByName<TimePicker>("StartTimePicker").Time = item.Start;
				l.FindByName<TimePicker>("EndTimePicker").Time = item.End;
				l.FindByName<Picker>("DayPicker").SelectedIndex = DOW.Names.IndexOf(item.Day);
			}

			detailsPage.Content = l.Content;
			l.FindByName<Button>("CancelButton").Clicked += ((o2, e2) =>
			{
				Navigation.PopModalAsync();
			});

			async void action(object o2, EventArgs e2)
			{
				Lesson newL = null;
				try
				{
					newL = AddLesson(l);
				}
				catch (Exception exc)
				{
					await DisplayAlert("Error", exc.Message, "OK");
					return;
				}

				if (nLesson)
				{
					cls.Lessons.Add(newL);
					CalendarControl.AllLessons.AddLesson(new IndexedLesson(newL, cls));
				}
				else
					cls.Lessons.Update(item, newL);

				await Navigation.PopModalAsync();
			}

			l.FindByName<Button>("OkButton").Clicked += ((o2, e2) =>
			{
				action(o2, e2);
			});

			l.FindByName<Entry>("NameEntry").Completed += ((o2, e2) =>
			{
				action(o2, e2);
			});

			LessonViewReset();

			await Navigation.PushModalAsync(detailsPage, false);
		}

		//

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
		
		private  void EditStudentsOk_Clicked(object sender, EventArgs e)
		{
			List<Student> choosen = new List<Student>();

			foreach (Student s in Students.SelectedItems)
				choosen.Add(s);

			foreach (Student s in choosen)
				cls.Students.Delete(s);

			StudentViewReset();
		}

		private void EditStudentsCancel_Clicked(object sender, EventArgs e)
		{
			StudentViewReset();
		}

		private void EditLessonsOk_Clicked(object sender, EventArgs e)
		{

			List<Lesson> choosen = new List<Lesson>();

			foreach (Lesson l in Lessons.SelectedItems)
				choosen.Add(l);

			foreach (Lesson l in choosen)
				cls.Lessons.Delete(l);

			LessonViewReset();	
		}

		private void EditLessonsCancel_Clicked(object sender, EventArgs e)
		{
			LessonViewReset();
		}

		//

		private void TapStudents_Tapped(object sender, EventArgs e)
		{
			if (Students.SelectionMode == SelectionMode.Single)
			{
				StudentPop(false);
				Students.SelectedItem = null;
			}
		}

		private void TapLessons_Tapped(object sender, EventArgs e)
		{
			if (Lessons.SelectionMode == SelectionMode.Single)
			{
				LessonPop(false);
				Lessons.SelectedItem = null;
			}
		}

		//
		private void DrawButton_Clicked(object sender, EventArgs e)
		{
			if (cls.Students.List.Count > 0)
			{
				if (draw.Next(cls.Students.List.ToList()))
				{
					StudentDrawLabel.Text = draw.ChoosenStudent.Name;

					OkDrawButton.IsVisible = true;
				}
			}
		}

		private void OverrideDrawButton_Clicked(object sender, EventArgs e)
		{
			if (cls.Students.List.Count > 0)
			{
				if (draw.Next(cls.Students.List.ToList()))
				{
					StudentDrawLabel.Text = draw.ChoosenStudent.Name;

					OkDrawButton.IsVisible = true;

					draw.Null();
				}
			}
		}

		private async void DrawResetButton_Clicked(object sender, EventArgs e)
		{
			if (await DisplayAlert("Confirm", "Are you sure you want to reset the \"draw state\" of all students in this class?", "OK", "Cancel"))
			{
				foreach (Student s in cls.Students.List)
					s.NullDraw();
				draw.Null();

				StudentDrawLabel.Text = string.Empty;

				Students.ItemsSource = null;
				Students.ItemsSource = cls.Students.List;
			}
		}

		private void OkDrawButton_Clicked(object sender, EventArgs e)
		{
			if (draw.ChoosenStudent != null)
			{
				draw.ChoosenStudent.Draw++;
				draw.Null();

				Students.ItemsSource = null;
				Students.ItemsSource = cls.Students.List;
			}

			OkDrawButton.IsVisible = false;
			StudentDrawLabel.Text = string.Empty;

		}

		//
		
		private void TapDrawGestureRecognizer_Tapped(object sender, EventArgs e)
		{
			DrawStack.IsVisible = !DrawStack.IsVisible;

			DrawBox(DrawStack.IsVisible);

			OkDrawButton.IsVisible = false;
			StudentDrawLabel.Text = string.Empty;
		}

		private void SwipeDownGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
		{
			DrawStack.IsVisible = false;
			DrawBox(false);
			OkDrawButton.IsVisible = false;
		}

		private void SwipeUpGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
		{
			DrawStack.IsVisible = true;
			DrawBox(true);
			OkDrawButton.IsVisible = false;
		}

	}
}