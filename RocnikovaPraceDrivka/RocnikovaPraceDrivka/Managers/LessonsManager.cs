using System;
using System.Collections.Generic;
using System.Text;

using RocnikovaPraceDrivka.Controls;

namespace RocnikovaPraceDrivka.Managers
{
	public class LessonsManager: Manager<Lesson>
	{
		private Class owner;
		
		public LessonsManager(Class owner)
			: base("StudentsTable")
		{
			this.owner = owner;
		}

		//

		public override void Select()
		{
			List.Add(new MergedLesson(new Lesson("TohleJeHodina", new TimeSpan(0, 7, 0, 0), new TimeSpan(0, 12, 0, 0)), owner));
		}

		public override void UpdateValues(Lesson oldItem, Lesson newItem)
		{
			oldItem.SetValues(newItem);
		}

		public override void AddDB(Lesson item)
		{
		}

		public override void DeleteDB(Lesson item)
		{
		}

		public override void UpdateDB(int index, Lesson newItem)
		{
		}
	}
}
