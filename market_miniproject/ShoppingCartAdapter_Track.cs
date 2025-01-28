using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using market_miniproject.Classes;
using System;
using System.Collections.Generic;

namespace market_miniproject
{
    class ShoppingCartAdapter_Track : BaseAdapter<Track>
    {

        private Context _context;
        private List<Track> _items;
        private TextView _totalPrice;

        //save removeOrNot dialog
        private Dialog removeOrNotDialog;
        public ShoppingCartAdapter_Track(Context context, TextView totalPrice, List<Track> items) // for example (this, ShoppingCartList.shoppingCartList)
        {
            this._context = context;
            this._items = items;
            this._totalPrice = totalPrice;
        }


        public override Track this[int position]
        {
            get { return _items[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        public override int Count
        {
            get { return _items.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _items[position]; // item from the original list in the position

            var view = convertView;
            if (view == null)
                view = LayoutInflater.From(_context).Inflate(Resource.Layout.individualItem, parent, false);

            var _trackIcon_cart = view.FindViewById<ImageView>(Resource.Id.trackTypeImg_products);
            var _trackTitle_cart = view.FindViewById<TextView>(Resource.Id.trackTitle_products);
            var _trackAuthor_cart = view.FindViewById<TextView>(Resource.Id.trackAuthor_products);
            var _itemPrice_cart = view.FindViewById<TextView>(Resource.Id.itemPrice_products);
            var _removeFromCartBtn = view.FindViewById<Button>(Resource.Id.addToCartBtn);
            //var _itemsAmount = view.FindViewById<TextView>(Resource.Id.itemsAmount_cart);
            //var _minus = view.FindViewById<ImageButton>(Resource.Id.minus_cart);
            //var _plus = view.FindViewById<ImageButton>(Resource.Id.plus_cart);
            _trackIcon_cart.Tag = position;
            //_minus.Tag = position;
            //_plus.Tag = position;
            _trackIcon_cart.Click -= TrackTypeImg_cart_Click;
            _trackIcon_cart.Click += TrackTypeImg_cart_Click;
            //_minus.Click -= Minus_Click;
            //_minus.Click += Minus_Click;
            //_plus.Click -= Plus_Click;
            //_plus.Click += Plus_Click;
            _removeFromCartBtn.Click -= _removeFromCartBtn_Click;
            _removeFromCartBtn.Click += _removeFromCartBtn_Click;
            _removeFromCartBtn.Tag = position;

            _removeFromCartBtn.Text = "Remove";
            _removeFromCartBtn.TextSize = 10f;
            // Change the background tint dynamically
            var color = Color.ParseColor("#FF0000"); // Red color
            _removeFromCartBtn.BackgroundTintList = ColorStateList.ValueOf(color);


            double _updatedTotalPrice = 0;
            foreach (var tempItem in ShoppingCartList.shoppingCartList)
            {
                _updatedTotalPrice += tempItem.Price;
            }
            _totalPrice.Text = _updatedTotalPrice.ToString();


            _trackIcon_cart.SetImageResource(item.ImageId);
            _trackTitle_cart.Text = item.TrackTitle;
            _trackAuthor_cart.Text = item.Author;
            _itemPrice_cart.Text = $"{item.Price}$";
            //_itemsAmount.Text = 1.ToString();

            return view;
        }

        private void _removeFromCartBtn_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int position = (int)clickedButton.Tag;
            removeOrNotDialog = new Dialog(_context);
            removeOrNotDialog.SetContentView(Resource.Layout.removeFromCart);
            var _dontRemoveBtn = removeOrNotDialog.FindViewById<Button>(Resource.Id.dontRemoveBtn);
            var _removeBtn = removeOrNotDialog.FindViewById<Button>(Resource.Id.removeBtn);
            _removeBtn.Tag = position;
            _dontRemoveBtn.Click -= (s, args) => removeOrNotDialog.Dismiss();
            _dontRemoveBtn.Click += (s, args) => removeOrNotDialog.Dismiss(); // cancel -> closes the dialog
            _removeBtn.Click -= _RemoveBtn_Click;
            _removeBtn.Click += _RemoveBtn_Click; // agree -> removes current item from cart

            removeOrNotDialog.Show();
        }
        private void _RemoveBtn_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int position = (int)clickedButton.Tag;
            var item = _items[position];
            foreach (var product in ShoppingCartList.shoppingCartList)
            {
                if (product == item)
                {
                    ShoppingCartList.shoppingCartList.Remove(product);
                    break;
                }
            }
            removeOrNotDialog.Dismiss();
            double _updatedTotalPrice = 0;
            foreach (var tempItem in ShoppingCartList.shoppingCartList)
            {
                _updatedTotalPrice += tempItem.Price;
            }
            _totalPrice.Text = _updatedTotalPrice.ToString();
            NotifyDataSetChanged();
            Toast.MakeText(_context, $"{item.Title} Successfully removed!", ToastLength.Short).Show();
        }

        //private void Plus_Click(object sender, EventArgs e)
        //{
        //    ImageButton clickedBtn = (ImageButton)sender;
        //    int position = (int)clickedBtn.Tag;
        //    var item = _items[position]; // Find the item for the current position
        //    //var parent = (View)clickedBtn.Parent; // Get the parent view (the row) containing the button
        //    var rootView = clickedBtn.RootView; // Retrieve the root view of the hierarchy
        //    var _itemsAmount = rootView.FindViewById<TextView>(Resource.Id.itemsAmount_cart); // the current amount of the same item
        //    var _itemPrice_cart = rootView.FindViewById<TextView>(Resource.Id.itemPrice_cart); // the current price reagrding the amount of items

        //    double originalPrice = 0;// initally 0 until item is found
        //    foreach (var product in ProductsList.productsList)
        //    {
        //        if (product.TrackTitle == item.TrackTitle && product.Author == item.Author) // if the position item in the products list is found
        //        {
        //            originalPrice = product.Price;
        //            break;
        //        }
        //    }
        //    if (originalPrice == 0)
        //    {
        //        Toast.MakeText(_context, $"The product doesn't exists anymore!", ToastLength.Short).Show();
        //        ShoppingCartList.shoppingCartList.Remove(item);
        //        NotifyDataSetChanged();
        //        Toast.MakeText(_context, $"Product successfully removed!", ToastLength.Short).Show();
        //    }
        //    else
        //    {
        //        //visuals
        //        int currentAmount = int.Parse(_itemsAmount.Text); // the current amount(for example 1)
        //        currentAmount += 1;
        //        double updatedPrice = currentAmount * originalPrice;
        //        _itemPrice_cart.Text = updatedPrice.ToString() + "$"; // update the current price after adding
        //        _itemsAmount.Text = currentAmount.ToString(); // update the current amount after adding
        //        double updatedTotalPrice = 0;
        //        foreach (var tempItem in ShoppingCartList.shoppingCartList)
        //        {
        //            updatedTotalPrice += tempItem.Price;
        //        }
        //        _totalPrice.Text = updatedTotalPrice.ToString(); // update the total price text view
        //        NotifyDataSetChanged();

        //        //functionality
        //        item.Price = updatedPrice;
        //    }   
        //}

        //private void Minus_Click(object sender, EventArgs e)
        //{
        //    ImageButton clickedBtn = (ImageButton)sender;
        //    int position = (int)clickedBtn.Tag;
        //    var item = _items[position]; // Find the item for the current position            
        //    var rootView = clickedBtn.RootView;
        //    var _itemsAmount = rootView.FindViewById<TextView>(Resource.Id.itemsAmount_cart);
        //    var _itemPrice_cart = rootView.FindViewById<TextView>(Resource.Id.itemPrice_cart);

        //    //Visuals
        //    int currentAmount = int.Parse(_itemsAmount.Text); // the current amount (for example 1)
        //    double originalPrice = 0;// initally 0 until item is found
        //    foreach (var product in ProductsList.productsList)
        //    {
        //        if (product.TrackTitle == item.TrackTitle && product.Author == item.Author) // if the position item in the products list is found
        //        {
        //            originalPrice = product.Price;
        //            break;
        //        }
        //    }
        //    if (originalPrice == 0)
        //    {
        //        Toast.MakeText(_context, $"The product doesn't exists anymore!", ToastLength.Short).Show();
        //        ShoppingCartList.shoppingCartList.Remove(item);
        //        NotifyDataSetChanged();
        //        Toast.MakeText(_context, $"Product successfully removed!", ToastLength.Short).Show();
        //    }
        //    if (currentAmount == 1) // if I click minus when the amount is 1, it will ask me to remove the item from cart
        //    {
        //        removeOrNotDialog = new Dialog(_context);
        //        removeOrNotDialog.SetContentView(Resource.Layout.removeFromCart);
        //        var _dontRemoveBtn = removeOrNotDialog.FindViewById<Button>(Resource.Id.dontRemoveBtn);
        //        var _removeBtn = removeOrNotDialog.FindViewById<Button>(Resource.Id.removeBtn);
        //        _removeBtn.Tag = position;

        //        _dontRemoveBtn.Click -= (s, args) => removeOrNotDialog.Dismiss();
        //        _dontRemoveBtn.Click += (s, args) => removeOrNotDialog.Dismiss(); // cancel -> closes the dialog
        //        _removeBtn.Click -= _RemoveBtn_Click;
        //        _removeBtn.Click += _RemoveBtn_Click; // agree -> removes current item from cart

        //        removeOrNotDialog.Show();
        //    }
        //    else // current amount != 1
        //    {
        //        //visuals
        //        //int updatedAmount = currentAmount - 1; // after clicking "-", items amount decreases by one 
        //        currentAmount -= 1;
        //        double updatedPrice = currentAmount * originalPrice; // the price after subtracting the amount by one
        //        _itemsAmount.Text = currentAmount.ToString(); // update items amount after subtracting
        //        _itemPrice_cart.Text = updatedPrice.ToString() + "$"; // update item price after subtracting
        //        double updatedTotalPrice = 0;
        //        foreach (var tempItem in ShoppingCartList.shoppingCartList)
        //        {
        //            updatedTotalPrice += tempItem.Price;
        //        }
        //        _totalPrice.Text = updatedTotalPrice.ToString(); // update the total price
        //        NotifyDataSetChanged();

        //        //functionality
        //        item.Price = updatedPrice;
        //    }
        //}



        private void TrackTypeImg_cart_Click(object sender, EventArgs e)
        {
            ImageView clickedImg = (ImageView)sender;
            int position = (int)clickedImg.Tag;
            var item = _items[position];
            Dialog infoDialog = new Dialog(_context);
            infoDialog.SetContentView(Resource.Layout.individualInfo);
            var _moreInfoTxt = infoDialog.FindViewById<TextView>(Resource.Id.moreInfoTxt);
            _moreInfoTxt.Text = item.ToString();

            infoDialog.Show();
        }
        public void ExitCartFunction()
        {

        }
    }
}