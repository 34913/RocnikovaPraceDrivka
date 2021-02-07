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

		public override void AddDB(Class item)
		{
		}

		public override void DeleteDB(Class item)
		{
		}

		public override void UpdateDB(int index, Class newItem)
		{
		}

		public override void UpdateValues(Class oldItem, Class newItem)
		{
			oldItem.SetValues(newItem);
		}

		//
	}
}
