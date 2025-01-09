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

namespace market_miniproject.Classes
{
    class OtherTrack : Track
    {
        private string genre;
        public OtherTrack(int imageId, string title, string author, int duration, double price, string genre) : base(imageId, title, author, duration, price)
        {
            this.genre = genre;
        }
        public string Genre
        {
            get { return this.genre; }
            set { this.genre = value; }
        }
        public override string ToString()
        {
            return base.ToString() + $"\nGenre: {this.genre}";
        }
    }
}