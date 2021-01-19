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

		protected Connection con;

		protected string TableName { get; } = "UsersElektronickaDrivka";

		//

		public User(string name)
		{
			Email = name;
			Name = name;
		}

		public User(string name, string email)
		{
			Name = name;
			Email = email;
		}

		//
		
		public void Add()
		{

		}

		public static User Load()
		{

			return null;
		}

		public void Delete()
		{

		}

		//

	}
}
