using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RocnikovaPraceDrivka.ViewModels
{
	public class BaseViewModel: INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}
}
