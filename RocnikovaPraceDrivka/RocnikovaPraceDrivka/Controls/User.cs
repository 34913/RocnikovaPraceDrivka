using System;
using System.Collections.Generic;
using System.Text;

namespace RocnikovaPraceDrivka.Controls
{
	public class User
	{
		public int Id { get; private set; }

		private string name;

		public string Name {
			get => name;
			set
			{
				if (value.Contains("@"))
				{
					string s = value;
					name = s.Remove(s.IndexOf('@'));
				}
				else
					name = value;
			}
		}

		public string Email { get; private set; }

		//

		public User(string name)
		{
			Email = name;
			Name = name;
		}

		public User(int id, string email)
		{
			Email = name;
			Name = name;
			Id = id;
		}

		//
		
		public void Add()
		{

		}

		public void Load()
		{

		}

		public void Delete()
		{

		}

		//

	}
}
