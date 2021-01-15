using RocnikovaPraceDrivka.Controls;
using RocnikovaPraceDrivka.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RocnikovaPraceDrivka.Tabs
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalendarClassesTabs : TabbedPage
	{
		public CalendarClassesTabs(User user)
		{
			InitializeComponent();

			Title = user.Name;

			Children.Add(new Calendar());
			Children.Add(new ClassesList());

		}
	}
}