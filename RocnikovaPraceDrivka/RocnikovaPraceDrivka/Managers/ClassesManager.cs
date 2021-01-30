using System;
using System.Collections.Generic;
using System.Text;

using RocnikovaPraceDrivka.Controls;

namespace RocnikovaPraceDrivka.Managers
{

	public class ClassesManager : Manager<Class>
	{

		public ClassesManager()
			: base("ClassesTable")
		{

		}

		//
		
		public override void Select()
		{
			List.Add(new Class("1.C", "TohleJePopis"));


		}

		public override void Update(int index, Class newItem)
		{
			List.Insert(index, newItem);

			List[index].Lessons = List[index + 1].Lessons;
			List[index].Students = List[index + 1].Students;

			List.RemoveAt(index + 1);
		}

		//
	}
}
