using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Fragment.App;
using Google.Android.Material.BottomNavigation;
using System;
using System.Linq;
using market_miniproject.Classes;
using System.Threading.Tasks;

namespace market_miniproject
{
    [Activity(Label = "MainPageActivity")]
    public class MainPageActivity : AppCompatActivity
    {
        private string logInEmail, logInPassword, logInJoinDate;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main_page);

            logInEmail = Intent.GetStringExtra("email");
            logInPassword = Intent.GetStringExtra("password");
            logInJoinDate = Intent.GetStringExtra("joinDate");

            Toast.MakeText(this, $"Welcome {logInEmail}", ToastLength.Short).Show();

            var bottomNavigationView = FindViewById<BottomNavigationView>(Resource.Id.bottomNavigationView);

            // Load the default fragment
            LoadFragment(new HomeFragment());

            bottomNavigationView.NavigationItemSelected += (s, e) =>
            {
                AndroidX.Fragment.App.Fragment selectedFragment = null;

                switch (e.Item.ItemId)
                {
                    case Resource.Id.nav_home:
                        selectedFragment = new HomeFragment();
                        break;
                    case Resource.Id.nav_products:
                        selectedFragment = new ProductsFragment();
                        break;
                    case Resource.Id.nav_cart:
                        selectedFragment = new CartFragment();
                        break;
                    case Resource.Id.nav_account:
                        selectedFragment = new AccountFragment();
                        break;
                    case Resource.Id.nav_more:
                        selectedFragment = new MoreFragment();
                        break;
                }

                if (selectedFragment != null)
                {
                    LoadFragment(selectedFragment);
                }
            };
        }

        private void LoadFragment(AndroidX.Fragment.App.Fragment fragment)
        {
            if (fragment is AccountFragment accountFragment)
            {
                Bundle bundle = new Bundle();
                bundle.PutString("email", logInEmail);
                bundle.PutString("password", logInPassword);
                bundle.PutString("joinDate", logInJoinDate);
                accountFragment.Arguments = bundle;
                
            }
            if (fragment is CartFragment cartFragment)
            {
                Bundle bundle = new Bundle();
                bundle.PutString("email", logInEmail);
                cartFragment.Arguments = bundle;
            }
            if (fragment is MoreFragment moreFragment)
            {
                Bundle bundle = new Bundle();
                bundle.PutString("email", logInEmail);
                bundle.PutString("password", logInPassword);
                bundle.PutString("joinDate", logInJoinDate);
                moreFragment.Arguments = bundle;
            }

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.frameLayout, fragment)
                .Commit();
        }
    }

    //  Fragment Classes
    public class HomeFragment : AndroidX.Fragment.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Initialize data here


        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_home, container, false);
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

        }
    }

    public class ProductsFragment : AndroidX.Fragment.App.Fragment
    {
        private TrackAdapter _productsAdapter;
        private ListView _products_listview;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Initialize data here
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_products, container, false);
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _products_listview = view.FindViewById<ListView>(Resource.Id.products_listView);

            _productsAdapter = new TrackAdapter(Context, ProductsList.productsList);
            _products_listview.Adapter = _productsAdapter;

            
        }
    }

    public class CartFragment : AndroidX.Fragment.App.Fragment
    {
        private Dialog checkOutDialog , previousOrdersDialog;
        private ListView _cart_listView;
        private TextView _totalPrice;
        private ShoppingCartAdapter_Track _cartAdapter;
        private Button checkOutBtn, prevOrdersBtn;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Initialize data here


        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_cart, container, false);
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _cart_listView = view.FindViewById<ListView>(Resource.Id.cart_listView);
            _totalPrice = view.FindViewById<TextView>(Resource.Id.totalPriceTxt);
            _cart_listView = view.FindViewById<ListView>(Resource.Id.cart_listView);
            checkOutBtn = view.FindViewById<Button>(Resource.Id.checkOutBtn);
            prevOrdersBtn = view.FindViewById<Button>(Resource.Id.previousOrdersBtn);

            prevOrdersBtn.Click += PrevOrdersBtn_Click;
            checkOutBtn.Click += CheckOutBtn_Click; //***work on it in the future***

            double total = 0;
            foreach (var item in ShoppingCartList.shoppingCartList) // go over all the products in cart to count the total price
            {
                total += item.Price;
            }
            _totalPrice.Text = total.ToString() + "$";

            _cartAdapter = new ShoppingCartAdapter_Track(Context, _totalPrice, ShoppingCartList.shoppingCartList);
            _cart_listView.Adapter = _cartAdapter;
        }

        private void PrevOrdersBtn_Click(object sender, EventArgs e)
        {
            previousOrdersDialog = new Dialog(Context);
            previousOrdersDialog.SetContentView(Resource.Layout.previousOrders_page);
            var previousOrders_listView = previousOrdersDialog.FindViewById<ListView>(Resource.Id.prevOrdersLv);

            previousOrdersDialog.Show(); 
        }

        private void CheckOutBtn_Click(object sender, EventArgs e)
        {
            checkOutDialog = new Dialog(Context);
            checkOutDialog.SetContentView(Resource.Layout.Checkout_page);
            var checkOutTotalPriceTxt = checkOutDialog.FindViewById<TextView>(Resource.Id.checkOutTotalPrice);
            var buyNowBtn = checkOutDialog.FindViewById<Button>(Resource.Id.buyNowBtn);
            checkOutTotalPriceTxt.Text = _totalPrice.Text + "$";
            buyNowBtn.Click -= BuyNowBtn_ClickAsync;
            buyNowBtn.Click += BuyNowBtn_ClickAsync;
            var email = Arguments?.GetString("email");
            checkOutDialog.Show();
        }

        private async void BuyNowBtn_ClickAsync(object sender, EventArgs e)
        {
            var email = Arguments?.GetString("email");
            User user = new User(email);
            try
            {
                if (await user.Purchase(ShoppingCartList.shoppingCartList) == true)
                {
                    Toast.MakeText(Application.Context, "Order placed successfully. Cart has been emptied.", ToastLength.Short).Show();
                    checkOutDialog.Dismiss();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(Context, $"Error: {ex.Message}", ToastLength.Short).Show();
            }

        }
    }

    public class AccountFragment : AndroidX.Fragment.App.Fragment
    {
        ImageView persAccountImg;
        TextView accountUsernameTxt, accountDateJoinedTxt;
        Button logOutBtn;
        Dialog logOutOrNotDialog;
        string email, password, dateJoined;
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Initialize data here
            

        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_account, container, false);
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            persAccountImg = view.FindViewById<ImageView>(Resource.Id.persAccountImg);
            accountUsernameTxt = view.FindViewById<TextView>(Resource.Id.accountUsernameTxt);
            accountDateJoinedTxt = view.FindViewById<TextView>(Resource.Id.accountDateJoinedTxt);
            logOutBtn = view.FindViewById<Button>(Resource.Id.logOutBtn);

            // Retrieve arguments from MainPageActivity
            email = Arguments?.GetString("email", "Guest") ?? "Guest"; // if the email didn't pass, it will display "Guest" instead of the email
            password = Arguments?.GetString("password");
            dateJoined = Arguments?.GetString("joinDate");
            accountUsernameTxt.Text = $"Email: {email}";
            accountDateJoinedTxt.Text = $"Date joined: {dateJoined}";

            logOutBtn.Click -= LogOutBtn_Click;
            logOutBtn.Click += LogOutBtn_Click;
        }

        private void LogOutBtn_Click(object sender, EventArgs e)
        {
            logOutOrNotDialog = new Dialog(Context);
            logOutOrNotDialog.SetContentView(Resource.Layout.removeFromCart);
            var yesOrNoTxt = logOutOrNotDialog.FindViewById<TextView>(Resource.Id.yesOrNoTxt);
            var _dontLogout = logOutOrNotDialog.FindViewById<Button>(Resource.Id.dontRemoveBtn);
            var _logOut = logOutOrNotDialog.FindViewById<Button>(Resource.Id.removeBtn);
            yesOrNoTxt.Text = "Want to log out?";
            _dontLogout.Click -= (s, args) => logOutOrNotDialog.Dismiss();
            _dontLogout.Click += (s, args) => logOutOrNotDialog.Dismiss(); // cancel -> closes the dialog
            _logOut.Click -= _logOut_Click;
            _logOut.Click += _logOut_Click; ; // agree -> logs out

            logOutOrNotDialog.Show();

        }

        private async void _logOut_Click(object sender, EventArgs e)
        {
            User user = new User(email, password);
            try
            {
                if (await user.Logout() == true)
                {
                    logOutOrNotDialog.Dismiss();
                    Toast.MakeText(Context, "Logout successfully", ToastLength.Short).Show();
                    Intent intent = new Intent(Activity, typeof(LoginRegister));
                    intent.AddFlags(ActivityFlags.ClearTask | ActivityFlags.NewTask);
                    StartActivity(intent);
                    Activity?.Finish();
                }
                else
                {
                    Toast.MakeText(Context, "Logout failed", ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(Context, ex.Message, ToastLength.Short).Show();
            }
        }
    }

    public class MoreFragment : AndroidX.Fragment.App.Fragment
    {
        private Button _addNewTrackBtn, _aboutUsBtn, _settingsBtn;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Initialize data here
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_more, container, false);
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _addNewTrackBtn = view.FindViewById<Button>(Resource.Id.addNewTrackBtn);
            _aboutUsBtn = view.FindViewById<Button>(Resource.Id.aboutUsBtn);
            _settingsBtn = view.FindViewById<Button>(Resource.Id.settingsBtn);

            _settingsBtn.Click += _settingsBtn_Click;
            _addNewTrackBtn.Click += _addNewTrackBtn_Click;
            _aboutUsBtn.Click += _aboutUsBtn_Click;
        }

        private void _settingsBtn_Click(object sender, EventArgs e)
        {
            Intent settingsIntent = new Intent(Context, typeof(SettingsPageActivity));
            StartActivity(settingsIntent);
        }

        private void _aboutUsBtn_Click(object sender, System.EventArgs e)
        {
            var aboutUs_dialog = new Dialog(Context);
            aboutUs_dialog.SetContentView(Resource.Layout.aboutUs_individual);
            // Can be done both dinamically or manually in the XML...
            var _scrollView_aboutUs = aboutUs_dialog.FindViewById<ScrollView>(Resource.Id.scrollView_aboutUs); // in case is I'll want to make changes
            var _aboutUsTxt = aboutUs_dialog.FindViewById<TextView>(Resource.Id.aboutUsTxt);

            aboutUs_dialog.Show();
        }

        private void _addNewTrackBtn_Click(object sender, System.EventArgs e)
        {
            Intent addTrackIntent = new Intent(Context, typeof(AddNewTrack_Activity));
            StartActivity(addTrackIntent);

        }
    }

}
