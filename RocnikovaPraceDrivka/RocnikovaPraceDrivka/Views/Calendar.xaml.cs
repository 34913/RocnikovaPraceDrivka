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
			
			Grid zapati = new Grid();

			zapati.RowDefinitions.Add(new RowDefinition()
			{
				Height = GridLength.Auto,
			});
			zapati.ColumnDefinitions.Add(new ColumnDefinition
			{
				Width = new GridLength(90, GridUnitType.Absolute),
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
			mainGrid.ColumnDefinitions.Add(new ColumnDefinition
			{
				Width = GridLength.Auto,
			});
			mainGrid.RowDefinitions.Add(new RowDefinition
			{
				Height = GridLength.Auto,
			});

			mainGrid.Children.Add(zapati);
			Grid.SetColumn(zapati, 0);
			Grid.SetRow(zapati, mainGrid.RowDefinitions.Count - 1);

			//

			List<Grid> gridList = new List<Grid>();

			foreach (string s in DOW.Names) {
				Grid grid = new Grid
				{
					VerticalOptions = LayoutOptions.FillAndExpand,
					HorizontalOptions = LayoutOptions.FillAndExpand,
				};

				grid.RowDefinitions.Add(new RowDefinition
				{
					Height = GridLength.Auto,
				});
				grid.ColumnDefinitions.Add(new ColumnDefinition
				{
					Width = new GridLength(90, GridUnitType.Absolute)
				});

				Label label = new Label
				{
					Text = s,
				};
				grid.Children.Add(label);

				Grid.SetColumn(label, 0);
				Grid.SetRow(label, 0);

				gridList.Add(grid);
			}

			//

			List<Lesson> lessonsList = new List<Lesson>();
			foreach (Class cls in user.Classes.List)
			{
				foreach (Lesson l in cls.Lessons.List)
					lessonsList.Add(l);
			}

			lessonsList.Sort(new Comparer.LessonComparer());

			//

			int dayIndex = 0;
			int lessonsIndex = 0;
			TimeSpan lastEnd = new TimeSpan(7, 0, 0);

			while (lessonsIndex < lessonsList.Count)
			{
				Grid grid = gridList[dayIndex];
				Lesson lesson = lessonsList[lessonsIndex];
				TimeSpan startShort = lesson.Start - new TimeSpan(dayIndex, 0, 0, 0);
				TimeSpan endShort = lesson.End - new TimeSpan(dayIndex, 0, 0, 0);
				Frame f;
				Label l;

				if (lesson.Start.Days == dayIndex)
				{
					TimeSpan difference = startShort - lastEnd;

					if (startShort.TotalMinutes != 0)
					{
						f = new Frame
						{
							Padding = 0,
							Margin = 0,
							BorderColor = Color.White,
							HasShadow = false,
						};
						grid.Children.Add(f);

						grid.ColumnDefinitions.Add(new ColumnDefinition
						{
							Width = new GridLength((difference.TotalMinutes - 1) * multiplier, GridUnitType.Absolute)
						});
						Grid.SetColumn(f, grid.Children.Count - 1);
						Grid.SetRow(f, 0);
					}

					StackLayout s = new StackLayout();
					s.Children.Add(new Label
					{
						Text = lesson.Name,
						VerticalOptions = LayoutOptions.Center,
						HorizontalOptions = LayoutOptions.Center,
					});
					s.Children.Add(new Label
					{
						
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

			foreach(Grid grid in gridList)
			{
				mainGrid.RowDefinitions.Add(new RowDefinition
				{
					Height = new GridLength(1, GridUnitType.Star),
				});
				mainGrid.Children.Add(grid);

				Grid.SetColumn(grid, 0);
				Grid.SetRow(grid, mainGrid.RowDefinitions.Count - 1);
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