using System;
using System.Collections.Generic;
using System.Text;

namespace RocnikovaPraceDrivka.Interfaces
{
	public abstract class Manager<T>
	{
		public string TableName { get; private set; }

		public Manager(string tableName)
		{
			TableName = tableName;
		}
		
		public abstract void Add(T item);

		public abstract T Select();

		public abstract void Delete(T item);

		public abstract void Update(T item);

	}
}
