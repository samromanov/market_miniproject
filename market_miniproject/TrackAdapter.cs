﻿using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
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

        public TrackAdapter(Context context, List<Track> items)
        {
            this._context = context;
            this._items = items;
        }


        public override Track this[int position]
        {
            get { return _items[position]; }
        }

        public void Clear()
        {
            _items.Clear();
            NotifyDataSetChanged();
        }

        public void AddAll(IEnumerable<Track> newItems)
        {
            _items.AddRange(newItems);
            NotifyDataSetChanged();
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
            _addToCartBtn.Click -= AddToCartBtn_Click;
            _addToCartBtn.Click += AddToCartBtn_Click;
            _trackTypeImg_products.Click -= TrackTypeImg_products_Click;
            _trackTypeImg_products.Click += TrackTypeImg_products_Click;


            _addToCartBtn.Text = "Add";
            // Change the background tint dynamically
            var color = Color.ParseColor("#008000"); // Green color
            _addToCartBtn.BackgroundTintList = ColorStateList.ValueOf(color);

            _trackTypeImg_products.SetImageResource(item.ImageId); // change the icon of the track             
            _trackTitle_products.Text = item.TrackTitle;
            _trackAuthor_products.Text = item.Author;
            _itemPrice_products.Text = item.Price.ToString() + "$";

            return view;
        }

        private void TrackTypeImg_products_Click(object sender, EventArgs e)
        {
            ImageView clickedImg = (ImageView)sender;
            int position = (int)clickedImg.Tag;
            var item = _items[position]; // item in the original list in the position
            Dialog infoDialog = new Dialog(_context);
            infoDialog.SetContentView(Resource.Layout.individualInfo);
            var _moreInfoTxt = infoDialog.FindViewById<TextView>(Resource.Id.moreInfoTxt);
            _moreInfoTxt.Text = item.ToString();

            infoDialog.Show();
        }

        private void AddToCartBtn_Click(object sender, EventArgs e)
        {
            Button clickedBtn = (Button)sender;
            int position = (int)clickedBtn.Tag;
            var item = _items[position].ShallowCopy(); // item in the original list in the position
            bool alreadyAdded = false; // if the item (track) I want to add is already added
            foreach (var product in ShoppingCartList.shoppingCartList)
            {
                if (product.TrackTitle == item.TrackTitle && product.Author == item.Author) // found the item in the cart list
                {
                    alreadyAdded = true;
                    break;
                }
            }
            if (alreadyAdded == false)
            {
                ShoppingCartList.shoppingCartList.Add(item);
                Toast.MakeText(_context, $"Successfully added {item.TrackTitle}!", ToastLength.Short).Show();
            }
            else // if (alreadyAdded == true)
            {
                Toast.MakeText(_context, $"{item.TrackTitle} is already in the cart!", ToastLength.Short).Show();
            }
        }
    }
}
