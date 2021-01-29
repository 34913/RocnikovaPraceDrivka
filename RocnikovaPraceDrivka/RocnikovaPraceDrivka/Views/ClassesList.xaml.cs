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
		private User user;

		//

		public ClassesList(User user)
		{
			InitializeComponent();

			this.user = user;
		}

		protected override void OnAppearing()
		{
			list.ItemsSource = user.Classes.List;
			ClassesCountSpan.Text = user.Classes.List.Count.ToString();

			if (user.Classes.List.Count != 1)
				endingClassesSpan.Text = "es";
			else

				endingClassesSpan.Text = string.Empty;

			ChangeLightMode();

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
			MyClassButton obj = sender as MyClassButton;

			await Navigation.PushAsync(new InfoClass(obj.ClassItem));
		}

		private async void AddButton_Clicked(object sender, EventArgs e)
		{
			ContentPage detailsPage = new ContentPage
			{
				Padding = new Thickness(80, 80, 80, 80),
				BackgroundColor = Color.Transparent
			};

			AddClassPopup l = new AddClassPopup();
			detailsPage.Content = l.Content;

			l.FindByName<Button>("CancelButton").Clicked += ((o2, e2) =>
			{
				Navigation.PopModalAsync();
			});

			l.FindByName<Button>("OkButton").Clicked += ((o2, e2) =>
			{
				AddClass(l);
			});

			l.FindByName<Entry>("NameEntry").Completed += ((o2, e2) =>
			{
				AddClass(l);
			});

			l.FindByName<Entry>("DescEntry").Completed += ((o2, e2) =>
			{
				AddClass(l);
			});

			await Navigation.PushModalAsync(detailsPage, false);
		}

		//

		private async void AddClass(ContentPage l)
		{
			Entry nameEntry = l.FindByName<Entry>("NameEntry");
			Entry descEntry = l.FindByName<Entry>("DescEntry");

			if (string.IsNullOrWhiteSpace(descEntry.Text))
				descEntry.Text = string.Empty;

			if (string.IsNullOrWhiteSpace(nameEntry.Text))
				await DisplayAlert("Error", "Enter name", "OK");
			else
			{
				try
				{
					string str = nameEntry.Text.ToUpper();
					user.Classes.Add(new Class(nameEntry.Text, descEntry.Text));
				}
				catch(Exception exc)
				{
					await DisplayAlert("Error", "Wrong format, try something like 1.C", "OK");
					return;
				}

				await Navigation.PopModalAsync();
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

	}
}