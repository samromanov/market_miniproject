using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace market_miniproject
{
    [Activity(Label = "Activity_homePage")]
    public class Activity_homePage : Activity
    {
        private TrackAdapter _adapter;

        private TabHost _tabHost;

        private ImageButton _shopping_cartBtn;
        private ListView _products_listview;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.home_page);

            Init();
        }

        void Init()
        {
            // Initialize TabHost
            _tabHost = FindViewById<TabHost>(Resource.Id.tabHost);
            _tabHost.Setup();

            // Set up Home Tab 
            var homeTab = _tabHost.NewTabSpec("HomeTab");
            var homeTabIndicator = LayoutInflater.From(this).Inflate(Resource.Layout.tab_indicator, null);
            homeTabIndicator.FindViewById<ImageView>(Resource.Id.tabIcon).SetImageResource(Resource.Drawable.home);
            homeTabIndicator.FindViewById<TextView>(Resource.Id.tabLabel).Text = "Home";
            homeTab.SetIndicator(homeTabIndicator);
            homeTab.SetContent(Resource.Id.homeTab_content); // if it's error, check the home_page.xml
            _tabHost.AddTab(homeTab);

            // Set up Products Tab
            var productsTab = _tabHost.NewTabSpec("productsTab");
            var productsTabIndicator = LayoutInflater.From(this).Inflate(Resource.Layout.tab_indicator, null);
            productsTabIndicator.FindViewById<ImageView>(Resource.Id.tabIcon).SetImageResource(Resource.Drawable.products);
            productsTabIndicator.FindViewById<TextView>(Resource.Id.tabLabel).Text = "Products";
            productsTab.SetIndicator(productsTabIndicator);
            productsTab.SetContent(Resource.Id.productsTab_content); // if it's error, check the home_page.xml
            _tabHost.AddTab(productsTab);

            // Set up History Tab
            var historyTab = _tabHost.NewTabSpec("historyTab");
            var historyTabIndicator = LayoutInflater.From(this).Inflate(Resource.Layout.tab_indicator, null);
            historyTabIndicator.FindViewById<ImageView>(Resource.Id.tabIcon).SetImageResource(Resource.Drawable.history);
            historyTabIndicator.FindViewById<TextView>(Resource.Id.tabLabel).Text = "History";
            historyTab.SetIndicator(historyTabIndicator);
            historyTab.SetContent(Resource.Id.historyTab_content); // if it's error, check the home_page.xml
            _tabHost.AddTab(historyTab);

            // Set up Account Tab
            var accountTab = _tabHost.NewTabSpec("accountTab");
            var accountTabIndicator = LayoutInflater.From(this).Inflate(Resource.Layout.tab_indicator, null);
            accountTabIndicator.FindViewById<ImageView>(Resource.Id.tabIcon).SetImageResource(Resource.Drawable.user);
            accountTabIndicator.FindViewById<TextView>(Resource.Id.tabLabel).Text = "Account";
            accountTab.SetIndicator(accountTabIndicator);
            accountTab.SetContent(Resource.Id.accountTab_content); // if it's error, check the home_page.xml
            _tabHost.AddTab(accountTab);

            // Set up More Tab
            var moreTab = _tabHost.NewTabSpec("moreTab");
            var moreTabIndicator = LayoutInflater.From(this).Inflate(Resource.Layout.tab_indicator, null);
            moreTabIndicator.FindViewById<ImageView>(Resource.Id.tabIcon).SetImageResource(Resource.Drawable.more);
            moreTabIndicator.FindViewById<TextView>(Resource.Id.tabLabel).Text = "More";
            moreTab.SetIndicator(moreTabIndicator);
            moreTab.SetContent(Resource.Id.moreTab_content); // if it's error, check the home_page.xml
            _tabHost.AddTab(moreTab);

            //----------------------------------------------------------------------------------

            // Finding views by ID
            _shopping_cartBtn = FindViewById<ImageButton>(Resource.Id.shopping_cartBtn);

            // Home Tab


            // Products tab
            _products_listview = FindViewById<ListView>(Resource.Id.products_listView);
            _adapter = new TrackAdapter(this, ProductsList.productsList);
            _products_listview.Adapter = _adapter;
            

            // History tab

            //----------------------------------------------------------------------------------

            // Making buttons
            _shopping_cartBtn.Click += _shopping_cartBtn_Click;

        }

        private void _shopping_cartBtn_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}