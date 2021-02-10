using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RocnikovaPraceDrivka.Controls
{
	public class Lesson: Items<Lesson>
	{
		protected string name;

		//
		
		public TimeSpan Start { get; private set; }

		public TimeSpan End { get; private set; }

		public TimeSpan Length { get => End - Start; }

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
		}

		//

		public override bool Equals(object obj)
		{
			return Equals(this, obj as Lesson);
		}

		public static bool Equals(Lesson o1, Lesson o2)
		{
			if (o1 == null || o2 == null)
				throw new Exception(string.Format("argument {0} is of value null", (o1 == null && o2 == null) ? "1 and 2" : ((o1 == null) ? "1" : "2")));

			if (o1 == o2)
				return true;
			return (
				o1.name == o2.name &&
				o1.Start == o2.Start &&
				o1.End == o2.End);
		}

		public override string ToString()
		{
			return Name;
		}

		//

		public override void SetValues(Lesson vals)
		{
			name = vals.name;
			Start = vals.Start;
			End = vals.End;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
