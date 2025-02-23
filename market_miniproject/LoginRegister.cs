using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using market_miniproject.Classes;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;

namespace market_miniproject
{
    [Activity(Label = "LoginRegister")]
    public class LoginRegister : Activity
    {
        ImageView _logoImg_login;
        TextView _loginTxt;
        LinearLayout _propertiesLayout;
        EditText _name, _email, _password;
        Button _loginBtn_login, _cancelBtn;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.loginRegister_page);

            Init();
        }
        void Init()
        {
            _logoImg_login = FindViewById<ImageView>(Resource.Id.logoImg_login);
            _loginTxt = FindViewById<TextView>(Resource.Id.loginTxt);
            _propertiesLayout = FindViewById<LinearLayout>(Resource.Id.propertiesLayout);
            //_name = FindViewById<EditText>(Resource.Id.name);
            _email = FindViewById<EditText>(Resource.Id.username);
            _password = FindViewById<EditText>(Resource.Id.password);
            _loginBtn_login = FindViewById<Button>(Resource.Id.loginBtn_login);
            _cancelBtn = FindViewById<Button>(Resource.Id.cancelBtn);

            //------------------------------------------------------------------------------------

            _loginBtn_login.Click += LoginBtn_login_Click;
            _cancelBtn.Click += CancelBtn_Click;

            //------------------------------------------------------------------------------------

            string action = Intent.GetStringExtra("action");

            if (action == "login") // In case of Login page
            {
                _loginTxt.Text = "Login";
                _loginBtn_login.Text = "Login";
                //_name.Visibility = Android.Views.ViewStates.Gone;
            }
            else // In case of Register page (action == "register")
            {
                _loginTxt.Text = "Create new account";
                _loginBtn_login.Text = "Create account";
                //_name.Visibility = Android.Views.ViewStates.Visible;
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private async void LoginBtn_login_Click(object sender, EventArgs e)
        {
            switch (Intent.GetStringExtra("action"))
            {
                case "login":
                    if (_email.Text == "" || _password.Text == "") 
                    {
                        Toast.MakeText(this, "Empty fields detected", ToastLength.Short).Show();
                    }
                    else // empty fieleds not detected
                    {
                        try
                        {
                            User user = new User(_email.Text, _password.Text);
                            if (await user.Login() == true)
                            {
                                Toast.MakeText(this, "You're successfully logged in", ToastLength.Short).Show();
                                Intent intent = new Intent(this, typeof(MainPageActivity));
                                intent.PutExtra("email", _email.Text);
                                intent.PutExtra("password", _password.Text);     
                                StartActivityForResult(intent, 100);
                                //StartActivity(intent);
                            }
                            else
                            {
                                Toast.MakeText(this, "Wrong email or password", ToastLength.Short).Show();
                            }
                        }
                        catch (Exception ex)
                        {
                            Toast.MakeText(this, $"Error: {ex.Message}", ToastLength.Short).Show();
                        }
                    }
                    
                    break;
                case "register":
                    if (_email.Text == "" || _password.Text == "")
                    {
                        Toast.MakeText(this, "Empty fields detected", ToastLength.Short).Show();
                    }
                    else if (!_email.Text.Contains("@")) // if the email doesn't contains "@" in it
                    {
                        Toast.MakeText(this, "An email must contain (@)", ToastLength.Short).Show();
                    }
                    else if (_password.Text.Length < 8)// if the password contains less than 8 characters
                    {
                        Toast.MakeText(this, "A password must contain at least 8 characters", ToastLength.Short).Show();
                    }
                    else // empty fieleds not detected
                    {
                        try
                        {
                            User user = new User(_email.Text, _password.Text);
                            if (await user.Register() == true)
                            {

                                Toast.MakeText(this, "You're successfully registered", ToastLength.Short).Show();
                                Intent intent = new Intent(this, typeof(MainPageActivity));
                                intent.PutExtra("email", _email.Text);
                                intent.PutExtra("password", _password.Text);
                                intent.PutExtra("joinDate", DateTime.Now.ToString("dd/MM/yyyy"));
                                StartActivityForResult(intent, 200);
                            }
                            else
                            {
                                Toast.MakeText(this, "Username or email already exists", ToastLength.Short).Show();
                            }
                        }
                        catch (Exception ex)
                        {
                            Toast.MakeText(this, $"Error: {ex.Message}", ToastLength.Short).Show();
                        }
                    }
                    break;
            }
        }
    }
}