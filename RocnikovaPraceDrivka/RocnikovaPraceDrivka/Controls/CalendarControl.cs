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
		public ObservableCollection<MergedLesson> List { get; private set; }

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		//

		public CalendarControl()
		{
			List = new ObservableCollection<MergedLesson>();
		}

		//

		public void Clear()
		{
			List.Clear();
			NotifyPropertyChanged();
		}

		public void LoadAll()
		{
			foreach (Class c in User.u.Classes.List)
				foreach (Lesson l in c.Lessons.List)
					AddLesson(l, c);
		}

		public void AddLesson(Lesson lesson, Class owner)
		{

			int i = 0;

			while(i < List.Count)
			{
				if (lesson.Start < List[i].Start)
					break;
				else if (lesson.Equals(List[i]))
				{
					List[i].OwnerList.Add(owner);
					NotifyPropertyChanged();
					return;
				}
				i++;
			}
			List.Insert(i, new MergedLesson(lesson, owner));

			NotifyPropertyChanged();
		}

		public void UpdateLesson(Lesson oldLesson, Lesson newLesson)
		{
			for (int i = 0; i < List.Count; i++)
			{
				MergedLesson inst = List[i];
				if (oldLesson.Equals(inst))
				{
					inst = new MergedLesson(newLesson, inst.OwnerList);

					NotifyPropertyChanged();
					return;
				}
			}
		}

		public void RemoveLesson(Lesson lesson, Class owner)
		{
			for(int i = 0; i < List.Count; i++)
			{
				MergedLesson inst = List[i];
				if (lesson.Equals(inst))
				{
					inst.OwnerList.Remove(owner);

					if (inst.OwnerList.Count == 0)
						List.RemoveAt(i);
					NotifyPropertyChanged();
					return;
				}
			}
		}

		public bool DontCross(Lesson l)
		{
			foreach(Lesson inst in List)
			{
				if ((inst.Start > l.Start && inst.Start < l.End) ||
					(l.Start > inst.Start && l.Start < inst.End) ||
					(l.Start == inst.Start) ||
					(l.End == inst.End))
					return false;
			}
			return true;
		}

		//

		public static CalendarControl cc;
	}
}
