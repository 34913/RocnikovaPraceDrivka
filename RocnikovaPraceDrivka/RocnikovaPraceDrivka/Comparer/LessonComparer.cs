using System;
using System.Collections.Generic;
using System.Text;

using RocnikovaPraceDrivka.Controls;

namespace RocnikovaPraceDrivka.Comparer
{
	class LessonComparer : IComparer<Lesson>
	{
		public int Compare(Lesson x, Lesson y)
		{
			Lesson l1 = (Lesson)x;
			Lesson l2 = (Lesson)y;
			if (l1.Start > l2.Start)
				return 1;
			if (l1.Start < l2.Start)
				return -1;
			else
			{
				if (l1.End > l2.End)
					return 1;
				if (l1.End < l2.End)
					return -1;
				return 0;
			}
		}
	}
}
