using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RocnikovaPraceDrivka.Controls;
using RocnikovaPraceDrivka.Handles;

namespace RocnikovaPraceDrivka.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Calendar : ContentPage
	{
		private bool reload = true;
		
		private User user;

		//

		public Calendar(User user)
		{
			InitializeComponent();

			this.user = user;

			ResetCalendarControl();

			CalendarControl.AllLessons.PropertyChanged += AllLessons_PropertyChanged;
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
					Text = i.ToString() + ":00"
				};
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

			while (lessonsIndex < CalendarControl.AllLessons.List.Count)
			{
				Grid grid = gridList[dayIndex];
				Lesson lesson = CalendarControl.AllLessons.List[lessonsIndex];
				TimeSpan startShort = lesson.Start - new TimeSpan(dayIndex, 0, 0, 0);
				TimeSpan endShort = lesson.End - new TimeSpan(dayIndex, 0, 0, 0);
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

					sl.Children.Add(new Label
					{
						Text = lesson.Name,
					});

					string text = string.Empty;
					if (lesson is IndexedLesson)
						text = (lesson as IndexedLesson).Owner.Name;
					else if(lesson is MergedLesson)
					{
						text = (lesson as MergedLesson).OwnerList[0].Name;
						if ((lesson as MergedLesson).OwnerList.Count > 1) {
							for (int i = 1; i < (lesson as MergedLesson).OwnerList.Count - 1; i++)
								text += ", " + (lesson as MergedLesson).OwnerList[i];
							text += " and " + (lesson as MergedLesson).OwnerList[(lesson as MergedLesson).OwnerList.Count - 1];
						}
					}

					sl.Children.Add(new Label
					{
						Text = text,
					});
					sl.Children.Add(new Label
					{
						Text = lesson.LengthMinutes.ToString() + " min",
					});

					f = new Frame
					{
						//WidthRequest = 200,
						//HeightRequest = 200,
						BorderColor = Color.White,
						Content = sl,
						Margin = 0,
						Padding = 3,
						HasShadow = false,
						BindingContext = lesson,
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

		}

		private void ResetCalendarControl()
		{
			CalendarControl.AllLessons = new CalendarControl();

			foreach (Class cls in user.Classes.List)
			{
				foreach(Lesson l in cls.Lessons.List)
				{
					CalendarControl.AllLessons.AddLesson(new IndexedLesson(l, cls));
				}

			}
		}

		//

		private void TapStudents_Tapped(object sender, EventArgs e)
		{
			Lesson l = (sender as Frame).BindingContext as Lesson;

			// todo
			// Popup window

		}

	}
}