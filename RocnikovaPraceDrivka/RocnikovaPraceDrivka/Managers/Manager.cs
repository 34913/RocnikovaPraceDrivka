using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using RocnikovaPraceDrivka.Controls;

namespace RocnikovaPraceDrivka.Managers
{
	public abstract class Manager<T>
	{
		public ObservableCollection<T> List { get; set; }

		public string TableName { get; private set; }

		//

		public Manager(string tableName)
		{
			TableName = tableName;
			List = new ObservableCollection<T>();
		}
		
		//

		public void Add(T item)
		{
			List.Add(item);
			AddDB(item);
		}

		public abstract void AddDB(T item);

		public abstract void Select();

		public void Delete(T item)
		{
			if (List.IndexOf(item) == -1)
				throw new Exception("Not existing item");

			DeleteDB(item);
			List.Remove(item);
		}

		public abstract void DeleteDB(T item);

		public void Update(T oldItem, T newItem)
		{
			int index = List.IndexOf(oldItem);

			UpdateValues(oldItem, newItem);

			UpdateDB(index, newItem);
		}

		public abstract void UpdateDB(int index, T newItem);

		public abstract void UpdateValues(T oldItem, T newItem);

	}
}
