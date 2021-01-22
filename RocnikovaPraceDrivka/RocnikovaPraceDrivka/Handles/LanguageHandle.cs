using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RocnikovaPraceDrivka
{
	public static class LanguageHandle
	{
		public enum ListModes
		{
			english = 0,
			czech = 1
		};

		private static ListModes state = ListModes.english;

		private static string languageKeyHolder = "LanguageKey";

		//

		public static ListModes State
		{
			get => state;
			set
			{
				state = value;
				int val = (int)state;
				if (Application.Current.Properties.ContainsKey(languageKeyHolder))
					Application.Current.Properties.Remove(languageKeyHolder);
				Application.Current.Properties.Add(languageKeyHolder, val);
			}
		}

		public static bool English
		{
			get
			{
				if (State == ListModes.english)
					return true;
				return false;
			}
		}

		public static bool Czech
		{
			get
			{
				if (State == ListModes.czech)
					return true;
				return false;
			}
		}

		public static string ModeName
		{
			get
			{
				switch (state)
				{
					case ListModes.english:
						return "English";
					default:
						return "Čeština";
				}
			}
		}

		//

		static LanguageHandle()
		{
			if (Application.Current.Properties.ContainsKey(languageKeyHolder))
				state = (ListModes)Application.Current.Properties[languageKeyHolder];
			else
				State = ListModes.english;
		}


	}
}
