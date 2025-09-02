using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoManager
{
    class MusicVideo : Video
    {
        public string Artist { get; set; }
        public int Rating { get; set; }

        public MusicVideo(string title, int lenght, string artist, int rating): base(title, lenght)
        {
            Artist = artist;
            Rating = rating;
        }

        public override string ToString()
        {
            return $"De music video {Title} komt van de artist: {Artist}, duurt {Lenght} minuten en heeft een rate {Rating}.\n";
        }
    }
}
