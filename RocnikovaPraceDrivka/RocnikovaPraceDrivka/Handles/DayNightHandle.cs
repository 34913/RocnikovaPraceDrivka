using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RocnikovaPraceDrivka.Handles
{
	public static class DayNightHandle
	{
		public enum ListModes
		{
			day = 0,
			night = 1
		};

		private static ListModes state = ListModes.day;

		private static string dayNightKeyHolder = "DayNightKey";

		//

		public static ListModes State
		{
			get => state;
			set
			{
				state = value;
				int val = (int)state;
				if (Application.Current.Properties.ContainsKey(dayNightKeyHolder))
					Application.Current.Properties.Remove(dayNightKeyHolder);
				Application.Current.Properties.Add(dayNightKeyHolder, val);
			}
		}

		public static bool Day
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

		public static bool Night
		{
			get
			{
				if (State == ListModes.night)
					return true;
				return false;
			}
			set
			{
				if (value == true)
					State = ListModes.night;
				else
					State = ListModes.day;
			}
		}

		public static string ModeName
		{
			get
			{
				if (Day)
					return "Day mode";
				return "Night mode";
			}
		}

		//

		static DayNightHandle()
		{
			if (Application.Current.Properties.ContainsKey(dayNightKeyHolder))
				state = (ListModes)Application.Current.Properties[dayNightKeyHolder];
			else
				State = ListModes.day;
		}

	}
}
