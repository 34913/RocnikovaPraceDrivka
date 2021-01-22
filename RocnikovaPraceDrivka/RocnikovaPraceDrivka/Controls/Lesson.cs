using System;
using System.Collections.Generic;
using System.Text;

namespace RocnikovaPraceDrivka.Controls
{
	public class Lesson
	{
		protected string name;

		//

		public DateTime Start { get; private set; }

		public DateTime End { get; private set; }

		public TimeSpan Length { get; private set; }

		public string Name { get => name; }

		//

		public Lesson(string name, DateTime start, DateTime end)
		{
			this.name = name;
			Start = start;
			End = end;

			Length = end - start;
		}

		//

	}
}
