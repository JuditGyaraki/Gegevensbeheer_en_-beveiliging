using System.Diagnostics;

namespace Gezelschapsspellenwinkel_De_Ork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Spel spel1 = new Spel();
            spel1.Naam = "Catan";
            spel1.Prijs = 41.99F;
            spel1.Promo = false;
            spel1.Voorraad = 2;
            spel1.Leeftijd = 10;

            Spel spel2 = new Spel();
            spel2.Naam = "Carcassonne";
            spel2.Prijs = 27.99F;
            spel2.Promo = true;
            spel2.Voorraad = 3;
            spel2.Leeftijd = 8;

            Spel spel3 = new Spel();
            spel3.Naam = "Uno";
            spel3.Prijs = 9.99F;
            spel3.Promo = false;
            spel3.Voorraad = 5;
            spel3.Leeftijd = 7;

            Spel spel4 = new Spel();
            spel4.Naam = "Scrabble";
            spel4.Prijs = 39.99F;
            spel4.Promo = false;
            spel4.Voorraad = 3;
            spel4.Leeftijd = 10;

            Spel spel5 = new Spel();
            spel5.Naam = "Pietjesbak";
            spel5.Prijs = 14.99F;
            spel5.Promo = true;
            spel5.Voorraad = 2;
            spel5.Leeftijd = 8;

            spel1.PrintSpel();
            spel2.PrintSpel();
            spel3.PrintSpel();
            spel4.PrintSpel();
            spel5.PrintSpel();


        }
    }

    class Spel
    {
        public string Naam;
        public float Prijs;
        public bool Promo;
        public int Voorraad;
        public int Leeftijd;

        public void PrintSpel()
        {
            if(Promo == false)
            {
                Console.WriteLine($"Het spel {Naam} kost {Prijs} euro en is te spelen vanaf {Leeftijd} jaar. Er zijn {Voorraad} spellen op voorraad en het staat niet in promo.");
            }
            else
            {
                Console.WriteLine($"Het spel {Naam} kost {Prijs} euro en is te spelen vanaf {Leeftijd} jaar. Er zijn {Voorraad} spellen op voorraad en het staat in promo.");
            }
            
        }
    }
}
