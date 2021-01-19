using System;
using System.Collections.Generic;
using System.Text;

namespace RocnikovaPraceDrivka.Controls
{
	public class ClassesManager
	{
		public List<Class> ClsList { get; private set; }

		//

		public ClassesManager()
		{
			ClsList = new List<Class>();
		}

		//
		
		public void Add(Class cls)
		{
			ClsList.Add(cls);
		}

		public void Update(Class cls, int index)
		{
			ClsList[index] = cls;
		}

		public void Update(Class cls, Class last)
		{
			ClsList[ClsList.IndexOf(last)] = cls;
		}

		public void Delete(Class cls)
		{
			ClsList.RemoveAt(ClsList.IndexOf(cls));
		}

		public void Delete(int index)
		{
			ClsList.RemoveAt(index);
		}

		//
	}
}
