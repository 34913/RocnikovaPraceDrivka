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

		public void LoadAll()
		{
			foreach (Class c in User.u.Classes.List)
				foreach (MergedLesson l in c.Lessons.List)
					AddLesson(l);
		}

		public void AddLesson(MergedLesson lesson)
		{
			int i = 0;

			while(i < List.Count)
			{
				if (lesson.Start < List[i].Start)
					break;
				else if (lesson.Equals(List[i]))
				{
					List[i].OwnerList.AddRange(lesson.OwnerList);

					NotifyPropertyChanged();
					return;
				}
				i++;
			}
			List.Insert(i, lesson);

			NotifyPropertyChanged();
		}

		public void UpdateLesson(Lesson oldLesson, Lesson newLesson)
		{
			for (int i = 0; i < List.Count; i++)
			{
				if (oldLesson.Equals(List[i]))
				{
					List[i].SetValues(newLesson);

					NotifyPropertyChanged();
					return;
				}
			}
			throw new Exception(string.Format("dont exist, length {0}", List.Count));
		}

		public void RemoveLesson(Lesson lesson, Class owner)
		{
			for(int i = 0; i < List.Count; i++)
			{
				if (lesson.Equals(List[i]))
				{
					List[i].OwnerList.Remove(owner);

					if (List[i].OwnerList.Count == 0)
						List.RemoveAt(i);
					NotifyPropertyChanged();
					return;
				}
			}
			throw new Exception(string.Format("dont exist, length {0}", List.Count));
		}

		public bool DontCross(Lesson oldLesson, Lesson newLesson)
		{
			foreach(Lesson inst in List)
			{
				if (oldLesson != null)
					if (inst.Equals(oldLesson))
						continue;
				if ((inst.Start > newLesson.Start && inst.Start < newLesson.End) ||
					(newLesson.Start > inst.Start && newLesson.Start < inst.End) ||
					(newLesson.Start == inst.Start) ||
					(newLesson.End == inst.End))
					return false;
			}
			return true;
		}

		//

		public static CalendarControl cc;
	}
}
