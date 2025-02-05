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
using Firebase.Firestore;
using Firebase.Auth;
using Firebase.Storage;
using Java.Util;
using System.Threading.Tasks;
using Android.Gms.Extensions;
using market_miniproject.Classes;

namespace market_miniproject.Classes
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Firebase Authentication and Firestore
        FirebaseAuth firebaseAuthentication;
        FirebaseFirestore database;

        public const string COLLECTION_NAME = "users";
        public const string CURRENT_USER_FILE = "currentUserFile";




        public User()
        {
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

        public async Task<bool> Login()
        {
            try
            {
                await this.firebaseAuthentication.SignInWithEmailAndPassword(this.Email, this.Password);
                return true;
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, $"Error: {ex.Message}", ToastLength.Short).Show();
                return false;
            }
        }

        public async Task<bool> Register()
        {
            try
            {
                // Create user in Firebase Authentication
                var userCredential = await firebaseAuthentication.CreateUserWithEmailAndPassword(this.Email, this.Password);
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, $"Error: {ex.Message}", ToastLength.Short).Show();
                return false;
            }
            try
            {
                HashMap userMap = new HashMap();
                //userMap.Put("fullName", this.Name);
                userMap.Put("email", this.Email);
                userMap.Put("password", this.Password);
                DocumentReference userReference = this.database.Collection(COLLECTION_NAME).Document(this.firebaseAuthentication.Uid);
                await userReference.Set(userMap);


                return true;
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, $"Error: {ex.Message}", ToastLength.Short).Show();
                return false;
            }
        }
    }
}