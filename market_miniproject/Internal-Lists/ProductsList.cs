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

namespace market_miniproject.Internal_Lists
{
    internal class ProductsList
    {
        // Public List Property
        public List<string> productsLst; // { get; set; }

        // Constructor to initialize the list
        public ProductsList()
        {
            productsLst = new List<string>();
        }

        // Method to manipulate the list
        public void AddItem(string item)
        {
            productsLst.Add(item);
        }

    }
}