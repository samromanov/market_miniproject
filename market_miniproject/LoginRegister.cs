using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;

namespace market_miniproject
{
    [Activity(Label = "LoginRegister")]
    public class LoginRegister : Activity
    {
        ImageView _logoImg_login;
        TextView _loginTxt;
        LinearLayout _propertiesLayout;
        EditText _name, _username, _password;
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
            _name = FindViewById<EditText>(Resource.Id.name);
            _username = FindViewById<EditText>(Resource.Id.username);
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
                _name.Visibility = Android.Views.ViewStates.Gone;
            }
            else // In case of Register page (action == "register")
            {
                _loginTxt.Text = "Create new account";
                _loginBtn_login.Text = "Create account";
                _name.Visibility = Android.Views.ViewStates.Visible;
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void LoginBtn_login_Click(object sender, EventArgs e)
        {
            switch (Intent.GetStringExtra("action"))
            {
                case "login":
                    Intent intent = new Intent(this, typeof(Activity_homePage));
                    StartActivity(intent);
                    break;
                case "register":
                    Intent intent2 = new Intent(this, typeof(Activity_homePage));
                    StartActivity(intent2);
                    break;
            }
        }
    }
}