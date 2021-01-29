using System;
using System.Collections.Generic;
using System.Text;

namespace RocnikovaPraceDrivka.Controls
{
	public class Student
	{
		protected string name;

		protected int draw;

		//

		public string Name { get => name; set => name = value; }

		public int Draw { get => draw; }

		//

		public Student(string name)
		{
			Name = name;
			NullDraw();
		}

		//

		public void Drawen()
		{
			draw++;
		}

		public void NullDraw()
		{
			draw = 0;
		}

	}
}
