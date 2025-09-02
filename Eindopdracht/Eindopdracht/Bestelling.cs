using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht
{
    internal class Bestelling
    {
        public int TafelNr;
        public Drank Drank;
        public int Aantal;

        public Bestelling(Drank drank, int tafelNr)
        {
            Drank = drank;
            TafelNr = tafelNr;
            Aantal = 1;
        }

        public override string ToString()
        {
            return $"Tafelnummer {TafelNr} heeft {Aantal} {Drank} besteld. Prijs/stuk: {Math.Round(Drank.Prijs, 2)} euro. Prijs: {Drank.Prijs * Aantal} euro";
        }
    }
}
