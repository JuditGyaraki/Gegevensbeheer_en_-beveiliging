namespace CafeSportComposition_V1
{
    internal class Program
    {
        static private List<Drank> Dranken;
        static private List<Bestelling> Bestellingen;
        static void Main(string[] args)
        {
            string menuKeuze = "";

            Dranken = new List<Drank>
            {
                new Drank("Fanta", 2),
                new Drank("Fristi", 2.5F),
                new Drank("Chocomelk", 1.5F),
                new Drank("Koffie", 1),
                new Drank("Thee citroen", 1.75F),
                new Drank("Capri-Sun", 1)
            };

            Bestellingen = new List<Bestelling> { };

            while (menuKeuze != "q")
            {
                //Console.Clear();
                ToonList();
                Console.WriteLine();
                Console.WriteLine();
                ToonBestelling();
                Console.WriteLine();
                Console.WriteLine("Optie 1: Bestelling plaatsen\nOptie 2: Bestelling afronden\nOptie q: Programma beëindigen\n");
                Console.Write("Kies een optie (1, 2, q): ");
                menuKeuze = Console.ReadLine();
                if (menuKeuze == "1")
                {
                    Bestellen(Bestellingen);
                }
                else if (menuKeuze == "2")
                {
                    Afrekenen(Bestellingen);
                }
            }
        }

        static void ToonList()
        {
            Console.WriteLine("Cafè Sport");
            Console.WriteLine("************");
            int i = 0;
            for (int j = 0; j < Dranken.Count; j++)
            {
                i++;
                Console.WriteLine($"{i}. {Dranken[j].Naam}, {Dranken[j].Prijs} euro");
            }
        }

        static void ToonBestelling()
        {
            Console.WriteLine();
            Console.WriteLine();
            for (int t = 0; t < Bestellingen.Count; t++)
            {
                if (Bestellingen.Count == 0)
                {
                    Console.WriteLine("LEEG");
                }
                else
                {
                    Console.WriteLine(Bestellingen[t]);
                }
            }
        }

        static void Bestellen(List<Bestelling> bestellingen)
        {
            int tafelNummer = 0;
            int drankNummer = 0;

            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Geef tafelnummer: ");
            tafelNummer = int.Parse(Console.ReadLine());
            Console.WriteLine("");
            Console.Write("Geef dranknummer: ");
            drankNummer = int.Parse(Console.ReadLine());
            if (drankNummer > 0 && drankNummer < 7)
            {
                Console.WriteLine("");
                Console.WriteLine("");
                drankNummer--;
                int j = 0;

                for (j = 0; j <= drankNummer; j++)
                {
                    if (drankNummer == j)
                    {
                        bestellingen.Add(new Bestelling(tafelNummer, Dranken[j]));
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Foutieve invoer. Kies tussen 1 en 6");
                Console.WriteLine("");
                return;
            }
            
        }

        static void Afrekenen(List<Bestelling> bestellingen)
        {
            int tafelNummer = 0;
            float som = 0;
            string keuze = "";
            int tellerRemove = 0;

            Console.WriteLine("Geef tafelnummer: ");
            tafelNummer = int.Parse(Console.ReadLine());
            Console.WriteLine("");
            foreach (var item in bestellingen)
            {
                if (tafelNummer == item.Tafelnummer)
                {
                    Console.WriteLine($"Tafelnummer {item.Tafelnummer} heeft een {item.Drank.Naam} besteld.");
                    som = som + item.Drank.Prijs;
                }

            }
            Console.WriteLine($"Totaal: {som} euro.");
            Console.Write("Afrekenen? (Dit verwijderdt al de bestellingen van deze tafel. (y/n)) ");
            keuze = Console.ReadLine();
            if (keuze == "y")
            {
                foreach (var item in bestellingen.ToList())
                {
                    tellerRemove++;
                    if (tafelNummer == item.Tafelnummer)
                    {
                        bestellingen.Remove(item);
                    }
                }
            }
        }
    }
    
}
