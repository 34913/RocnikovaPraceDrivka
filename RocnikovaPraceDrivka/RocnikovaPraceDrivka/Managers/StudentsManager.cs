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

		public override void AddDB(Student item)
		{

		}

		public override void DeleteDB(Student item)
		{
		}

		public override void UpdateDB(int index, Student newItem)
		{
		}

		public override void UpdateValues(Student oldItem, Student newItem)
		{
			oldItem.SetValues(newItem);
		}

	}
}
