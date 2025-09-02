using Crypto_Employees_PBKDF2;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using BC = BCrypt.Net.BCrypt;

namespace Eindopdracht_ScientistsProjects
{
    internal class Program
    {
        private static ScientistsProjectsContext db = new ScientistsProjectsContext();
        static void Main(string[] args)
        {
            //Crypto crypto = new Crypto();
            //Console.WriteLine("Geef de voornaam: ");
            //string voornaam = Console.ReadLine();
            //Console.WriteLine("Geef de achternaam: ");
            //string achternaam = Console.ReadLine();
            //Console.WriteLine("Geef de wachtwoord: ");
            //string wachtwoord = Console.ReadLine();
            //Console.WriteLine("Geef de initialization vector: ");
            //int Ivector = int.Parse(Console.ReadLine());

            //byte[] salt = RandomNumberGenerator.GetBytes(20);  // PBKDF2 sleutel maken
            //byte[] keyDerivedFromPassword = Rfc2898DeriveBytes.Pbkdf2(wachtwoord, salt, Ivector, HashAlgorithmName.SHA512, 32);

            //Console.WriteLine("Geef de loon: ");
            //string loonInput = Console.ReadLine();
            //byte[] loon = Encoding.UTF8.GetBytes(loonInput);
            //byte[] bIvector = Encoding.UTF8.GetBytes(Ivector.ToString());

            //byte[] encryptedLoon = crypto.Encrypt(loon, keyDerivedFromPassword).cipher_output;

            //string passwordHash = BC.HashPassword(wachtwoord);

            //db.Scientists.Add(new Scientist { Firstname = voornaam, Lastname = achternaam, Password = passwordHash, Salary = encryptedLoon, Iv = bIvector });

            //db.SaveChanges();
            //db.Dispose();


            string gebruikersnaam = "";
            bool loginResult = false;
            while (!loginResult)
            {
                Console.Clear();
                Console.Write("Geef de gebruikersnaam in: ");
                gebruikersnaam = Hoofdletter(Console.ReadLine());
                Console.Write("Geef de wachtwoord in: ");
                string password = Console.ReadLine();
                loginResult = Login(gebruikersnaam, password);
                if (loginResult)
                {
                    Console.WriteLine("Login succesvol!");
                    loginResult = true;
                }
                else
                {
                    Console.WriteLine("Login mislukt.");
                }
                Console.WriteLine("Druk enter om verder te gaan...");

                Console.ReadKey();
            }

            bool keuzeHoofdmenu = true;
            while (keuzeHoofdmenu)
            {
                if (gebruikersnaam == "Gyaraki")
                {


                    int adminkeuze;
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Welkom admin! Wat wil je doen?\n\n1: Wetenschapper bewerken\n2: Project bewerken\n3: Lijst van wetenschappers en actuele projecten tonen\n4: Programma beëindigen");
                        adminkeuze = ReadInt("\nMaak uw keuze aub: ", 1, 4);
                        switch (adminkeuze)
                        {
                            case 1:
                                int adminkeuze2;

                                do
                                {
                                    Console.Clear();
                                    WetenschapperBewerken.ToonScientists();

                                    Console.WriteLine("\nMogelijkheden:\n\n1: Nieuwe wetenschapper toevoegen\n2: Wetenschapper aanpassen\n3: Wetenschapper verwijderen\n4: Terug naar menu\n");
                                    adminkeuze2 = ReadInt("Maak uw keuze aub: ", 1, 4);

                                    switch (adminkeuze2)
                                    {
                                        case 1:
                                            WetenschapperBewerken.ScientistToevoegen();
                                            break;
                                        case 2:
                                            WetenschapperBewerken.ScientistAanpassen();
                                            break;
                                        case 3:
                                            WetenschapperBewerken.ScientistVerwijderen();
                                            break;
                                        case 4:
                                            Console.WriteLine("Terug naar het hoofdmenu...");
                                            break;
                                    }
                                } while (adminkeuze2 != 4);
                                break;

                            case 2:

                                int adminkeuze3;
                                do
                                {
                                    Console.Clear();
                                    ProjectBewerken.ToonProjects();
                                    Console.WriteLine("Mogelijkheden:\n\n1: Nieuwe project toevoegen\n2: Project aanpassen\n3: Project verwijderen\n4: Terug naar menu\n");
                                    adminkeuze3 = ReadInt("Geef jouw keuze: ", 1, 4);

                                    switch (adminkeuze3)
                                    {
                                        case 1:
                                            Console.Clear();
                                            ProjectBewerken.ProjectToevoegen();
                                            break;
                                        case 2:
                                            Console.Clear();
                                            ProjectBewerken.ProjectAanpassen();
                                            break;
                                        case 3:
                                            ProjectBewerken.ProjectVerwijderen();
                                            break;
                                        case 4:
                                            break;
                                    }
                                } while (adminkeuze3 != 4);
                                break;
                            case 3:
                                WetenschapperBewerken.ToonScientists();
                                Console.WriteLine();
                                ProjectBewerken.ToonProjectEnScientist();
                                break;
                            case 4:
                                keuzeHoofdmenu = false;
                                break;
                        } 
                    } while (adminkeuze != 4);
                    break;
                }
                else
                {
                    Console.Clear();

                    int keuze = 0;
                    do
                    {
                        Console.WriteLine("1. Jouw projecten bekijken\n2. Lijst van wetenschappers\n3. Lijst van projecten\n4. Stop\n");
                        keuze = ReadInt("Maak jouw keuze: ", 1, 4);

                        switch (keuze)
                        {
                            case 1:
                                ProjectBewerken.ToonProjectEnScientist(gebruikersnaam);
                                break;
                            case 2:
                                WetenschapperBewerken.ToonScientistEnProject();
                                break;
                            case 3:
                                ProjectBewerken.ToonProjectEnScientist();
                                break;
                            case 4:
                                break;
                        }
                    } while (keuze != 4);
                    break ;
                                                          
                }
            }
            db.Dispose();
            
        }


        // INLOGGEN
        private static bool Login(string username, string password)
        {
            bool loginResult = false;

            using (var db = new ScientistsProjectsContext())
            {
                var user = db.Scientists.FirstOrDefault(user => user.Lastname == username);
                if (user == null || BC.Verify(password, user.Password) == false)
                {
                    Console.WriteLine("Username does not exist or password doesn't match.");
                    loginResult = false;
                }
                else
                {
                    loginResult = true;
                }
            }
            return loginResult;

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
