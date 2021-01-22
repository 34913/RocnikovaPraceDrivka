using System;
using System.Collections.Generic;
using System.Text;

namespace RocnikovaPraceDrivka.Handles
{
	class DOW
	{
		public static List<string> Names = new List<string>();

		static DOW()
		{
			if (LanguageHandle.Czech)
			{
				Names.Add("Pondělí");
				Names.Add("Úterý");
				Names.Add("Pátek");
				Names.Add("Čtvrtek");
				Names.Add("Pátek");
			}
			else if (LanguageHandle.English)
			{
				Names.Add("Monday");
				Names.Add("Tuesday");
				Names.Add("Wednesday");
				Names.Add("Thursday");
				Names.Add("Friday");
			}
		}




	}
}
