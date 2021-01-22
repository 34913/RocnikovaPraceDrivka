using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RocnikovaPraceDrivka.Handles;

namespace RocnikovaPraceDrivka.Popup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddLessonPopup : ContentPage
	{
		public AddLessonPopup()
		{
			InitializeComponent();

			DayPicker.ItemsSource = DOW.Names;
		}
	}
}