using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace RocnikovaPraceDrivka.Controls
{
	public class CalendarControl : INotifyPropertyChanged
	{
		public ObservableCollection<Lesson> List { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		//

		public CalendarControl()
		{
			List = new ObservableCollection<Lesson>();
		}

		//

		public void AddLesson(IndexedLesson l)
		{
			int i = 0;
			for(i = 0; i < List.Count;)
			{

				if (l.Start < List[i].Start)
				{
					List.Insert(i, l);
					break;
				}
				else if (l.Equals(List[i]))
				{
					if (List[i] is IndexedLesson)
						List[i] = new MergedLesson(List[i] as IndexedLesson);
					(List[i] as MergedLesson).OwnerList.Add(l.Owner);
					break;
				}
				i++;
			}

			if (i == List.Count)
				List.Add(l);
			NotifyPropertyChanged();
		}





		public static CalendarControl AllLessons = new CalendarControl();
	}
}
