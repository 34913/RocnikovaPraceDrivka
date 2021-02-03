using System;
using System.Collections.Generic;
using System.Text;

namespace RocnikovaPraceDrivka.Controls
{
	public class MergedLesson : Lesson
	{
		public List<Class> OwnerList { get; private set; }

		public MergedLesson(Lesson lesson, List<Class> owners)
			: base(lesson.Name, lesson.Start, lesson.End)
		{
			OwnerList = owners;
		}

		public MergedLesson(Lesson lesson, Class owner)
			: base(lesson.Name, lesson.Start, lesson.End)
		{
			OwnerList = new List<Class>();
			OwnerList.Add(owner);
		}

		public MergedLesson(IndexedLesson lesson)
			: base(lesson.Name, lesson.Start, lesson.End)
		{
			OwnerList = new List<Class>();
			OwnerList.Add(lesson.Owner);
		}

		//


	}
}
