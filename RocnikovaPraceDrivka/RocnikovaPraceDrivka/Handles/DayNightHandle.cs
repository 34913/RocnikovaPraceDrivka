using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using System.Runtime.CompilerServices;

namespace RocnikovaPraceDrivka.Handles
{
	public class DayNightHandle : INotifyPropertyChanged
	{
		public enum ListModes
		{
			day = 0,
			night = 1
		};

		private ListModes state = ListModes.day;

		private string dayNightKeyHolder = "DayNightKey";

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		//

		protected ListModes State
		{
			get => state;
			set
			{
				if (value != state)
				{
					state = value;
					int val = (int)state;
					if (Application.Current.Properties.ContainsKey(dayNightKeyHolder))
						Application.Current.Properties.Remove(dayNightKeyHolder);
					Application.Current.Properties.Add(dayNightKeyHolder, val);

					NotifyPropertyChanged();
				}
			}
		}

		public bool Day
		{
			get
			{
				if (State == ListModes.day)
					return true;
				return false;
			}
			set
			{
				if (value == true)
					State = ListModes.day;
				else
					State = ListModes.night;
			}
		}

		public bool Night
		{
			get
			{
				return !Day;
			}
			set
			{
				Day = !value;
			}
		}

		public string ModeName
		{
			get
			{
				if (Day)
					return "Day mode";
				return "Night mode";
			}
		}

		public  void Swap()
		{
			Day = !Day;
		}

		//

		public DayNightHandle()
		{
			if (Application.Current.Properties.ContainsKey(dayNightKeyHolder))
				state = (ListModes)Application.Current.Properties[dayNightKeyHolder];
			else
				State = ListModes.day;
		}


		public static DayNightHandle DayNight;
	}

}
