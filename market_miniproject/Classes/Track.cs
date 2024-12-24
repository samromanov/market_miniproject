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
    class Track : Activity
    {
        private string trackTitle;
        private string author;
        private int duration_s;
        public Track(string trackTitle,string author,int duration_s)
        {
            this.trackTitle = trackTitle;
            this.author = author;
            this.duration_s = duration_s;
        }
        public string TrackTitle
        {
            get { return this.trackTitle; }
            set { this.trackTitle = value; }
        }
        public string Author
        {
            get { return this.author; }
            set { this.author = value; }
        }
        public int Duration
        {
            get { return this.duration_s; }
            set { this.duration_s = value; }
        }
        public override string ToString() // virtual(?)
        {
            int totalSeconds = this.duration_s; // Total duration in seconds
            int minutes = totalSeconds / 60; // Calculate minutes
            int seconds = totalSeconds % 60; // Calculate remaining seconds

            if (minutes < 1) // if the duration is less than a minute (it will count only the seconds)
            {
                return $"Title: {this.trackTitle}\nAuthor: {this.author}\nDuration: {seconds}s";
            }
            return $"Title: {this.trackTitle}\nAuthor: {this.author}\nDuration: {minutes}:{seconds}s";
        }
    }
}