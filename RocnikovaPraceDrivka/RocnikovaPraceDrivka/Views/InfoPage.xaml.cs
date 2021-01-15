using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RocnikovaPraceDrivka.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InfoPage : ContentPage
	{
		public InfoPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{ 
			var born = new DateTime(2002, 4, 19);

			int age = DateTime.Now.Year - born.Year;
			if (DateTime.Now < born.AddYears(age))
				age--;

			AgeSpan.Text = age.ToString();

			base.OnAppearing();
		}
	}
}