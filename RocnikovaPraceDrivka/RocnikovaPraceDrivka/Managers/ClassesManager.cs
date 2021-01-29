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

		//
	}
}
