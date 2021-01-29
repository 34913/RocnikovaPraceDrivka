using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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

		public virtual void Add(T item)
		{
			List.Add(item);
		}

		public abstract void Select();

		public void Delete(T item)
		{
			int index = List.IndexOf(item);
			if (index == -1)
				throw new Exception("Not existing item");
			Delete(List.IndexOf(item));
		}

		public virtual void Delete(int indexWhere)
		{
			if(indexWhere == -1)
			{
				throw new Exception("Not valid index");
			}
			try
			{
				List.RemoveAt(indexWhere);
			}
			catch(Exception exc)
			{
				throw exc;
			}
		}

		public void Update(T oldItem, T newItem)
		{
			int index = List.IndexOf(oldItem);

			Update(index, newItem);
		}

		public virtual void Update(int index, T newItem)
		{
			List.Insert(index, newItem);
			List.RemoveAt(index + 1);
		}

	}
}
