using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RocnikovaPraceDrivka.Controls
{
	public class Draw
	{
		public Random r = new Random();

		protected Student choosenStudent = null;
		protected int choosenStudentIndex = -1;
		
		//

		public Student ChoosenStudent { get => choosenStudent; }
		public int ChoosenStudentIndex { get => choosenStudentIndex; }

		//

		public Draw()
		{
		}

		//

		public bool Next(List<Student> list)
		{
			List<Student> newList = new List<Student>();
			foreach (Student s in list)
				if (!s.OneDraw)
					newList.Add(s);

			if (newList.Count == 0)
				return false;

			Random r = new Random();

			choosenStudentIndex = r.Next(0, newList.Count);
			choosenStudent = newList[choosenStudentIndex];

			return true;
		}

		public void Null()
		{
			choosenStudent = null;
			choosenStudentIndex = -1;
		}


	}
}
