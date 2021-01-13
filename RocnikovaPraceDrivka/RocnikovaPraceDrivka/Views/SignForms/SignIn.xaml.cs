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

namespace RocnikovaPraceDrivka.Views.SignForms
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignIn : ContentPage
	{
        private bool register;

        protected string registeredKey = "registerKey";
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

            base.OnAppearing();
		}

        //

		private async void SubmitButton_Clicked(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(EmailEntry.Text) 
                || string.IsNullOrWhiteSpace(PswdEntry.Text) 
                || (string.IsNullOrWhiteSpace(PswdVerifyEntry.Text) && register))
			{
                bool first = false;
                string s = "Please enter ";

                if (string.IsNullOrWhiteSpace(EmailEntry.Text)) {
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
                    first = true;
                }

                await DisplayAlert("Invalid input", s, "OK");
			}

            if (register && PswdEntry.Text != PswdVerifyEntry.Text)
            {
                await DisplayAlert("Different passwords", "The passwords don't match each other", "OK");
            }

			if (!IsValidEmail(EmailEntry.Text))
            {
                await DisplayAlert("Invalid email", "Please enter email in valid form", "OK");
            }
			else
            {
                User user = new User(EmailEntry.Text);

                if (register)
                    user.Add();
                else
                    user.Load();

                register = false;

                if (Application.Current.Properties.ContainsKey(registeredKey))
                    Application.Current.Properties.Remove(registeredKey);
                Application.Current.Properties.Add(registeredKey, 0);

                if (Application.Current.Properties.ContainsKey(loginNameKey))
                    Application.Current.Properties.Remove(loginNameKey);
                Application.Current.Properties.Add(loginNameKey, EmailEntry.Text);

                ChangeModeAction();

                if (Application.Current.MainPage is NavigationPage)
                    await (Application.Current.MainPage as NavigationPage).PushAsync(new Calendar());
            }
            ClearPswd();
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

        //

        private void SignFunc()
		{
            if (Application.Current.Properties.ContainsKey(registeredKey))
            {
                register = ((int)Application.Current.Properties[registeredKey] == 1) ? true : false;

                if (!register)
                    EmailEntry.Text = (string)Application.Current.Properties[loginNameKey];
            }
            else
            {
                register = true;
                Application.Current.Properties.Add(registeredKey, register ? 1 : 0);
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private void ChangeModeAction()
		{
            if (register)
            {
                PswdVerifyEntry.IsVisible = true;
                ChangeSignButton.Text = "Login";
            }
            else
            {
                PswdVerifyEntry.IsVisible = false;
                ChangeSignButton.Text = "Register";
            }
        }

        private void ClearPswd()
		{
            PswdEntry.Text = string.Empty;
            PswdVerifyEntry.Text = string.Empty;
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
	}
}