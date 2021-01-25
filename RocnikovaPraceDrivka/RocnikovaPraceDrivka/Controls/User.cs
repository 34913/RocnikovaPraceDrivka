using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

using RocnikovaPraceDrivka.Interfaces;

namespace RocnikovaPraceDrivka.Controls
{
	public class User: Manager<User>
	{
		protected string name;

		public ObservableCollection<Class> Classes { get; set; } = new ObservableCollection<Class>();

		public ClassesManager Manager { get; } = new ClassesManager();

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
			: base("UsersTable")
		{
			Email = email;
			Name = email;
		}

		public User(string name, string email)
			: base("UsersTable")
		{
			Name = name;
			Email = email;
		}

		//

		public override void Add(User item)
		{
			return;
		}

		public void Add()
		{
			Add(this);
		}

		public override User Select()
		{
			return this;
		}

		public override void Delete(User item)
		{
			return;
		}

		public override void Update(User item)
		{
			return;
		}

		//




	}
}
