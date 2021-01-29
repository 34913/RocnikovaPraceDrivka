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

	}
}
