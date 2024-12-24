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
    class RockTrack : Track
    {
        private string subGenre; // "Punk", "Metal", etc.
        private string lyricsTheme; // "Love", "Rebellion", etc.
        public RockTrack(string songName, string bandName, int duration, double price, string subGenre, string lyricsTheme) : base(songName, bandName, duration, price)
        {
            this.subGenre = subGenre;
            this.lyricsTheme = lyricsTheme;
        }
        public string SubGenre
        {
            get { return this.subGenre; }
            set { this.subGenre = value; }
        }
        public string LyricsTheme
        {
            get { return this.lyricsTheme; }
            set { this.lyricsTheme = value; }
        }
        public override string ToString()
        {
            return base.ToString() + $"\nSub Genre: {this.subGenre}\nLyrics Theme{this.lyricsTheme}";
        }
    }
}