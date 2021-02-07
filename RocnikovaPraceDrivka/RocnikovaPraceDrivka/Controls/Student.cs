using System;
using System.Collections.Generic;
using System.Text;

namespace RocnikovaPraceDrivka.Controls
{
	public class Student: Items<Student>
	{
		protected string name;

		protected int draw;

		//

		public string Name { get => name; set => name = value; }

		public int Draw {
			get
			{
				return draw;
			}
			set
			{
				if (value < 0)
					throw new Exception("Not in range, need number bigger than zero");
				draw = value;
			}
		}

		public bool OneDraw { get => draw > 0; }

		//

		public Student(string name)
		{
			Name = name;
			NullDraw();
		}

		//

		public void NullDraw()
		{
			draw = 0;
		}

		public override bool Equals(object obj)
		{
			Student s = obj as Student;

			return (
				name == s.name &&
				draw == s.draw);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		//

		public override void SetValues(Student vals)
		{
			name = vals.name;
			draw = vals.draw;
		}

	}
}
