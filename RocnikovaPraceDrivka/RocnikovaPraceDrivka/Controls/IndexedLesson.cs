using System;
using System.Collections.Generic;
using System.Text;

namespace RocnikovaPraceDrivka.Controls
{
	class IndexedLessonZkouska: Lesson
	{

		public Class Owner { get; private set; }

		public IndexedLessonZkouska(Lesson lesson, Class owner)
			:base(lesson.Name, lesson.Start, lesson.End)
		{
			Owner = owner;
		}

	}
}
