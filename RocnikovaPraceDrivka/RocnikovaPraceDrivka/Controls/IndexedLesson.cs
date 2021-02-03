using System;
using System.Collections.Generic;
using System.Text;

namespace RocnikovaPraceDrivka.Controls
{
	public class IndexedLesson : Lesson
	{
		public Class Owner { get; private set; }

		//
		
		public IndexedLesson(Lesson lesson, Class owner)
			: base(lesson.Name, lesson.Start, lesson.End)
		{
			Owner = owner;
		}

		//

	}
}
