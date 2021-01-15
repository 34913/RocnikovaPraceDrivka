using System;
using System.Collections.Generic;
using System.Text;

namespace RocnikovaPraceDrivka.Controls
{
	public class Class
	{
		private int year;
		private char type;

		public int Year { get => year; }

		public char Type { get => type; }

		public static char separator = '.';

		public string Name
		{
			set
			{
				string[] array = value.Split('.');

				if (!(int.TryParse(array[0], out year) && char.TryParse(array[1], out type)))
					throw new Exception("wrong parse");
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

		}

		public Class(int year, char type, string desc)
		{
			this.year = year;
			this.type = type;
			Desc = desc;
		}

		//


		/// <summary>
		/// deletes user from sql table
		/// </summary>
		public void Delete()
		{

		}





	}
}
