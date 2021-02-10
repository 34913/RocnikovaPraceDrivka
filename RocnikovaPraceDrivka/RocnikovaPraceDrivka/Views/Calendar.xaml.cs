using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RocnikovaPraceDrivka.Controls;
using RocnikovaPraceDrivka.Handles;
using RocnikovaPraceDrivka.Popup;

namespace RocnikovaPraceDrivka.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Calendar : ContentPage
	{

		private bool reload = true;

		//

		public Calendar()
		{
			InitializeComponent();

			CalendarControl.cc.PropertyChanged += AllLessons_PropertyChanged;
		}

		//

		protected override void OnAppearing()
		{
			if (reload)
			{
				ListLessons.Children.Clear();
				LoadGridTable();
				reload = false;
			}

			DayNightHandle.DayNight.PropertyChanged += DayNight_PropertyChanged;

			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			DayNightHandle.DayNight.PropertyChanged -= DayNight_PropertyChanged;

			base.OnDisappearing();
		}

		//

		private void AllLessons_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			reload = true;
		}

		private void DayNight_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			ChangeLightMode();
		}

		//

		private void LoadGridTable()
		{
			double multiplier = 2;

			Grid zapati = new Grid
			{
				ColumnSpacing = 0,
			};

			zapati.RowDefinitions.Add(new RowDefinition()
			{
				Height = GridLength.Auto,
			});

			for (int i = 7; i < 21; i++)
			{
				zapati.ColumnDefinitions.Add(new ColumnDefinition
				{
					Width = new GridLength(60 * multiplier, GridUnitType.Absolute)
				});

				Label label = new Label
				{
					Text = i.ToString() + ":00",
				};
				label.SetDynamicResource(Label.TextColorProperty, "Label");
				zapati.Children.Add(label);

				Grid.SetColumn(label, zapati.ColumnDefinitions.Count - 1);
				Grid.SetRow(label, 0);
			}

			//

			Grid mainGrid = new Grid()
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
			};
			zapati.ColumnDefinitions.Add(new ColumnDefinition
			{
				Width = new GridLength(90, GridUnitType.Absolute),
			});
			mainGrid.ColumnDefinitions.Add(new ColumnDefinition
			{
				Width = GridLength.Auto,
			});
			mainGrid.RowDefinitions.Add(new RowDefinition
			{
				Height = GridLength.Auto,
			});

			mainGrid.Children.Add(zapati);
			Grid.SetColumn(zapati, 1);
			Grid.SetRow(zapati, 0);

			List<Grid> gridList = new List<Grid>();
			for (int i = 0; i < DOW.Names.Count; i++) {
				mainGrid.RowDefinitions.Add(new RowDefinition
				{
					Height = new GridLength(1, GridUnitType.Star),
				});

				Label label = new Label
				{
					Text = DOW.Names[i],
				};
				label.SetDynamicResource(Label.TextColorProperty, "Label");
				mainGrid.Children.Add(label);

				Grid.SetColumn(label, 0);
				Grid.SetRow(label, i + 1);

				gridList.Add(new Grid
				{
					ColumnSpacing = 0,
				});
			}

			//

			int dayIndex = 0;
			int lessonsIndex = 0;
			TimeSpan lastEnd = new TimeSpan(7, 0, 0);

			while (lessonsIndex < CalendarControl.cc.List.Count)
			{
				Grid grid = gridList[dayIndex];
				MergedLesson lesson = CalendarControl.cc.List[lessonsIndex];
				TimeSpan startShort = lesson.Start - new TimeSpan(dayIndex, 0, 0, 0);
				TimeSpan endShort = lesson.End - new TimeSpan(dayIndex, 0, 0, 0);
				Label l;
				Frame f;

				if (lesson.Start.Days == dayIndex)
				{
					TimeSpan difference = startShort - lastEnd;

					if (difference.TotalMinutes != 0)
					{
						f = new Frame
						{
							Padding = 0,
							Margin = 0,
							HasShadow = false,
							BackgroundColor = Color.Transparent,
							BorderColor = Color.Transparent,
						};
						grid.Children.Add(f);

						grid.ColumnDefinitions.Add(new ColumnDefinition
						{
							Width = new GridLength(difference.TotalMinutes * multiplier, GridUnitType.Absolute)
						});
						Grid.SetColumn(f, grid.Children.Count - 1);
						Grid.SetRow(f, 0);
					}

					StackLayout sl = new StackLayout
					{
						VerticalOptions = LayoutOptions.Center,
						HorizontalOptions = LayoutOptions.Center,
						Orientation = StackOrientation.Vertical,
						Spacing = 1,
					};

					l = new Label
					{
						Text = lesson.Name,
					};
					l.SetDynamicResource(Label.TextColorProperty, "Label");
					sl.Children.Add(l);

					string text = lesson.OwnerList[0].Name;
					if (lesson.OwnerList.Count > 1)
					{
						if (lesson.OwnerList.Count > 2)
							for (int i = 1; i < lesson.OwnerList.Count - 1; i++)
								text += ", " + lesson.OwnerList[i];
						text += " and " + lesson.OwnerList[lesson.OwnerList.Count - 1];
					}

					l = new Label
					{
						Text = text,
					};
					l.SetDynamicResource(Label.TextColorProperty, "Label");
					sl.Children.Add(l);

					l = new Label
					{
						Text = lesson.LengthMinutes.ToString() + " min",
					};
					l.SetDynamicResource(Label.TextColorProperty, "Label");
					sl.Children.Add(l);

					f = new Frame
					{
						//WidthRequest = 200,
						//HeightRequest = 200,
						BackgroundColor = Color.Transparent,
						BorderColor = Color.White,
						Content = sl,
						Margin = 0,
						Padding = 3,
						HasShadow = false,
						BindingContext = lesson,
					};
					f.SetDynamicResource(Frame.BorderColorProperty, "Border");
					f.SetDynamicResource(Frame.BackgroundColorProperty, "Background");

					TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer
					{
						NumberOfTapsRequired = 1,
					};
					tapGestureRecognizer.Tapped += (s, e) =>
					{
						TapStudents_Tapped(s, e);
					};
					f.GestureRecognizers.Add(tapGestureRecognizer);

					grid.Children.Add(f);

					grid.ColumnDefinitions.Add(new ColumnDefinition
					{
						Width = new GridLength(lesson.LengthMinutes * multiplier, GridUnitType.Absolute)
					});
					Grid.SetColumn(f, grid.Children.Count - 1);
					Grid.SetRow(f, 0);

					//

					lastEnd = endShort;

					lessonsIndex++;
				}
				else
				{
					dayIndex++;
					lastEnd = new TimeSpan(7, 0, 0);
				}
			}

			//

			for(int i = 0; i < gridList.Count; i++)
			{
				mainGrid.Children.Add(gridList[i]);

				Grid.SetColumn(gridList[i], 1);
				Grid.SetRow(gridList[i], i + 1);
			}

			ListLessons.Children.Add(mainGrid);
		}

		private void ChangeLightMode()
		{
			if (DayNightHandle.DayNight.Day)
			{
				
			}
			else
			{

			}
		}

		private async void LessonPop(Lesson lesson)
		{
			ContentPage detailsPage = new ContentPage
			{
				BackgroundColor = Color.Transparent,
			};

			MergedLesson selected = lesson as MergedLesson;

			AddLessonPopup l = new AddLessonPopup(selected);

			Entry nameEntry = l.FindByName<Entry>("NameEntry");
			TimePicker startPicker = l.FindByName<TimePicker>("StartTimePicker");
			TimePicker endPicker = l.FindByName<TimePicker>("EndTimePicker");
			Picker dayPicker = l.FindByName<Picker>("DayPicker");

			l.FindByName<StackLayout>("CreateNewStack").IsVisible = false;
			l.FindByName<StackLayout>("MergeStack").IsVisible = false;

			detailsPage.Content = l.Content;
			l.FindByName<Button>("CancelButton").Clicked += ((o2, e2) =>
			{
				Navigation.PopModalAsync();
			});

			async void action(object o2, EventArgs e2)
			{
				try
				{
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

					int day = dayPicker.SelectedIndex;

					TimeSpan start = startPicker.Time;
					TimeSpan end = endPicker.Time;

					Lesson newL = new Lesson(nameEntry.Text, new TimeSpan(day, start.Hours, start.Minutes, start.Seconds), new TimeSpan(day, end.Hours, end.Minutes, end.Seconds));

					if (!CalendarControl.cc.DontCross(selected, newL))
						throw new Exception("There is another lesson in this time");

					CalendarControl.cc.UpdateLesson(selected, newL);
					for(int i = 0; i < selected.OwnerList.Count; i++)
						selected.OwnerList[i].Lessons.Update(selected, newL);
				}
				catch (Exception exc)
				{
					await DisplayAlert("Error", exc.Message, "OK");
					return;
				}

				LoadGridTable();

				await Navigation.PopModalAsync();
			}

			l.FindByName<Button>("OkButton").Clicked += ((o2, e2) =>
			{
				action(o2, e2);
			});

			nameEntry.Completed += ((o2, e2) =>
			{
				action(o2, e2);
			});

			await Navigation.PushModalAsync(detailsPage, false);
		}

		//

		private void TapStudents_Tapped(object sender, EventArgs e)
		{
			Lesson l = (sender as Frame).BindingContext as MergedLesson;

			LessonPop(l);

			// todo
			// Popup window

		}

	}
}