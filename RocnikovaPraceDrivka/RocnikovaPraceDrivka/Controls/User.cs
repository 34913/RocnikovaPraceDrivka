using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

using RocnikovaPraceDrivka.Managers;

namespace RocnikovaPraceDrivka.Controls
{
	public class User: Items<User>
	{
		protected string name;

		protected EmailControl email;

		protected int id;

		string pswd;

		//

		public ClassesManager Classes { get; private set; }

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

		public string Email { get => email.Address; }

		//

		public User(string email)
		{
			Name = email;

			Classes = new ClassesManager();
			this.email = new EmailControl(email);
		}

		public User(string name, string email)
		{
			Name = name;
			
			Classes = new ClassesManager();
			this.email = new EmailControl(email);
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

		public override void SetValues(User vals)
		{
			name = vals.name;
			id = vals.id;
			pswd = vals.pswd;
			email = vals.email;
			Classes = vals.Classes;
		}

		public static User u;

	}
}
