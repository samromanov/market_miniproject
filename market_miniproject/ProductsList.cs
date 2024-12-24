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
    internal static class ProductsList
    {
        // Public List Property
        public static List<Track> productsList = new List<Track>();

        // Constructor to initialize the list
        //public ProductsList()
        //{
        //    productsLst = new List<string>();
        //}

        // Method to manipulate the list
        public static void AddItem(Track item)
        {
            productsList.Add(item);
        }

    }
}