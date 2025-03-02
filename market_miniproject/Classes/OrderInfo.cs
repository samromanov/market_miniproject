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
using System.Text.Json;
using System.Text.Json.Serialization;


namespace market_miniproject.Classes
{
    public class OrderInfo
    {
		public string orderContent { get; set; }
		public double TotalPrice { get;  set; }
		public string OrderId { get;  set; }
		public DateTime OrderDate { get;  set; }
		public OrderInfo()
        {

        }
		public OrderInfo(string products, double totalPrice, string orderId, DateTime orderDate)
		{
			this.orderContent = products; // the products in the shopping cart
			this.TotalPrice = totalPrice; // the total price of the order
			this.OrderId = orderId; // the Id of the order
			this.OrderDate = orderDate; // the date of the order
		}
		
	}
}