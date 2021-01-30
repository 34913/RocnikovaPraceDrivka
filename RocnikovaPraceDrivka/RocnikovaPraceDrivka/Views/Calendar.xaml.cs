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
		private User user;

		//List<Lesson> weekly = new List<Lesson>();

		//

		public Calendar(User user)
		{
			InitializeComponent();
			this.user = user;
		}

		//

		protected override void OnAppearing()
		{
			LoadGridTable();

			DayNightHandle.DayNight.PropertyChanged += DayNight_PropertyChanged;

			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			DayNightHandle.DayNight.PropertyChanged -= DayNight_PropertyChanged;

			ListLessons.Children.Clear();

			base.OnDisappearing();
		}

		//

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

			List<IndexedLesson> lessonsList = new List<IndexedLesson>();
			for(int i = 0; i < user.Classes.List.Count; i++)
			{
				Class cls = user.Classes.List[i];
				foreach (Lesson l in cls.Lessons.List)
					lessonsList.Add(new IndexedLesson(l, i));
			}

			lessonsList.Sort(new Comparer.LessonComparer());

			//

			int dayIndex = 0;
			int lessonsIndex = 0;
			TimeSpan lastEnd = new TimeSpan(7, 0, 0);

			while (lessonsIndex < lessonsList.Count)
			{
				Grid grid = gridList[dayIndex];
				IndexedLesson lesson = lessonsList[lessonsIndex];
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

					StackLayout s = new StackLayout
					{
						VerticalOptions = LayoutOptions.Center,
						HorizontalOptions = LayoutOptions.Center,
						Orientation = StackOrientation.Vertical,
					};
					s.Children.Add(new Label
					{
						Text = lesson.Name + ", " + user.Classes.List[lesson.ClassIndex].Name,
					});
					s.Children.Add(new Label
					{
						Text = lesson.LengthMinutes.ToString() + " min",
					});

					f = new Frame
					{
						//WidthRequest = 200,
						//HeightRequest = 200,
						BorderColor = Color.White,
						Content = s,
						Margin = 0,
						Padding = 3,
						HasShadow = false,
					};
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

		private void AddElementGrid(int minutes, List<Grid> list, int day, string label)
		{
			ColumnDefinition column = new ColumnDefinition
			{
				Width = new GridLength(minutes)
			};

			list[day].ColumnDefinitions.Add(column);

			Label popis = new Label
			{
				Text = label
			};

			list[day].Children.Add(popis);

			Grid.SetColumn(popis, list[day].ColumnDefinitions.Count - 1);
		}

		
		private void ChangeLightMode()
		{

		}

	}
}