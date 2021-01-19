using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace RocnikovaPraceDrivka.MyElements
{
	class MyEntry: Entry
	{
		protected override void OnTextChanged(string oldValue, string newValue)
		{
			char c = newValue[newValue.Length - 1];
			

			base.OnTextChanged(oldValue, newValue);
		}
	}
}
