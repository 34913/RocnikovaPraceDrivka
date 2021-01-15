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

            base.OnAppearing();
		}

        //

        // systems calling

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

                if (!(Application.Current.MainPage is NavigationPage)) {
                    string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    int currentLine = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber();
                    throw new Exception(string.Format("file {0}, line {1}", currentFile, currentLine));
                }
                await Navigation.PushAsync(new RocnikovaPraceDrivka.Tabs.CalendarClassesTabs(user));
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

        /// <summary>
        /// Verify if given string is email
        /// </summary>
        /// <param name="email">string of email form to verify</param>
        /// <returns>true if email is truly functional email</returns>
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

	}
}