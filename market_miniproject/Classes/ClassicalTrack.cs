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
    class ClassicalTrack : Track
    {
        private string era; // "Baroque", "Romantic", "Modern", etc.
        private string type; // "Waltz", "Minuet", "Tango", "March", "Ballade", etc.
        public ClassicalTrack(string pieceTitle, string composer, int duration,double price, string era, string type) : base(pieceTitle, composer, duration, price)
        {
            this.era = era;
            this.type = type;
        }
        public string Era
        {
            get { return this.era; }
            set { this.era = value; }
        }
        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        public override string ToString()
        {
            return base.ToString() +$"\nEra: {this.era}\nType: {this.type}"; 
        }
    }
}