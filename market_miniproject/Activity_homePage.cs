using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using market_miniproject.Classes;

namespace market_miniproject
{
    [Activity(Label = "Activity_homePage")]
    public class Activity_homePage : Activity
    {
        private TrackAdapter _adapter;

        private TabHost _tabHost;

        // Products
        private ImageButton _shopping_cartBtn;
        private ListView _products_listview;
        // History

        // Account

        // More
        private Button _addNewTrackBtn, _aboutUsBtn;
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
            //var homeTabIndicator = LayoutInflater.From(this).Inflate(Resource.Layout.tab_indicator, null);
            //homeTabIndicator.FindViewById<ImageView>(Resource.Id.tabIcon).SetImageResource(Resource.Drawable.home);
            //homeTabIndicator.FindViewById<TextView>(Resource.Id.tabLabel).Text = "Home";
            homeTab.SetIndicator("Home");
            homeTab.SetContent(Resource.Id.homeTab_content); // if it's error, check the home_page.xml
            _tabHost.AddTab(homeTab);

            // Set up Products Tab
            var productsTab = _tabHost.NewTabSpec("productsTab");
            //var productsTabIndicator = LayoutInflater.From(this).Inflate(Resource.Layout.tab_indicator, null);
            //productsTabIndicator.FindViewById<ImageView>(Resource.Id.tabIcon).SetImageResource(Resource.Drawable.products);
            //productsTabIndicator.FindViewById<TextView>(Resource.Id.tabLabel).Text = "Products";
            productsTab.SetIndicator("Products");
            productsTab.SetContent(Resource.Id.productsTab_content); // if it's error, check the home_page.xml
            _tabHost.AddTab(productsTab);

            //// Set up History Tab
            //var historyTab = _tabHost.NewTabSpec("historyTab");
            ////var historyTabIndicator = LayoutInflater.From(this).Inflate(Resource.Layout.tab_indicator, null);
            ////historyTabIndicator.FindViewById<ImageView>(Resource.Id.tabIcon).SetImageResource(Resource.Drawable.history);
            ////historyTabIndicator.FindViewById<TextView>(Resource.Id.tabLabel).Text = "History";
            //historyTab.SetIndicator("History");
            //historyTab.SetContent(Resource.Id.historyTab_content); // if it's error, check the home_page.xml
            //_tabHost.AddTab(historyTab);

            //// Set up Account Tab
            //var accountTab = _tabHost.NewTabSpec("accountTab");
            ////var accountTabIndicator = LayoutInflater.From(this).Inflate(Resource.Layout.tab_indicator, null);
            ////accountTabIndicator.FindViewById<ImageView>(Resource.Id.tabIcon).SetImageResource(Resource.Drawable.user);
            ////accountTabIndicator.FindViewById<TextView>(Resource.Id.tabLabel).Text = "Account";
            //accountTab.SetIndicator("Account");
            //accountTab.SetContent(Resource.Id.accountTab_content); // if it's error, check the home_page.xml
            //_tabHost.AddTab(accountTab);

            // Set up More Tab
            var moreTab = _tabHost.NewTabSpec("moreTab");
            //var moreTabIndicator = LayoutInflater.From(this).Inflate(Resource.Layout.tab_indicator, null);
            //moreTabIndicator.FindViewById<ImageView>(Resource.Id.tabIcon).SetImageResource(Resource.Drawable.more);
            //moreTabIndicator.FindViewById<TextView>(Resource.Id.tabLabel).Text = "More";
            moreTab.SetIndicator("More");
            moreTab.SetContent(Resource.Id.moreTab_content); // if it's error, check the home_page.xml
            _tabHost.AddTab(moreTab);

            //----------------------------------------------------------------------------------

            // Finding views by ID
            _shopping_cartBtn = FindViewById<ImageButton>(Resource.Id.shopping_cartBtn);

            // Home Tab


            // Products tab
            ClassicalTrack track1 = new ClassicalTrack(Resource.Drawable.classics_icon,"Nocturne 1","Chopin",300,9.99,"Modern Era","Nocturne");
            JazzTrack track2 = new JazzTrack(Resource.Drawable.jazz_icon, "What a Wonderful World", "Louis Armstrong", 180, 18.67, "Trumpet", 15);
            RockTrack track3 = new RockTrack(Resource.Drawable.rock_icon, "Bohemian Rhapsody", "Queen", 355, 30.99, "Progressive Rock", "Guilt and Redemption");
            OtherTrack track4 = new OtherTrack(Resource.Drawable.other1_icon, "My Life", "Avraham Avinu", 180, 8.50, "Metal");
            ClassicalTrack track5 = new ClassicalTrack(Resource.Drawable.classics_icon, "Nocturne 1", "Chopin", 300, 9.99, "Modern Era", "Nocturne");
            JazzTrack track6 = new JazzTrack(Resource.Drawable.jazz_icon, "What a Wonderful World", "Louis Armstrong", 180, 18.67, "Trumpet", 15);
            RockTrack track7 = new RockTrack(Resource.Drawable.rock_icon, "Bohemian Rhapsody", "Queen", 355, 30.99, "Progressive Rock", "Guilt and Redemption");
            OtherTrack track8 = new OtherTrack(Resource.Drawable.other1_icon, "My Life", "Avraham Avinu", 180, 8.50, "Metal");
            ClassicalTrack track9 = new ClassicalTrack(Resource.Drawable.classics_icon, "Nocturne 1", "Chopin", 300, 9.99, "Modern Era", "Nocturne");
            JazzTrack track10 = new JazzTrack(Resource.Drawable.jazz_icon, "What a Wonderful World", "Louis Armstrong", 180, 18.67, "Trumpet", 15);
            RockTrack track11 = new RockTrack(Resource.Drawable.rock_icon, "Bohemian Rhapsody", "Queen", 355, 30.99, "Progressive Rock", "Guilt and Redemption");
            OtherTrack track12 = new OtherTrack(Resource.Drawable.other1_icon, "My Life", "Avraham Avinu", 180, 8.50, "Metal");



            _products_listview = FindViewById<ListView>(Resource.Id.products_listView);
            _adapter = new TrackAdapter(this, ProductsList.productsList);
            _products_listview.Adapter = _adapter;


            // History tab



            // Account Tab



            // More Tab
            _addNewTrackBtn = FindViewById<Button>(Resource.Id.addNewTrackBtn);
            _aboutUsBtn = FindViewById<Button>(Resource.Id.aboutUsBtn);

            _addNewTrackBtn.Click += _addNewTrackBtn_Click;
            _aboutUsBtn.Click += _aboutUsBtn_Click;


            //----------------------------------------------------------------------------------

            // Making buttons
            _shopping_cartBtn.Click += _shopping_cartBtn_Click;

        }

        private void _aboutUsBtn_Click(object sender, System.EventArgs e)
        {
            var aboutUs_dialog = new Dialog(this);
            aboutUs_dialog.SetContentView(Resource.Layout.aboutUs_individual);
            // Can be done both dinamically or manually in the XML...
            var _scrollView_aboutUs = FindViewById<ScrollView>(Resource.Id.scrollView_aboutUs);
            var _aboutUsTxt = FindViewById<TextView>(Resource.Id.aboutUsTxt);

            aboutUs_dialog.Show();
        }

        private void _addNewTrackBtn_Click(object sender, System.EventArgs e)
        {
            Intent addTrackIntent = new Intent(this, typeof(AddNewTrack_Activity));
            StartActivity(addTrackIntent);

        }

        private void _shopping_cartBtn_Click(object sender, System.EventArgs e)
        {
            
            Intent cartIntent = new Intent(this, typeof(ShoppingCartActivity));
            StartActivity(cartIntent);
        }
    }
}