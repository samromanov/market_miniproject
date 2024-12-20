using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace market_miniproject
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button loginBtn;
        TextView registerBtx,contactUsBtx;
        ImageView logoImg;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Init();
        }
        void Init()
        {
            loginBtn = FindViewById<Button>(Resource.Id.loginBtn);
            registerBtx = FindViewById<TextView>(Resource.Id.registerBtx);
            contactUsBtx = FindViewById<TextView>(Resource.Id.contactUsBtx);
            logoImg = FindViewById<ImageView>(Resource.Id.logoImg);

            // Underline the text using PaintFlags
            registerBtx.PaintFlags = registerBtx.PaintFlags | PaintFlags.UnderlineText;
            contactUsBtx.PaintFlags = contactUsBtx.PaintFlags | PaintFlags.UnderlineText;

            loginBtn.Click += LoginBtn_Click;
            registerBtx.Click += RegisterBtx_Click;
            contactUsBtx.Click += ContactUsBtx_Click;


        }

        private void ContactUsBtx_Click(object sender, System.EventArgs e)
        {
            var contactUs_dialog = new Dialog(this);
            contactUs_dialog.SetContentView(Resource.Layout.contactUs);
            var _phoneNum = FindViewById<TextView>(Resource.Id.phoneNum);
            var _email = FindViewById<TextView>(Resource.Id.email);
            var _sendSMS = FindViewById<TextView>(Resource.Id.sendSMS);

            contactUs_dialog.Show();
        }

        private void RegisterBtx_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(LoginRegister));
            intent.PutExtra("action", "register");
            StartActivity(intent);
            
        }

        private void LoginBtn_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this,typeof(LoginRegister));
            intent.PutExtra("action", "login");
            StartActivity(intent);

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}