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
    class JazzTrack : Track
    {
        private string leadInstrument; // "Piano", "Guitar", "Saxophone", etc.
        private int soloDuration; // Duration of solos in seconds
        public JazzTrack(string pieceTitle,string author,int duration,string leadInstrument, int soloDuration) :base(pieceTitle, author, duration)
        {
            this.leadInstrument = leadInstrument;
            this.soloDuration = soloDuration;
        }
        public string LeadInstrument
        {
            get { return this.leadInstrument; }
            set { this.leadInstrument = value; }
        }
        public int SoloDuration
        {
            get { return this.soloDuration; }
            set { this.soloDuration = value; }
        }
        public override string ToString()
        {
            int totalSeconds = this.soloDuration; // Total duration in seconds
            int minutes = totalSeconds / 60; // Calculate minutes
            int seconds = totalSeconds % 60; // Calculate remaining seconds

            if (minutes < 1) // if the duration is less than a minute
            {
                return base.ToString() + $"\nLead Instrument: {this.leadInstrument}\nSolo Duration: {seconds}s";
            }
            return base.ToString()+$"\nLead Instrument: {this.leadInstrument}\nSolo Duration: {minutes}:{seconds}s";
        }

    }
}