//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Firebase.Firestore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace market_miniproject.Classes
//{
//    public class OrderItemsEventListener : Java.Lang.Object, IEventListener
//    {
//        List<OrderInfo> ordersList;

//        public event EventHandler<OrderEventArgs> OnOrderRetrieved;//ארוע שאליו ירשמו כל מי שמעוניין לדעת שהיה שינוי באוסף ההזמנות
//        public class OrderEventArgs : EventArgs
//        {
//            internal List<OrderInfo> Orders { get; set; }
//        }
//        public OrderItemsEventListener()
//        {//בפעולה הבונה מבקשים שיאזין לאוסף ההזמנות 
//            FirebaseHelper.GetFirestore().Collection(Orders.ORDERS_COLLECTION_NAME).AddSnapshotListener(this);
//        }

//        public void OnEvent(Java.Lang.Object value, FirebaseFirestoreException error)
//        {//מביא לנו פיזית את כל הזמנות  כobject
//            var snapshot = (QuerySnapshot)value;//משמש כצילום של כל הזמנות בענן
//                                                //בנית רשימת הזמנות ריקה
//            this.ordersList = new List<Orders>();
//            //לולאה שעוברת על כל הזמנות
//            foreach (DocumentSnapshot item in snapshot.Documents)
//            {
//                Orders order = new Orders();
//                if (item.Get("totalPrice") != null)
//                {
//                    order.totalPrice = double.Parse(item.Get("totalPrice").ToString());
//                }
//                else
//                {
//                    order.totalPrice = 0;
//                }


//                if (item.Get("email") != null)
//                {
//                    order.email = item.Get("email").ToString();
//                }
//                else
//                {
//                    order.email = "";
//                }

//                if (item.Get("Id") != null)
//                {
//                    order.orderId = item.Get("Id").ToString();
//                }
//                else
//                {
//                    order.orderId = "";
//                }

//                if (item.Get("orderDate") != null)
//                {
//                    order.orderDate = Convert.ToDateTime(item.Get("orderDate").ToString());
//                }
//                else
//                {
//                    order.orderDate = Convert.ToDateTime("0");
//                }

//                this.ordersList.Add(order);

//            }
//            //בדיקה אם נרשמו לארוע כל מי שרוצה לקבל הודעה על שינוי הזמנות
//            if (this.OnOrderRetrieved != null)
//            {//יצירת עצם עם נתוני הארוע והכנסת הרשימה לשם
//                OrderEventArgs e = new OrderEventArgs();
//                e.Orders = this.ordersList;
//                //זימון כל הפעולות שנרשמו לארוע
//                OnOrderRetrieved.Invoke(this, e);
//            }
//        }
//    }
//}