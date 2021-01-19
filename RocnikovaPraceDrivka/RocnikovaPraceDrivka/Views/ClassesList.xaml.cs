using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RocnikovaPraceDrivka.MyElements;

using RocnikovaPraceDrivka.Controls;

namespace RocnikovaPraceDrivka.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClassesList : ContentPage
	{
		private ClassesManager Manager { get; set; }

		public ClassesList()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			Manager = new ClassesManager();

			LoadClasses();

			list.ItemsSource = Manager.ClsList;


			//MyClassButton[] array = (MyClassButton[])AllStuff.Children.OfType<MyClassButton>();

			MyClassButton[] array = AllStuff.Children.Where(p => p is MyClassButton) as MyClassButton[];



			//for (int i = 0; i < Manager.ClsList.Count; i++)
			//{
			//	array[i].ClassItem = Manager.ClsList[i];
			//}

			base.OnAppearing();
		}

		private void LoadClasses()
		{
			Manager.Add(new Class("1.C", "hajzlíci"));
			Manager.Add(new Class("2.C", "hajzlíci"));
			Manager.Add(new Class("3.C", "hajzlíci"));
			Manager.Add(new Class("4.C", "hajzlíci"));
			Manager.Add(new Class("5.C", "hajzlíci"));
			Manager.Add(new Class("6.C", "hajzlíci"));
			Manager.Add(new Class("7.C", "hajzlíci"));
			Manager.Add(new Class("8.C", "hajzlíci"));
		}

		private async void Alert(MyClassButton b)
		{
			await DisplayAlert("button", b.Text, "OK");
		}

		private async void DetailsButton_Clicked(object sender, EventArgs e)
		{
			MyClassButton obj = sender as MyClassButton;

			await DisplayAlert(obj.ClassItem.Name, obj.ClassItem.Desc, "OK");
		}
	}
}