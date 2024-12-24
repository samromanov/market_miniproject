using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using market_miniproject.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace market_miniproject
{
    internal class TrackAdapter : BaseAdapter<Track>
    {

        private Context _context;
        private List<Track> _items;
        private Track positionTrack;

        public TrackAdapter(Context context, List<Track> items)
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
            var item = _items[position]; // item in the original list in the position

            var view = convertView;
            if (view == null)
                view = LayoutInflater.From(_context).Inflate(Resource.Layout.individualItem, parent, false);

            var _trackTypeImg_products = view.FindViewById<ImageView>(Resource.Id.trackTypeImg_products);
            var _trackTitle_products = view.FindViewById<TextView>(Resource.Id.trackTitle_products);
            var _trackAuthor_products = view.FindViewById<TextView>(Resource.Id.trackAuthor_products);
            var _itemPrice_products = view.FindViewById<TextView>(Resource.Id.itemPrice_products);           
            var _addToCartBtn = view.FindViewById<Button>(Resource.Id.addToCartBtn); // set up the button which shows adds the item to the shopping cart
            _addToCartBtn.Tag = position;
            _trackTypeImg_products.Tag = position;
            _addToCartBtn.Click += AddToCartBtn_Click;
            _trackTypeImg_products.Click += TrackTypeImg_products_Click;

            //// change the icon of the track
            //positionTrack = item; 
            //if (positionTrack is ClassicalTrack)
            //{
            //    _trackTypeImg_products.SetImageResource(Resource.Drawable.classics);

            //}
            //else if (positionTrack is JazzTrack)
            //{
            //    _trackTypeImg_products.SetImageResource(Resource.Drawable.jazz);
            //}
            //else // if (tempTrack is RockTrack)
            //{
            //    _trackTypeImg_products.SetImageResource(Resource.Drawable.rock);
            //}
            for (int i = 0; i < ProductsList.productsList.Count; i++) // change the icon of the track
            {
                positionTrack = ProductsList.productsList[i];
                if (positionTrack.Title == _trackTitle_products.Text && positionTrack.Author == _trackAuthor_products.Text) // found the track from the list in the position
                {
                    if (positionTrack is ClassicalTrack)
                    {
                        _trackTypeImg_products.SetImageResource(Resource.Drawable.classics);
                        break;

                    }
                    else if (positionTrack is JazzTrack)
                    {
                        _trackTypeImg_products.SetImageResource(Resource.Drawable.jazz);
                        break;
                    }
                    else // if (tempTrack is RockTrack)
                    {
                        _trackTypeImg_products.SetImageResource(Resource.Drawable.rock);
                        break;
                    }
                }
            }
            _trackTitle_products.Text = item.TrackTitle;
            _trackAuthor_products.Text = item.Author;
            _itemPrice_products.Text = item.Price.ToString();

            return view;
        }

        private void TrackTypeImg_products_Click(object sender, EventArgs e)
        {
            ImageView clickedImg = (ImageView)sender;
            int position = (int)clickedImg.Tag;
            Dialog infoDialog = new Dialog(_context);
            infoDialog.SetContentView(Resource.Layout.individualInfo);
            var _moreInfoTxt = infoDialog.FindViewById<TextView>(Resource.Id.moreInfoTxt);
            _moreInfoTxt.Text = positionTrack.ToString();

            infoDialog.Show();
        }

        private void AddToCartBtn_Click(object sender, EventArgs e)
        {
            Button clickedBtn = (Button)sender;
            int position = (int)clickedBtn.Tag;
            bool alreadyAdded = false; // if the item (track) I want to add is already added
            for (int i = 0; i < ShoppingCartList.shoppingCartList.Count; i++)
            {
                if (positionTrack == ShoppingCartList.shoppingCartList[i])
                {
                    alreadyAdded = true;
                    break;
                }
            }
            if (alreadyAdded == false)
            {
                ShoppingCartList.shoppingCartList.Add(positionTrack);
                Toast.MakeText(_context, $"Successfully added {positionTrack.TrackTitle}!", ToastLength.Short).Show();
            }
            else // if (alreadyAdded == true)
            {
                Toast.MakeText(_context, $"{positionTrack.TrackTitle} is already in the cart!", ToastLength.Short).Show();
            }
        }
    }
}