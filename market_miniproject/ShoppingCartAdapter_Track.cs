using Android.App;
using Android.Content;
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
        private Track positionTrack;

        public ShoppingCartAdapter_Track(Context context, List<Track> items)
        {
            this._context = context;
            this._items = items;
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
                view = LayoutInflater.From(_context).Inflate(Resource.Layout.cart_individualItem, parent, false);

            var _trackTypeImg_cart = view.FindViewById<ImageView>(Resource.Id.trackTypeImg_cart);
            var _trackTitle_cart = view.FindViewById<TextView>(Resource.Id.trackTitle_cart);
            var _trackAuthor_cart = view.FindViewById<TextView>(Resource.Id.trackAuthor_cart);
            var _itemPrice_cart = view.FindViewById<TextView>(Resource.Id.itemPrice_cart);
            var _itemsAmount = view.FindViewById<TextView>(Resource.Id.itemsAmount_cart);
            var _minus = view.FindViewById<ImageButton>(Resource.Id.minus_cart);
            var _plus = view.FindViewById<ImageButton>(Resource.Id.plus_cart);
            _trackTypeImg_cart.Tag = position;
            _minus.Tag = position;
            _plus.Tag = position;
            _trackTypeImg_cart.Click += TrackTypeImg_cart_Click;
            _minus.Click += Minus_Click;
            _plus.Click += Plus_Click;

            // change the icon of the track
            positionTrack = item;
            if (positionTrack is ClassicalTrack)
            {
                _trackTypeImg_cart.SetImageResource(Resource.Drawable.classics);
            }
            else if (positionTrack is JazzTrack)
            {
                _trackTypeImg_cart.SetImageResource(Resource.Drawable.jazz);
            }
            else // if (tempTrack is RockTrack)
            {
                _trackTypeImg_cart.SetImageResource(Resource.Drawable.rock);
            }
            //for (int i = 0; i < ProductsList.productsList.Count; i++) // change the icon of the track
            //{
            //    positionTrack = ProductsList.productsList[i];
            //    if (positionTrack.Title == _trackTitle_cart.Text && positionTrack.Author == _trackAuthor_cart.Text) // found the track from the list in the position
            //    {
            //        if (positionTrack is ClassicalTrack)
            //        {
            //            _trackTypeImg_cart.SetImageResource(Resource.Drawable.classics);
            //            break;

            //        }
            //        else if (positionTrack is JazzTrack)
            //        {
            //            _trackTypeImg_cart.SetImageResource(Resource.Drawable.jazz);
            //            break;
            //        }
            //        else // if (tempTrack is RockTrack)
            //        {
            //            _trackTypeImg_cart.SetImageResource(Resource.Drawable.rock);
            //            break;
            //        }
            //    }
            //}
            _trackTitle_cart.Text = item.TrackTitle;
            _trackAuthor_cart.Text = item.Author;
            _itemPrice_cart.Text = (item.Price * int.Parse(_itemsAmount.Text)).ToString() + "$"; // total price = price * amount

            return view;
        }

        private void Plus_Click(object sender, EventArgs e)
        {
            ImageButton clickedBtn = (ImageButton)sender;
            int position = (int)clickedBtn.Tag;
            var item = positionTrack; // Find the item for the current position            
            var parent = (View)clickedBtn.Parent; // Get the parent view (the row) containing the button

            // Find the TextView for the item amount within the parent view 
            var _itemsAmount = parent.FindViewById<TextView>(Resource.Id.itemsAmount_cart);
            var _itemPrice_cart = parent.FindViewById<TextView>(Resource.Id.itemPrice_cart);
            int currentAmount = int.Parse(_itemsAmount.Text); // for example 1
            int added = currentAmount++; // now it will be 2

            _itemPrice_cart.Text = added.ToString();

            //Toast.MakeText(_context, $"Successfully added!", ToastLength.Short).Show();
        }

        private void Minus_Click(object sender, EventArgs e)
        {
            ImageButton clickedBtn = (ImageButton)sender;
            int position = (int)clickedBtn.Tag;
            var item = positionTrack; // Find the item for the current position            
            var parent = (View)clickedBtn.Parent; // Get the parent view (the row) containing the button

            // Find the TextView for the item amount within the parent view 
            var _itemsAmount = parent.FindViewById<TextView>(Resource.Id.itemsAmount_cart);
            var _itemPrice_cart = parent.FindViewById<TextView>(Resource.Id.itemPrice_cart);

            if (int.Parse(_itemsAmount.Text) == 1)
            {
                Dialog removeOrNotDialog = new Dialog(_context);
                removeOrNotDialog.SetContentView(Resource.Layout.removeFromCart);
                var _dontRemoveBtn = removeOrNotDialog.FindViewById<Button>(Resource.Id.dontRemoveBtn);
                var _removeBtn = removeOrNotDialog.FindViewById<Button>(Resource.Id.removeBtn);

                _dontRemoveBtn.Click += (s, args) => removeOrNotDialog.Dismiss();
                _removeBtn.Click += _RemoveBtn_Click;

                removeOrNotDialog.Show();
            }
            else
            {
                int currentAmount = int.Parse(_itemsAmount.Text); // for example 5
                int decreased = currentAmount--; // now it will be 4

                _itemPrice_cart.Text = decreased.ToString();
            }
        }

        private void _RemoveBtn_Click(object sender, EventArgs e)
        {
            var item = positionTrack;
            for (int i = 0; i < ShoppingCartList.shoppingCartList.Count; i++)
            {
                if (item == ShoppingCartList.shoppingCartList[i])
                {
                    ShoppingCartList.shoppingCartList.Remove(ShoppingCartList.shoppingCartList[i]);
                    break;
                }
            }
            Toast.MakeText(_context, $"Successfully removed!", ToastLength.Short).Show();
        }

        private void TrackTypeImg_cart_Click(object sender, EventArgs e)
        {
            ImageView clickedImg = (ImageView)sender;
            int position = (int)clickedImg.Tag;
            Dialog infoDialog = new Dialog(_context);
            infoDialog.SetContentView(Resource.Layout.individualInfo);
            var _moreInfoTxt = infoDialog.FindViewById<TextView>(Resource.Id.moreInfoTxt);
            _moreInfoTxt.Text = positionTrack.ToString();

            infoDialog.Show();
        }
    }
}