using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3
{
    public class Movie
    {
        public string Naam { get; set; }
        public int ReleaseJaar { get; set; }
        public string Genre { get; set; }
        public string Regisseur { get; set; }
        public int Speelduur { get; set; }

        public Movie(string naam, int releaseJaar, string genre, string regisseur, int speelduur)
        {
            Naam = naam;
            ReleaseJaar = releaseJaar;
            Genre = genre;
            Regisseur = regisseur;
            Speelduur = speelduur;
        }
        public override string ToString()
        {
            return $"Titel: {Naam} - Released: {ReleaseJaar} - Genre: {Genre} - Director: {Regisseur} - Duration: {Speelduur}";
        }
    }
}
