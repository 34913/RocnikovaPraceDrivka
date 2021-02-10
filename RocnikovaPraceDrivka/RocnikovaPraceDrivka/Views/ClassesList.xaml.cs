using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RocnikovaPraceDrivka.MyElements;
using RocnikovaPraceDrivka.Controls;
using RocnikovaPraceDrivka.Popup;
using RocnikovaPraceDrivka.Handles;

namespace RocnikovaPraceDrivka.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClassesList : ContentPage
	{
		
		//

		public ClassesList()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			list.ItemsSource = User.u.Classes.List;
			ReloadCount();

			ChangeLightMode();

			OkButton.IsVisible = false;
			CancelButton.IsVisible = false;

			list.SelectionMode = SelectionMode.Single;

			DayNightHandle.DayNight.PropertyChanged += DayNight_PropertyChanged;

			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			DayNightHandle.DayNight.PropertyChanged -= DayNight_PropertyChanged;

			base.OnDisappearing();
		}

		//

		private void DayNight_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			ChangeLightMode();
		}

		private async void DetailsButton_Clicked(object sender, EventArgs e)
		{
			Button button = sender as Button;

			await Navigation.PushAsync(new InfoClass(button.BindingContext as Class));
		}

		private void AddButton_Clicked(object sender, EventArgs e)
		{
			ClassPop(true);
		}

		private void TapClass_TappedAsync(object sender, EventArgs e)
		{
			if (list.SelectionMode == SelectionMode.Single)
			{
				ClassPop(false);
				list.SelectedItem = null;
			}
		}

		private void EditButton_Clicked(object sender, EventArgs e)
		{
			EditButton.IsVisible = false;
			OkButton.IsVisible = true;
			CancelButton.IsVisible = true;

			list.SelectionMode = SelectionMode.Multiple;
		}

		private void CancelButton_Clicked(object sender, EventArgs e)
		{
			OkButton.IsVisible = false;
			CancelButton.IsVisible = false;

			list.SelectionMode = SelectionMode.Single;

			ReloadCount();
		}

		private void OkButton_Clicked(object sender, EventArgs e)
		{
			OkButton.IsVisible = false;
			CancelButton.IsVisible = false;

			List<Class> choosen = new List<Class>();

			foreach (Class c in list.SelectedItems)
				choosen.Add(c);

			foreach (Class c in choosen)
				User.u.Classes.Delete(c);

			list.SelectionMode = SelectionMode.Single;
			list.SelectedItems = null;
			ReloadCount();
		}

		//

		private Class AddClass(ContentPage l)
		{
			Entry nameEntry = l.FindByName<Entry>("NameEntry");
			Entry descEntry = l.FindByName<Entry>("DescEntry");

			if (string.IsNullOrWhiteSpace(descEntry.Text))
				descEntry.Text = string.Empty;

			if (string.IsNullOrWhiteSpace(nameEntry.Text))
				throw new Exception("Enter name");
			else
			{
				Class cls;
				try
				{
					string str = nameEntry.Text.ToUpper();
					cls = new Class(nameEntry.Text, descEntry.Text);
				}
				catch (Exception exc)
				{
					throw new Exception(string.Format("Wrong format, try something like 1.C\n{0}", exc.Message));
				}

				return cls;
			}
		}

		private void ChangeLightMode()
		{
			if (DayNightHandle.DayNight.Day)
			{
				
			}
			else
			{
			
			}
		}

		private void ReloadCount()
		{
			if (User.u.Classes.List.Count != 0)
				EditButton.IsVisible = true;
			else
				EditButton.IsVisible = false;

			ClassesCountSpan.Text = User.u.Classes.List.Count.ToString();

			if (User.u.Classes.List.Count != 1)
				endingClassesSpan.Text = "es";
			else
				endingClassesSpan.Text = string.Empty;
		}

		private async void ClassPop(bool nClass)
		{
			ContentPage detailsPage = new ContentPage
			{
				BackgroundColor = Color.Transparent
			};

			Class selected = list.SelectedItem as Class;

			AddClassPopup l;
			if (nClass)
				l = new AddClassPopup();
			else
				l = new AddClassPopup(selected);
			
			detailsPage.Content = l.Content;

			l.FindByName<Button>("CancelButton").Clicked += ((o2, e2) =>
			{
				Navigation.PopModalAsync();
			});

			async void action(object o, EventArgs e)
			{
				Class c = null;
				try
				{
					c = AddClass(l);
				}
				catch (Exception exc)
				{
					await DisplayAlert("Error", exc.Message, "OK");
					return;
				}

				if (nClass)
					User.u.Classes.Add(c);
				else
					User.u.Classes.Update(selected, c);

				list.ItemsSource = null;
				list.ItemsSource = User.u.Classes.List;

				await Navigation.PopModalAsync();
			}

			l.FindByName<Button>("OkButton").Clicked += ((o2, e2) =>
			{
				action(o2, e2);
			});

			l.FindByName<Entry>("NameEntry").Completed += ((o2, e2) =>
			{
				action(o2, e2);
			});

			l.FindByName<Entry>("DescEntry").Completed += ((o2, e2) =>
			{
				action(o2, e2);
			});

			await Navigation.PushModalAsync(detailsPage, false);
		}
	}
}