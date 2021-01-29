using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

using RocnikovaPraceDrivka.Managers;

namespace RocnikovaPraceDrivka.Controls
{
	public class User
	{
		protected string name;

		protected int id;

		string pswd;



		//

		public ClassesManager Classes { get; } = new ClassesManager();

		//

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

		public User(string email)
		{
			Email = email;
			Name = email;
		}

		public User(string name, string email)
		{
			Name = name;
			Email = email;
		}

		//

		public void Select()
		{

		}

		public void Add()
		{

		}

		public void Delete()
		{

		}

		public void Update()
		{

		}

	}
}
