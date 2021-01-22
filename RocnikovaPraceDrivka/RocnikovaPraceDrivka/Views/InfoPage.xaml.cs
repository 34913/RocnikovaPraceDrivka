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

			ChangeLightMode();

			base.OnAppearing();
		}

		private void DayNightToolbarItem_Clicked(object sender, EventArgs e)
		{
			if (Handles.DayNightHandle.Day)
				Handles.DayNightHandle.Night = true;
			else
				Handles.DayNightHandle.Day = true;
			ChangeLightMode();
		}

		private void ChangeLightMode()
		{
			if (Handles.DayNightHandle.Day)
			{
				
				DayNightToolbarItem.IconImageSource = "Day.png";

				//

				var navigationPage = Application.Current.MainPage as NavigationPage;
				navigationPage.BarBackgroundColor = Color.White;

				content.BackgroundColor = Color.White;
			}
			else
			{
				NameLabel.TextColor = Color.White;
				EmailLabel.TextColor = Color.White;
				AgeLabel.TextColor = Color.White;
				extrasLabel.TextColor = Color.White;
				DayNightToolbarItem.IconImageSource = "Night.png";

				//

				var navigationPage = Application.Current.MainPage as NavigationPage;
				navigationPage.BarBackgroundColor = Color.Black;

				content.BackgroundColor = Color.Black;
			}
		}
	}
}