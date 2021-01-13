using System;
using System.Collections.Generic;
using System.Text;

namespace RocnikovaPraceDrivka.Controls
{
	public class ClassesManager
	{
		public List<Class> List { get; private set; }

		//

		public ClassesManager()
		{
			List = new List<Class>();
		}

		//

		public void Add(Class cls)
		{
			List.Add(cls);
		}

		public void Update(Class cls, int index)
		{
			List[index] = cls;
		}

		public void Delete(int index)
		{
			List.RemoveAt(index);
		}

		//



	}
}
