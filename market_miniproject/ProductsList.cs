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
        public static List<Track> productsList = new List<Track>
        {
            new ClassicalTrack(Resource.Drawable.classics_icon,"Nocturne 1","Fredric Chopin",300,9.99,"1830","Nocturne"),
            new JazzTrack(Resource.Drawable.jazz_icon, "What a Wonderful World", "Louis Armstrong", 180, 18.67, "Trumpet", 15),
            new RockTrack(Resource.Drawable.rock_icon, "Bohemian Rhapsody", "Queen", 355, 30.99, "Progressive Rock", "Guilt and Redemption"),
            new OtherTrack(Resource.Drawable.other1_icon, "My Life", "Avraham Avinu", 180, 8.50, "Metal"),
            new ClassicalTrack(Resource.Drawable.classics_icon, "Symphony No.9", "Ludwig Wan Beethoven", 300, 9.99, "1824", "Symphony"),
            new JazzTrack(Resource.Drawable.jazz_icon, "Hit the Road Jack", "Ray Charles", 140, 28.97, "Piano", 25),
            new RockTrack(Resource.Drawable.rock_icon, "Paint It Black", "The Rolling Stones", 183, 11.99, "Psychedelic Rock", "Grief and Despair"),
            new OtherTrack(Resource.Drawable.other3_icon, "Life Could Be a Dream", "The Chords", 145, 33.50, "Soft Jazz"),
            new ClassicalTrack(Resource.Drawable.classics_icon, "Ballade No.1", "Sam Romanov", 300, 39.99, "2023", "Ballade"),
            new JazzTrack(Resource.Drawable.jazz_icon, "Giant Steps", "John Coltrane", 283, 12.87, "Saxophone", 150),
            new RockTrack(Resource.Drawable.rock_icon, "Johnny B. Goode", "Chuck Berry", 355, 30.99, "Rock and Roll", "Guilt and Redemption"),
            new OtherTrack(Resource.Drawable.other5_icon, "The First Rain", "Sam Romanov", 180, 8.50, "Classical"),
            new OtherTrack(Resource.Drawable.other2_icon, "Life Is a Highway", "Rascal Flats", 145, 7.99,"Rock"),
            new OtherTrack(Resource.Drawable.other4_icon, "N.Y. State of Mind", "Nas", 150, 20.55, "Rap")
        };

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
        public static void RemoveItem(Track item)
        {
            productsList.Remove(item);
        }

    }
}