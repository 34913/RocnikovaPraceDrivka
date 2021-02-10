using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

using RocnikovaPraceDrivka.Managers;

namespace RocnikovaPraceDrivka.Controls
{
	public class Class: Items<Class>
	{
		public StudentsManager Students { get; set; }

		public LessonsManager Lessons { get; set; }

		//

		private int year;
		private string type;

		public int Year { get => year; }

		public string Type { get => type; }

		public static char separator = '.';

		public string Name
		{
			set
			{
				string str = value;
				string[] array;
				if (!str.Contains("."))
				{
					int i = 0;
					do
					{
						if (!char.IsDigit(str[i]))
						{
							if (i == 0)
								throw new Exception("No class number");
							else
								break;
						}
						i++;
					} while (i < str.Length);

					if (i == str.Length)
						throw new Exception("No class character");
					array = new string[2];

					array[0] = str.Substring(0, i);
					array[1] = str.Substring(i);
				}
				else
				{
					array = str.Split('.');
					if (array.Length != 2)
						throw new Exception("Too many dots");
				}
			
				if (!(int.TryParse(array[0], out year)))
					throw new Exception("Class number is not number");
				type = array[1];
			}
			get
			{
				return year.ToString() + separator + type.ToString().ToUpper();
			}
		}

		public string Desc { get; private set; }

		//

		public Class(string name, string desc)
		{
			Name = name;
			Desc = desc;

			Students = new StudentsManager();
			Lessons = new LessonsManager(this);
		}

		public Class(int year, string type, string desc)
		{
			this.year = year;
			this.type = type;
			Desc = desc;

			Students = new StudentsManager();
			Lessons = new LessonsManager(this);
		}

		//

		public override string ToString()
		{
			return Name;
		}

		//

		public override void SetValues(Class vals)
		{
			Name = vals.Name;
			Desc = vals.Desc;


			//Lessons = vals.Lessons;
			//Students = vals.Students;
		}

	}
}
