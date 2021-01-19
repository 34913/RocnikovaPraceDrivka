using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RocnikovaPraceDrivka.Controls;

namespace RocnikovaPraceDrivka.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InfoClass : ContentPage
	{
		public InfoClass(Class cls)
		{
			InitializeComponent();

			NameLabel.BindingContext = cls;
			DescLabel.BindingContext = cls;
		}

		protected override void OnAppearing()
		{




			base.OnAppearing();
		}
	}
}