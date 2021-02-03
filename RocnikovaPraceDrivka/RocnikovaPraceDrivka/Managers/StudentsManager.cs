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

		public override void Update(int index, Student newItem)
		{
			List.Insert(index, newItem);

			List[index].Draw = List[index + 1].Draw;
			
			List.RemoveAt(index + 1);
		}

	}
}
