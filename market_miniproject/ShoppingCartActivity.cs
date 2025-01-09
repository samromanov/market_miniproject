using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace market_miniproject
{
    [Activity(Label = "ShoppingCartActivity")]
    public class ShoppingCartActivity : Activity
    {
        private ImageButton _back_fromCart;
        private ListView _cart_listView;
        private TextView _totalPrice;
        private ShoppingCartAdapter_Track _cartAdapter;
        private Button checkOutBtn;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.shoppingCart);

            Init();
        }
        void Init()
        {
            _back_fromCart = FindViewById<ImageButton>(Resource.Id.back_fromCart);
            _cart_listView = FindViewById<ListView>(Resource.Id.cart_listView);
            _totalPrice = FindViewById<TextView>(Resource.Id.totalPriceTxt);
            _cart_listView = FindViewById<ListView>(Resource.Id.cart_listView);
            checkOutBtn = FindViewById<Button>(Resource.Id.checkOutBtn);

            //checkOutBtn.Click += CheckOutBtn_Click; //***work on it in the future***

            _back_fromCart.Click += _back_fromCart_Click;
            double total = 0;
            foreach (var item in ShoppingCartList.shoppingCartList) // go over all the products in cart to count the total price
            {
                total += item.Price;
            }
            _totalPrice.Text = total.ToString();

            _cartAdapter = new ShoppingCartAdapter_Track(this, _totalPrice, ShoppingCartList.shoppingCartList);
            _cart_listView.Adapter = _cartAdapter;

        }

        private void CheckOutBtn_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _back_fromCart_Click(object sender, EventArgs e)
        {
            Finish();
        }
    }
}