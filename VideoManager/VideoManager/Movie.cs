using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoManager
{
     class Movie : Video
    {
        public string Director {  get; set; }
        public int ReleaseYear { get; set; }
        public string Genre { get; set; }
        public int Rating { get; set; }
        public Movie(string title, int lenght, string director, int releaseYear, string genre, int rating) : base(title, lenght)

        {
            Director = director;
            ReleaseYear = releaseYear;
            Genre = genre;
            Rating = rating;
        }

        public override string ToString()
        {
            return $"De film {Title} is een {Genre} directed by {Director} in {ReleaseYear}, duurt {Lenght} minuten en heeft een rate {Rating}.\n";
        }
    }
}
