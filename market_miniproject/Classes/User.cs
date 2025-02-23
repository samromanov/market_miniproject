using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.Widget;
using Firebase.Auth;
using Firebase.Firestore;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace market_miniproject.Classes
{
    public class User
    {
        public string Uid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Firebase Authentication and Firestore
        FirebaseAuth firebaseAuthentication;
        FirebaseFirestore database;

        public const string COLLECTION_NAME = "users"; // the collection of the users in the database (collection)
        public const string CURRENT_USER_FILE = "currentUserFile"; // the current user that uses the application
        public const string ORDERS_COLLECTION_NAME = "orders";



        public User()
        {
            this.firebaseAuthentication = FirebaseHelper.GetFirebaseAuthentication();
            this.database = FirebaseHelper.GetFirestore();
        }
        public User(string email)
        {
            this.Email = email;
            this.firebaseAuthentication = FirebaseHelper.GetFirebaseAuthentication();
            this.database = FirebaseHelper.GetFirestore();
        }
        public User(string name, string email, string password) // when registering
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.firebaseAuthentication = FirebaseHelper.GetFirebaseAuthentication();
            this.database = FirebaseHelper.GetFirestore();
        }
        public User(string email, string password) // when logging in
        {
            this.Email = email;
            this.Password = password;
            this.firebaseAuthentication = FirebaseHelper.GetFirebaseAuthentication();
            this.database = FirebaseHelper.GetFirestore();
        }


        public static void SaveUserInfo(string email, string password) // saves the current user info to the SharedPreference
        {
            // Get SharedPreferences Editor
            var editor = Application.Context.GetSharedPreferences(CURRENT_USER_FILE, FileCreationMode.Private).Edit();

            // Store user email and password
            editor.PutString("email", email);
            editor.PutString("password", password);

            // Apply changes
            editor.Apply();
        }

        public async Task<bool> GetUserByEmil()
        {
            try
            {
                var obj = await this.database.Collection(COLLECTION_NAME).WhereEqualTo("email", this.Email).Get();
                QuerySnapshot snapshot = (QuerySnapshot)obj;
                if (snapshot.Documents.Count>0)
                {
                    DocumentSnapshot item = snapshot.Documents[0];
                    if (item.Get("email") != null)
                    {
                        this.Email = item.Get("email").ToString();
                    }
                    else
                    {
                        this.Email = "";
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, $"Error: {ex.Message}", ToastLength.Short).Show();
                return false;
            }
            return true;
        }
        public async Task<bool> Login()
        {
            try  
            {
                await this.firebaseAuthentication.SignInWithEmailAndPassword(this.Email, this.Password);
                this.Uid = firebaseAuthentication.Uid;
                User.SaveUserInfo(this.Email, this.Password);

            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, $"Error: {ex.Message}", ToastLength.Short).Show();
                return false;
            }
            return true;
        }

        public async Task<bool> Logout()
        {
            try
            {
                await Task.Run(() =>
                {
                    var editor = Application.Context.GetSharedPreferences(CURRENT_USER_FILE, FileCreationMode.Private).Edit();
                    editor.PutString("email", "");
                    editor.PutString("password", "");
                    editor.Apply(); // Still doesn't return a Task, but now it's executed on a background thread
                });

                // If firebaseAuthentication has an asynchronous sign-out method, use it:
                // await firebaseAuthentication.SignOutAsync();

                // Otherwise, if it's synchronous:
                firebaseAuthentication.SignOut();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public async Task<bool> Register(bool isAdmin = false)
        {
            try
            {
                // Create user in Firebase Authentication
                await firebaseAuthentication.CreateUserWithEmailAndPassword(this.Email, this.Password);
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, $"Error: {ex.Message}", ToastLength.Short).Show();
                return false;
            }
            try
            {
                HashMap userMap = new HashMap();
                userMap.Put("userName", this.Name);
                userMap.Put("email", this.Email);
                userMap.Put("password", this.Password);
                userMap.Put("isAdmin", isAdmin);

                DocumentReference userReference = this.database.Collection(COLLECTION_NAME).Document(this.firebaseAuthentication.CurrentUser.Uid);
                // creates me the user with the same Uid as in the firebase authentication
                await userReference.Set(userMap);
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, $"Error: {ex.Message}", ToastLength.Short).Show();
                return false;
            }
            return true;
        }

        //public async Task<bool> FetchOrders()
        //{
        //    var previousOrders = new List<OrderInfo>();
        //    try
        //    {
        //        var ordersRef = database.Collection("orders").Document(this.firebaseAuthentication.CurrentUser.Uid).Collection("UserOrders");
        //        var snapshot = await ordersRef.Get().AsAsync<QuerySnapshot>();

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        public async Task<bool> Purchase(List<Track> cartList)
        {
            if (cartList == null || cartList.Count == 0)
            {
                Toast.MakeText(Application.Context, "Cart is empty. Cannot proceed with checkout.", ToastLength.Short).Show();
                return false;
            }

            // Generate Order ID (using timestamp)
            string orderId = DateTime.Now.ToString().Replace("/",".");

            // Calculate total price
            double totalPrice = 0;
            //foreach (var item in cartList)
            //{
            //    totalPrice += item.Price;
            //}
            cartList.ForEach(item => totalPrice += item.Price); // one liner

            // Create Order Data
            Dictionary<string, Java.Lang.Object> data = new Dictionary<string, Java.Lang.Object>
            {
                { "orderId", orderId }, // the date of the order
                { "email", Email },
                { "orderContent", ListToString(cartList) }, // string of the order's content (all the items "Title" and "Author")
                { "totalPrice", totalPrice }
            };

            // Save order to Firestore
            bool isOrderSaved = await PostOrder(data, orderId);

            if (isOrderSaved) // succeed
            {
                // Clear the shopping cart
                ShoppingCartList.shoppingCartList.Clear();
                //Console.WriteLine("Order placed successfully. Cart has been emptied.");
                return true;
            }
            else
            {
                Toast.MakeText(Application.Context, "Failed to place order.", ToastLength.Short).Show();
                return false;
            }
        }
        public async Task<bool> PostOrder(Dictionary<string, Java.Lang.Object> data, string orderId)
        {
            try
            {
                // Get current user reference
                DocumentReference userReference = this.database.Collection(ORDERS_COLLECTION_NAME).Document(this.firebaseAuthentication.CurrentUser.Uid + "/orders/" + orderId);

                // Create a new document with the ORDERID as id in the collection
                await userReference.Set(data);

                Toast.MakeText(Application.Context, "Order saved successfully.", ToastLength.Short).Show();
                return true;
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, $"Error: {ex.Message}", ToastLength.Short).Show();
                return false;
            }
        }

        public string ListToString(List<Track> list)
        {
            string info = "";
            foreach (var item in list)
            {
                string title = item.TrackTitle;
                string author = item.Author;
                info += $"Title: {title}, author: {author}\n";
            }
            return info;
        }
    }
}