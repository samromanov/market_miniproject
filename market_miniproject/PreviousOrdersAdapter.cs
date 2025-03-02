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
using market_miniproject.Classes;

namespace market_miniproject
{
    class PreviousOrdersAdapter : BaseAdapter<OrderInfo>
    {

        private Context _context;
        private List<OrderInfo> _items;


        public PreviousOrdersAdapter(Context context, List<OrderInfo> itemsList)
        {
            this._context = context;
            this._items = itemsList;
        }
        public override OrderInfo this[int position]
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
                view = LayoutInflater.From(_context).Inflate(Resource.Layout.individualOrder, parent, false);

            var _orderDateAndTime = view.FindViewById<TextView>(Resource.Id.orderDateAndTime);
            var _orderTotalPrice = view.FindViewById<TextView>(Resource.Id.orderTotalPrice);
            var _orderContentTxt = view.FindViewById<TextView>(Resource.Id.orderContentTxt);

            _orderDateAndTime.Text = item.OrderDate.ToString();
            _orderTotalPrice.Text = item.TotalPrice.ToString() + "$";
            _orderContentTxt.Text = item.orderContent;
            return view;
        }
    }   
}