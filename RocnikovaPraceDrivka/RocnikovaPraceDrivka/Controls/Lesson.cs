using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RocnikovaPraceDrivka.Controls
{
	public class Lesson
	{
		protected string name;

		//

		public TimeSpan Start { get; private set; }

		public TimeSpan End { get; private set; }

		public TimeSpan Length { get; private set; }

		public int LengthMinutes { get => (int)Length.TotalMinutes; }

		public string Day { get => Handles.DOW.Names[Start.Days]; }

		public string ShortDay { get => Day.Substring(0, 3); }

		public string Name { get => name; }

		//

		public Lesson(string name, TimeSpan start, TimeSpan end)
		{
			this.name = name;
			Start = start;
			End = end;

			Length = end - start;
		}

		//

		public override bool Equals(object obj)
		{
			return Equals(this, obj as Lesson);
		}

		public static bool Equals(Lesson o1, Lesson o2)
		{
			if (o1 == null || o2 == null)
				return false;

			return (
				o1.Day == o2.Day &&
				o1.Start == o2.Start &&
				o1.End == o2.End &&
				o1.Name == o2.Name);
		}

		public override string ToString()
		{
			return Name;
		}

	}
}
