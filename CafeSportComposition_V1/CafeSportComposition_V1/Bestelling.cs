using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeSportComposition_V1
{
    public class Bestelling
    {
        public int Tafelnummer;
        public Drank Drank;


        public Bestelling(int tafelNummer, Drank drank)
        {
            Tafelnummer = tafelNummer;
            Drank = drank;
        }

        public override string ToString()
        {
            return $"Tafel {Tafelnummer} heeft een {Drank.Naam} besteld.";
        }
    }
}
