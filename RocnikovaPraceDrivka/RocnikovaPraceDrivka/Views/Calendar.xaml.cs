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

		List<Lesson> weekly = new List<Lesson>();

		//

		public Calendar(User user)
		{
			InitializeComponent();
			this.user = user;
			list.ItemsSource = DOW.Names;
		}

		protected override void OnAppearing()
		{
			LoadGridTable();

			base.OnAppearing();
		}

		//

		private void LoadGridTable()
		{
			foreach (Class cls in user.Classes)
			{
				foreach (Lesson l in cls.LessonsList)
					weekly.Add(l);
			}

			weekly.Sort(new Comparer.LessonComparer());

			int index = 0;
			int day = 0;
			int min = (int)new TimeSpan(7, 0, 0).TotalMinutes;
			int actualMin = min;

			//int used = 0;
			//List<int> dayEndingList = new List<int>();
			//for (int i = 0; i < DOW.Names.Count; i++)
			//	dayEndingList.Add(min);

			List<Grid> gridList = new List<Grid>();
			for (int i = 0; i < DOW.Names.Count; i++)
				gridList.Add(new Grid());

			List<int> endingList = new List<int>();


			while (index != weekly.Count)
			{

				if (day == DOW.Names.Count)
					throw new Exception();

				if (endingList.Count == 0)
				{
					if (weekly[index].Start.Day == day)
					{
						int minutes = (int)weekly[index].Start.TimeOfDay.TotalMinutes - actualMin;

						// add lesson break in day grid
						// in this they have free period

						actualMin += minutes;
						endingList.Add((int)weekly[index].End.TimeOfDay.TotalMinutes - actualMin);

						index++;
					}
					else
					{
						day++;
						actualMin = min;
					}
				}
				else
				{
					int i = 0, a = 0;
					int minutes = endingList[0];

					if (endingList.Count > 1)
					{
						for (i = 1; i < endingList.Count; i++)
							if (endingList[i] < minutes)
								minutes = endingList[i];
					}

					if (weekly[index].Start.Day == day)
					{
						a = (int)weekly[index].Start.TimeOfDay.TotalMinutes - actualMin;
						if (a < minutes)
						{
							minutes = a;

							// add lesson in day grid
							// in this they have lesson inside lesson

							actualMin += minutes;
							endingList.Add((int)weekly[index].End.TimeOfDay.TotalMinutes - actualMin);

							index++;

							continue;
						}
					}

					index++;
					endingList.Remove(minutes);
					actualMin += minutes;

					// add lesson in day grid
					// in this time they have lesson

				}
			}
		}

		//



	}
}