using System;
using System.Collections.Generic;
using System.Text;

namespace RocnikovaPraceDrivka.Controls
{
	public class Student
	{
		protected string name;

		//

		public virtual string Name { get => name; set => name = value; }

		//

		public Student(string name)
		{
			Name = name;
		}
	}
}
