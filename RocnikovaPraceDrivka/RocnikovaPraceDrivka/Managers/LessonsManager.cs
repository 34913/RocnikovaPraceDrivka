using System;
using System.Collections.Generic;
using System.Text;

using RocnikovaPraceDrivka.Controls;

namespace RocnikovaPraceDrivka.Managers
{
	public class LessonsManager: Manager<Lesson>
	{
		public LessonsManager()
			: base("StudentsTable")
		{

		}

		//

		public override void Select()
		{
			List.Add(new Lesson("TohleJeHodina", new TimeSpan(0, 7, 0, 0), new TimeSpan(0, 12, 0, 0)));
		}

		public override void Add(Lesson item)
		{
			base.Add(item);
		}

		public override void Update(int index, Lesson newItem)
		{
			if (List[index].Equals(newItem))
				return;

			base.Update(index, newItem);
		}

	}
}
