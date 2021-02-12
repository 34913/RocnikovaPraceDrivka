using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RocnikovaPraceDrivka.Controls;
using RocnikovaPraceDrivka.Handles;

namespace RocnikovaPraceDrivka.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignIn : ContentPage
	{

        /// <summary>
        /// boolean val, if true, the user has not been logged in on this device
        /// </summary>
        private bool register;

        /// <summary>
        /// register properties key
        /// </summary>
        protected string registeredKey = "registerKey";
        
        /// <summary>
        /// login properties key
        /// </summary>
        protected string loginNameKey = "loginNameKey";

        //

        public SignIn()
        {
            InitializeComponent();
        }

		protected override void OnAppearing()
		{
            ClearPswd();

            SignFunc();

            ChangeModeAction();

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

        // systems calling

        private void DayNight_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ChangeLightMode();
        }

        private async void SubmitButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailEntry.Text)
                || string.IsNullOrWhiteSpace(PswdEntry.Text)
                || (string.IsNullOrWhiteSpace(PswdVerifyEntry.Text) && register))
            {
                bool first = false;
                string s = "Please enter ";

                if (string.IsNullOrWhiteSpace(EmailEntry.Text))
                {
                    s += "email";
                    first = true;
                }
                if (string.IsNullOrWhiteSpace(PswdEntry.Text))
                {
                    if (first)
                    {
                        if (string.IsNullOrWhiteSpace(PswdVerifyEntry.Text) && register)
                            s += ", ";
                        else
                            s += " and ";
                    }
                    s += "password";
                    first = true;
                }
                if (string.IsNullOrWhiteSpace(PswdVerifyEntry.Text) && register)
                {
                    if (first)
                        s += " and ";
                    s += "password verify";
                    //first = true;
                }

                ClearPswd();
                await DisplayAlert("Invalid input", s, "OK");
                return;
            }

            if (register && PswdEntry.Text != PswdVerifyEntry.Text)
            {
                ClearPswd();
                await DisplayAlert("Different passwords", "The passwords don't match each other", "OK");
                return;
            }

            try
            {
                User.u = new User(EmailEntry.Text);
            }
            catch(Exception exc)
			{
                await DisplayAlert("Error", exc.Message, "OK");

                ClearPswd();
                return;
			}

            PushView();
        }

		private void ChangeSignButton_Clicked(object sender, EventArgs e)
		{
            register = !register;

            ClearPswd();
            ChangeModeAction();
        }

		private async void LostAccButton_Clicked(object sender, EventArgs e)
		{
            await DisplayAlert("well", "tak to máš asi zatím smůlu", "OK");
		}

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry obj;
            try
            {
                obj = (Entry)sender;
            }
            catch
            {
                return;
            }

            string s = obj.Text;
            if (s.Length == 0)
                return;

            char c = s[s.Length - 1];

            if (!(char.IsLetterOrDigit(c) || (obj == EmailEntry && (c == '@' || c == '.'))))
            {
                obj.Text = s.Remove(s.Length - 1);
                //await DisplayAlert("Invalid character", "Use only letter and numbers", "OK");
            }
        }

        private async void IconToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InfoPage());
        }

        //

        /// <summary>
        /// called to get info about previous successuful logins
        /// </summary>
        private void SignFunc()
		{
            if (Application.Current.Properties.ContainsKey(registeredKey))
            {
                register = ((int)Application.Current.Properties[registeredKey] == 1);

                if (!register)
                    EmailEntry.Text = (string)Application.Current.Properties[loginNameKey];
            }
            else
            {
                register = true;
                Application.Current.Properties.Add(registeredKey, register ? 1 : 0);
            }
        }

        /// <summary>
        /// function changing numerous names after changing login/register mode
        /// </summary>
        private void ChangeModeAction()
		{
            if (register)
            {
                PswdVerifyEntry.IsVisible = true;
                ChangeSignButton.Text = "Login";
                Title = "Register";
            }
            else
            {
                PswdVerifyEntry.IsVisible = false;
                ChangeSignButton.Text = "Register";
                Title = "Login";
            }
        }

        /// <summary>
        /// clears text inside entry forms, handfull after login, to clear password inputs
        /// </summary>
        private void ClearPswd()
		{
            PswdEntry.Text = string.Empty;
            PswdVerifyEntry.Text = string.Empty;
		}

        private async void PushView()
		{
            if (register)
            {
                try
                {
                    User.u.Add();
                }
                catch (Exception exc)
                {
                    throw exc;
                }
            }
            else
            {
                try
                {
                    User.u.Select();
                    User.u.Classes.Select();

                    foreach (Class cls in User.u.Classes.List)
                    {
                        cls.Students.Select();
                        cls.Lessons.Select();
                    }
                }
                catch (Exception exc)
                {
                    throw exc;
                }
            }

            CalendarControl.cc = new CalendarControl();
            CalendarControl.cc.LoadAll();

            register = false;

            if (Application.Current.Properties.ContainsKey(registeredKey))
                Application.Current.Properties.Remove(registeredKey);
            Application.Current.Properties.Add(registeredKey, 0);

            if (Application.Current.Properties.ContainsKey(loginNameKey))
                Application.Current.Properties.Remove(loginNameKey);
            Application.Current.Properties.Add(loginNameKey, EmailEntry.Text);

            ChangeModeAction();

            if (!(Application.Current.MainPage is NavigationPage))
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                int currentLine = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber();
                throw new Exception(string.Format("file {0}, line {1}", currentFile, currentLine));
            }
            await Navigation.PushAsync(new Tabs.CalendarClassesTabs(), false);
        }

		private void DayNightToolbarItem_Clicked(object sender, EventArgs e)
		{
            DayNightHandle.DayNight.Swap();
		}

        private void ChangeLightMode()
		{
            if (DayNightHandle.DayNight.Day)
            {
                UserImage.Source = "UserDay.png";
                InfoToolbarItem.IconImageSource = "InfoDay.png";
                DayNightToolbarItem.IconImageSource = "Day.png";
            }
            else
            {
                UserImage.Source = "UserNight2.png";
                InfoToolbarItem.IconImageSource = "InfoNight.png";
                DayNightToolbarItem.IconImageSource = "Night.png";
            }
        }

	}
}