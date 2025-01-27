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

namespace market_miniproject
{
    [Activity(Label = "MainPageActivity")]
    public class MainPageActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main_page);

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
            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.frameLayout, fragment)
                .Commit();
        }
    }

    // Example Fragment Classes
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

            //prevOrdersBtn.Click += PrevOrdersBtn_Click;
            //checkOutBtn.Click += CheckOutBtn_Click; //***work on it in the future***

            double total = 0;
            foreach (var item in ShoppingCartList.shoppingCartList) // go over all the products in cart to count the total price
            {
                total += item.Price;
            }
            _totalPrice.Text = total.ToString();

            _cartAdapter = new ShoppingCartAdapter_Track(Context, _totalPrice, ShoppingCartList.shoppingCartList);
            _cart_listView.Adapter = _cartAdapter;
        }

        private void PrevOrdersBtn_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CheckOutBtn_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    public class AccountFragment : AndroidX.Fragment.App.Fragment
    {
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


        }
    }

    public class MoreFragment : AndroidX.Fragment.App.Fragment
    {
        private Button _addNewTrackBtn, _aboutUsBtn;
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

            _addNewTrackBtn.Click += _addNewTrackBtn_Click;
            _aboutUsBtn.Click += _aboutUsBtn_Click;
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
