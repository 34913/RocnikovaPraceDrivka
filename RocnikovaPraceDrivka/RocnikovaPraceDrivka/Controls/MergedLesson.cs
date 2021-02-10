using System;
using System.Collections.Generic;
using System.Text;

namespace RocnikovaPraceDrivka.Controls
{
	public class MergedLesson : Lesson
	{
		public List<Class> OwnerList { get; private set; }

		public MergedLesson(string name, TimeSpan start, TimeSpan end, List<Class> owners)
			: base(name, start, end)
		{
			OwnerList = new List<Class>(owners);
		}

		public MergedLesson(string name, TimeSpan start, TimeSpan end, Class owner)
			: base(name, start, end)
		{
			OwnerList = new List<Class>
			{
				owner,
			};
		}

		//

		public override bool Equals(object obj)
		{
			return Lesson.Equals(this, obj as MergedLesson);
		}

	}
}
