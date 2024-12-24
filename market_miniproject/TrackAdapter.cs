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
        private Track tempTrack;
        private Dialog d;

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
            var item = _items[position];

            var view = convertView;
            if (view == null)
                view = LayoutInflater.From(_context).Inflate(Resource.Layout.individualItem, parent, false);

            var imgItemImage = view.FindViewById<ImageView>(Resource.Id.imgItemImage);
            var txtItemProps = view.FindViewById<TextView>(Resource.Id.txtItemProps);
            ImageButton moreInfoBtn = view.FindViewById<ImageButton>(Resource.Id.moreInfoBtn); // set up the button which shows the card is selected
            moreInfoBtn.Tag = position;
            moreInfoBtn.Click += MoreInfoBtn_Click;


            
            for (int i = 0; i < ProductsList.productsList.Count; i++)
            {
                if (ProductsList.productsList[i].ToString() == txtItemProps.Text)
                {
                    if (ProductsList.productsList[i] is ClassicalTrack)
                    {
                        imgItemImage.SetImageResource(Resource.Drawable.classics);
                        tempTrack = ProductsList.productsList[i];

                    }
                    else if (ProductsList.productsList[i] is JazzTrack)
                    {
                        imgItemImage.SetImageResource(Resource.Drawable.jazz);
                        tempTrack = ProductsList.productsList[i];
                    }
                    else // if (ProductsList.productsList[i] is RockTrack)
                    {
                        imgItemImage.SetImageResource(Resource.Drawable.rock);
                        tempTrack = ProductsList.productsList[i];
                    }
                }
            }

            txtItemProps.Text = item.TrackTitle + $" (by {item.Author})";

            return view;
        }

        private void MoreInfoBtn_Click(object sender, EventArgs e)
        {
            ImageButton clickedBtn = (ImageButton)sender;
            int position = (int)clickedBtn.Tag;
            ShowCurrentInfo(position);
        }
        private void ShowCurrentInfo(int position)
        {
            Track currentCard = ProductsList.productsList[position];
            var moreInfo_dialog = new Dialog(_context);
            moreInfo_dialog.SetContentView(Resource.Layout.individualInfo);
            var _moreInfoTxt = moreInfo_dialog.FindViewById<TextView>(Resource.Id.moreInfoTxt);
            //var currCardDialogLayout = moreInfo_dialog.FindViewById<LinearLayout>(Resource.Id.individual_card);

            if (currentCard != null)
            {
                _moreInfoTxt.Text = currentCard.ToString();
            }
            moreInfo_dialog.Show();
        }
    }
}