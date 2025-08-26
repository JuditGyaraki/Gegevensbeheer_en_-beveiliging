using System.Runtime.Serialization;

namespace RefactoringOppervlakteBerekenen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //test
            string keuze = "";
            while (keuze != "q")
            {
                keuze = ToonMenuEnVraagInput();

                if (keuze == "1")
                {
                    OppervlakteRechthoek();
                }
                else if (keuze == "2")
                {
                    OppervlakteDriehoek();
                }
                else if (keuze == "3")
                {
                    OppervlakteCirkel();
                }
                else if (keuze == "q")
                {
                    Console.WriteLine("Tot de volgende keer!");
                }
                else
                {
                    Console.WriteLine("Ongeldige keuze");
                    Console.Write("Druk enter om terug te gaan: ");
                    string enter = Console.ReadLine();
                    if (enter == "")
                    {
                        Console.Clear();
                    }
                }
            }
        }

        static string ToonMenuEnVraagInput()
        {
            Console.WriteLine("OPPERVLAKTE BEREKENEN");
            Console.WriteLine("=====================");
            Console.WriteLine("1. Bereken oppervlakte rechthoek");
            Console.WriteLine("2. Bereken oppervlakte driehoek");
            Console.WriteLine("3. Bereken oppervlakte cirkel");
            Console.WriteLine("q. Stop");
            Console.WriteLine("");
            Console.Write("Jouw keuze (1, 2, 3 of q): ");
            Console.Write("");
            string keuze = Console.ReadLine();
            return keuze;
        }

        static void OppervlakteRechthoek()
        {
            Console.WriteLine();
            Console.WriteLine("OPPERVLAKTE RECHTHOEK BEREKENEN");
            Console.WriteLine("===============================");
            Console.Write("Geef de breedte in cm: ");
            double breedte = double.Parse(Console.ReadLine());

            Console.Write("Geef de lengte in cm: ");
            double lengte = double.Parse(Console.ReadLine());

            Console.Write("De oppervlakte van de rechthoek is ");
            Console.Write(breedte * lengte);
            Console.Write(" cm².");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Druk enter om terug te gaan...");
            string enter = Console.ReadLine();
            if (enter == "")
                Console.Clear();
        }
        static void OppervlakteDriehoek()
        {
            Console.WriteLine();
            Console.WriteLine("OPPERVLAKTE DRIEHOEK BEREKENEN");
            Console.WriteLine("===============================");
            Console.Write("Geef de basis in cm: ");
            double basis = double.Parse(Console.ReadLine());

            Console.Write("Geef de hoogte in cm: ");
            double hoogtedrhk = double.Parse(Console.ReadLine());

            Console.Write("De oppervlakte van de driehoek is ");
            Console.Write(basis * hoogtedrhk / 2);
            Console.Write(" cm².");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Druk enter om terug te gaan...");
            string enter = Console.ReadLine();
            if (enter == "")
                Console.Clear();
        }

        static void OppervlakteCirkel()
        {
            Console.WriteLine();
            Console.WriteLine("OPPERVLAKTE CIRKEL BEREKENEN");
            Console.WriteLine("===============================");
            Console.Write("Geef de straal in cm: ");
            double straal = double.Parse(Console.ReadLine());
            double pi = Math.PI;

            Console.Write("De oppervlakte van de cirkel is ");
            Console.Write(pi * (straal * straal));
            Console.Write(" cm².");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Druk enter om terug te gaan...");
            string enter = Console.ReadLine();
            if (enter == "")
                Console.Clear();
        }
    }    
}
