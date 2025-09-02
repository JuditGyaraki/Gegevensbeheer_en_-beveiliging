using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Crypto_Employees_PBKDF2;
using BC = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace Eindopdracht_ScientistsProjects
{
    internal class WetenschapperBewerken
    {
        private static ScientistsProjectsContext db = new ScientistsProjectsContext();
        public static void ToonScientists()
        {
            Console.Clear();
            Console.WriteLine("WETENSCHAPPERS");
            Console.WriteLine("~~~~~~~~~~~~~~");
            Console.WriteLine();

            var scientistList = db.Scientists.OrderBy(s => s.Id).ToList();

            for (int i = 0; i < scientistList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {scientistList[i]}");
            }
            Console.WriteLine();
            Console.WriteLine();

        }

        public static void ScientistToevoegen()
        {
            string keuze = "";
            do
            {
                Console.Clear();
                Crypto crypto = new Crypto();
                Console.Write("Geef de voornaam: ");
                string voornaam = Hoofdletter(Console.ReadLine());
                Console.Write("Geef de achternaam: ");
                string achternaam = Hoofdletter(Console.ReadLine());
                if (db.Scientists.Any(s => s.Firstname == voornaam && s.Lastname == achternaam))
                {
                    Console.WriteLine("Wetenschapper bestaat al in de database.");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.Write("Geef de wachtwoord: ");
                    string scientistWachtwoord = Console.ReadLine();
                    Console.Write("Geef de admin wachtwoord voor Encrypt: ");
                    string wachtwoord = Console.ReadLine();
                    int iteration = 100000;
                    byte[] salt = Encoding.UTF8.GetBytes("MijnVasteSalt12345");
                    byte[] keyDerivedFromPassword = Rfc2898DeriveBytes.Pbkdf2(wachtwoord, salt, iteration, HashAlgorithmName.SHA512, 32);
                    Console.Write("Geef de loon: ");
                    string loonInput = Console.ReadLine();
                    byte[] loon = Encoding.UTF8.GetBytes(loonInput);
                    byte[] bIvector = Encoding.UTF8.GetBytes(iteration.ToString());
                    var result = crypto.Encrypt(loon, keyDerivedFromPassword);
                    byte[] encryptedLoon = result.cipher_output;
                    byte[] echteIv = result.IV;
                    string passwordHash = BC.HashPassword(wachtwoord);

                    db.Scientists.Add(new Scientist { Firstname = voornaam, Lastname = achternaam, Password = passwordHash, Salt = salt, Salary = encryptedLoon, Iv = echteIv });

                    db.SaveChanges();
                    Console.Write($"Wetenschapper {voornaam} {achternaam} is toegevoegd aan database.\nNog een wetenschapper toevoegen? j/n: ");
                    keuze = Console.ReadLine().ToLower();
                }
                
            } while (keuze != "n");
            
        }

        public static void ScientistAanpassen()
        {
            Console.Clear();
            Console.WriteLine("1. Naam wijzigen\n2. Loon wijzigen\n3. Wachtwoord  wijzigen\n4. Terug naar menu");
            int keuze = ReadInt("\nWat wil je wijzigen? Geef de nummer van je keuze: ", 1, 4);
            switch (keuze)
            {
                case 1:
                    string Kkeuze = "";
                    do
                    {
                        Console.Clear();

                        var scientistList = db.Scientists.OrderBy(s => s.Id).ToList();

                        for (int i = 0; i < scientistList.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {scientistList[i]}");
                        }

                        Console.WriteLine();
                        int gekozenWetenschapperIndex = ReadInt("Welke wetenschapper wil je aanpassen? Geef de nummer: ", 1, scientistList.Count());

                        var gekozenWetenschapper = scientistList[gekozenWetenschapperIndex - 1];

                        Console.WriteLine($"Naam van {gekozenWetenschapper} veranderen.");
                        Console.WriteLine();

                        Console.Write("Geef de nieuwe voornaam in: ");
                        string voornaam = Hoofdletter(Console.ReadLine());
                        Console.Write("Geef de nieuwe achternaam in: ");
                        string achternaam = Hoofdletter(Console.ReadLine());
                        gekozenWetenschapper.Firstname = voornaam;
                        gekozenWetenschapper.Lastname = achternaam;

                        db.SaveChanges();
                        Console.Write($"Naam werd gewijzigd. Nieuwe naam: {voornaam} {achternaam}.\nNog een naam aanpassen? j/n: "); 
                        Kkeuze = Console.ReadLine().ToLower();
                    } while (Kkeuze != "n");
                    break;
                case 2:
                    string invoer = "";
                    Console.Write("Geef de admin wachtwoord in: ");
                    string password = Console.ReadLine();
                    do
                    {
                        Console.Clear();
                        Crypto crypto = new Crypto();
                        var scientistList = db.Scientists.OrderBy(s => s.Id).ToList();

                        int iteration = 100000;
                        for (int i = 0; i < scientistList.Count; i++)
                        {
                            byte[] keyDerivedFromPassword2 = Rfc2898DeriveBytes.Pbkdf2(password, scientistList[i].Salt, iteration, HashAlgorithmName.SHA512, 32);
                            byte[] decryptedLoon = crypto.Decrypt(scientistList[i].Salary, keyDerivedFromPassword2, scientistList[i].Iv);
                            string loonAlsTekst = Encoding.UTF8.GetString(decryptedLoon);
                            Console.WriteLine($"{i + 1}. {scientistList[i]} - loon: {loonAlsTekst} euro");
                        }

                        Console.WriteLine();
                        int gekozenWetenschapperIndex = ReadInt("Wiens loon wil je aanpassen? Geef de nummer: ", 1, scientistList.Count());

                        var gekozenWetenschapper = scientistList[gekozenWetenschapperIndex - 1];

                        Console.WriteLine($"Loon wijzigen van {gekozenWetenschapper}");
                        Console.WriteLine();

                        Console.Write("Geef de nieuwe loon in: ");
                        string loonInput = Console.ReadLine();
                        byte[] nieuweLoon = Encoding.UTF8.GetBytes(loonInput);
                        byte[] salt = Encoding.UTF8.GetBytes("MijnVasteSalt12345");
                        byte[] keyDerivedFromPassword = Rfc2898DeriveBytes.Pbkdf2(password, salt, iteration, HashAlgorithmName.SHA512, 32);

                        var result = crypto.Encrypt(nieuweLoon, keyDerivedFromPassword);
                        byte[] encryptedLoon = result.cipher_output;
                        byte[] echteIv = result.IV;

                        gekozenWetenschapper.Salt = salt;
                        gekozenWetenschapper.Salary = encryptedLoon;
                        gekozenWetenschapper.Iv = echteIv;
                        db.SaveChanges();
                        Console.Write($"Loon van {gekozenWetenschapper} is gewijzigd. Nog een loon aanpassen? j/n: ");
                        invoer = Console.ReadLine().ToLower();
                    } while (invoer != "n");
                    break;
               

                case 3:
                    string choice = "";
                    do
                    {
                        Console.Clear();
                        var scientistList = db.Scientists.OrderBy(s => s.Id).ToList();

                        for (int i = 0; i < scientistList.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {scientistList[i]}");
                        }

                        int gekozenIndex = ReadInt("\nGeef de nummer van de wetenschapper om het wachtwoord aan te passen: ", 1, scientistList.Count);
                        var gekozenScientist = scientistList[gekozenIndex - 1];

                        Console.WriteLine($"Wachtwoord van {gekozenScientist} veranderen.");
                        Console.WriteLine();

                        Console.Write("Geef het nieuwe wachtwoord in: ");
                        string nieuwWachtwoord = Console.ReadLine();

                        string passwordHash = BC.HashPassword(nieuwWachtwoord);
                        gekozenScientist.Password = passwordHash;
                        db.SaveChanges();
                      
                        Console.WriteLine($"Het wachtwoord van {gekozenScientist} is veranderd.");

                        Console.Write("Nog een wachtwoord veranderen? j/n: ");
                        choice = Console.ReadLine().ToLower();
             
                    }
                    while (choice != "n");                  
                    break;
                case 4:
                    return;
            }
        }

        public static void ScientistVerwijderen()
        {
            Console.Clear();
            string keuze = "";
            do
            {
                Console.Clear();        
                Console.WriteLine("Wetenschappers:\n");

                var projectenVanScientist = db.AssignedTos
                    .Include (p => p.Project)
                    .Include (s => s.Scientist)
                    .ToList();

                var lijst = db.Scientists.OrderBy(s => s.Id).ToList();
                for (int i = 0; i < lijst.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {lijst[i]}");
                }
                int keuzeIndex = ReadInt("\nWelke wetenschapper wil je verwijderen? Geef het nummer: ", 1, lijst.Count);
                var teVerwijderen = lijst[keuzeIndex - 1];

                var gekoppeldeRecords = db.AssignedTos
                    .Where(a => a.ScientistId == teVerwijderen.Id)
                    .ToList();
                db.AssignedTos.RemoveRange(gekoppeldeRecords);
                db.Scientists.Remove(teVerwijderen);
                db.SaveChanges();

               //OF VIA NAAM:
                
                //Console.Write("\nWelke wetenschapper wil je verwijderen? Geef de voornaam: ");
                //string vnWetenschapper = Hoofdletter(Console.ReadLine());
                //Console.Write("Geef de achternaam: ");
                //string anWetenschapper = Hoofdletter(Console.ReadLine());
                //var teVerwijderen = db.Scientists.FirstOrDefault(s => s.Firstname == vnWetenschapper && s.Lastname == anWetenschapper);
                //if(teverwijderen == null)
                //{
                //    Console.WriteLine("Deze naam bestaat niet in de database. Druk enter en probeer het opnieuw...");
                //    //    Console.ReadKey();
                //    //    continue;
                //}

                Console.Write($"{teVerwijderen} is verwijderd uit database.\nNog een wetenschapper verwijderen? j/n: ");
                keuze = Console.ReadLine().ToLower();
            } while (keuze != "n");

        }

        public static void ToonScientistEnProject()
        {
            string keuze = "";
            do
            {
                List<Scientist> wetenschappers = db.Scientists
                    .Include(s => s.AssignedTos)
                        .ThenInclude(a => a.Project)
                    .ToList();

                Console.WriteLine("WETENSCHAPPERS EN HUN PROJECTEN");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

                int teller = 1;
                foreach (var s in wetenschappers)
                {
                    Console.WriteLine($"{teller++}. {s.Firstname} {s.Lastname}");

                    if (s.AssignedTos != null && s.AssignedTos.Any())
                    {
                        Console.WriteLine("Projecten:");

                        foreach (var a in s.AssignedTos)
                        {
                            Console.WriteLine($"- {a.Project.Name} (werkuren: {a.Hours})");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Geen projecten toegewezen.");
                    }

                    Console.WriteLine();
                }

                Console.WriteLine("Druk 'm' voor menu...");
                keuze = Console.ReadLine().ToLower();

            } while (keuze != "m");
        }




        static int ReadInt(string message, int min, int max)
        {
            int intInput = 0;

            Console.Write(message);
            bool success = int.TryParse(Console.ReadLine(), out intInput);

            while (!success || intInput < min || intInput > max)
            {
                Console.Write($"Ongeldige invoer. Kies tussen {min} en {max}. {message}");
                success = int.TryParse(Console.ReadLine(), out intInput);
            }
            return intInput;
        }

        public static string Hoofdletter(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }
    }
}
