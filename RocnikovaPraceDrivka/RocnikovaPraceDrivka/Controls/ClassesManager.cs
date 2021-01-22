using System;
using System.Collections.Generic;
using System.Text;

using RocnikovaPraceDrivka.Interfaces;

namespace RocnikovaPraceDrivka.Controls
{

	public class ClassesManager : Manager<Class>
	{

		public ClassesManager()
			: base("ClassesTable")
		{

		}

		//
		
		public override void Add(Class item)
		{
			throw new NotImplementedException();
		}

		public override Class Select()
		{
			throw new NotImplementedException();
		}

		public List<Class> SelectAll()
		{
			List<Class> list = new List<Class>();

			list.Add(new Class("1.C", "hajzlíci"));
			list.Add(new Class("2.C", "hajzlíci"));
			list.Add(new Class("3.C", "hajzlíci"));
			list.Add(new Class("4.C", "hajzlíci"));
			list.Add(new Class("5.C", "hajzlíci"));
			list.Add(new Class("6.C", "hajzlíci"));
			list.Add(new Class("7.C", "hajzlíci"));
			list.Add(new Class("8.C", "hajzlíci"));

			return list;
		}

		public override void Delete(Class item)
		{
			throw new NotImplementedException();
		}

		public override void Update(Class item)
		{
			throw new NotImplementedException();
		}

		//
	}
}
