using System;
using System.Collections.Generic;
using System.Text;

namespace RocnikovaPraceDrivka.Controls
{
	public class IndexedLesson : Lesson
	{
		public int ClassIndex { get; private set; }

		public IndexedLesson(string name, TimeSpan start, TimeSpan end, int classIndex) 
			: base(name, start, end)
		{
			ClassIndex = ClassIndex;
		}

		public IndexedLesson(Lesson lesson, int classIndex)
			:base(lesson.Name, lesson.Start, lesson.End)
		{
			ClassIndex = classIndex;
		}
	}
}
