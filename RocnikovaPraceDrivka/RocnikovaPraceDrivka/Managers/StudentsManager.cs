using System;
using System.Collections.Generic;
using System.Text;

using RocnikovaPraceDrivka.Controls;

namespace RocnikovaPraceDrivka.Managers
{
	public class StudentsManager : Manager<Student>
	{

		public StudentsManager()
			: base("StudentsTable")
		{

		}

		//

		public override void Select()
		{
			List.Add(new Student("TohleJeJmeno"));
		}

	}
}
