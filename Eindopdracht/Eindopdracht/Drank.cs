using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht
{
    internal class Drank
    {
        public string Naam { get; set; }
        public double Prijs { get; set; }

        public Drank(string naam, double prijs)
        {
            Naam = naam;
            Prijs = prijs;
        }

        public override string ToString()
        {
            return $"{Naam}, {Prijs}";
        }
    }


}
