using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Eindopdracht_ScientistsProjects
{  
    internal class ProjectBewerken
    {
        private static ScientistsProjectsContext db = new ScientistsProjectsContext();

        public static void ToonProjects()
        {
            List<Project> projecten = db.Projects.ToList();
            Console.WriteLine("PROJECTEN");
            Console.WriteLine("~~~~~~~~~");
            Console.WriteLine();
            int i = 1;
            foreach (var project in projecten)
            {
                Console.WriteLine($"{i++}. {project}");
            } 
        }
        public static void ProjectToevoegen()
        {
            Console.Clear();

            string adminkeuze;

            do
            {
                Console.Clear() ;
                Console.WriteLine("Geef de titel van de project: ");
                string titel = Console.ReadLine();

                if (db.Projects.Any(p => p.Name == titel))
                {
                    Console.WriteLine("Project bestaat al in de database.");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.WriteLine("Geef de beschrijving: ");
                    string beschrijving = Console.ReadLine();

                    db.Projects.Add(new Project { Name = titel, Description = beschrijving });
                    db.SaveChanges();
                    Console.WriteLine("Project werd toegevoegd.");

                    Console.WriteLine("Nog een project toevoegen? j/n: ");
                    adminkeuze = Console.ReadLine();
                }

            } while (adminkeuze != "n");

        }

        public static void ProjectAanpassen()
        {
            int adminkeuze;

            do
            {
                Console.Clear();
                Console.WriteLine("Wat wil je aanpassen? Mogelijkheden: \n\n1. Titel\n2. Beschrijving\n3. Wetenschapper(s) van project(en) verwijderen\n4. Wetenschapper(s) naar project(en) toewijzen\n5. Aantal werkuren van wetenschapper(s) wijzigen\n6. Terug naar menu\n");

                adminkeuze = ReadInt("Maak uw keuze aub: ", 1, 6);

                switch (adminkeuze)
                {
                    case 1:
                        Console.Clear();

                        ToonProjects();

                        string adminkeuze2;

                        do
                        {
                            Console.Clear();
                            var projecten = db.Projects.OrderBy(p => p.Id).ToList();

                            for (int i = 0; i < projecten.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {projecten[i].Name}");
                            }

                            int keuze = ReadInt("\nWelke projectnaam wil je aanpassen? Voer het nummer van het project in: ", 1, projecten.Count);
                            var gekozenProject = projecten[keuze - 1];

                            Console.WriteLine($"\nHuidige titel: {gekozenProject.Name}");
                            Console.WriteLine("\nVoer de nieuwe titel in: ");
                            string nieuweNaam = Console.ReadLine();

                            if (!string.IsNullOrWhiteSpace(nieuweNaam))
                            {
                                gekozenProject.Name = nieuweNaam;
                                db.SaveChanges();
                                Console.WriteLine($"\nProjectnaam succesvol aangepast. De nieuwe titel: {nieuweNaam}");
                            }
                            else
                            {
                                Console.WriteLine("Geen geldige naam ingevoerd. Wijziging geannuleerd.");
                            }
                            Console.Write("Nog een titel aanpassen? j/n: ");
                            adminkeuze2 = Console.ReadLine();
                        } while (adminkeuze2 != "n");
                        break;
                    case 2:
                        string adminkeuze3;

                        do
                        {
                            Console.Clear();
                            var projecten = db.Projects.OrderBy(p => p.Id).ToList();

                            for (int i = 0; i < projecten.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {projecten[i]}");
                            }

                            int keuze = ReadInt("\nWelke beschrijving wil je aanpassen? Voer het nummer van het project in: ", 1, projecten.Count);
                            var gekozenProject = projecten[keuze - 1];

                            Console.WriteLine($"\nHuidige beschrijving: {gekozenProject.Description}");
                            Console.Write("\nVoer de nieuwe beschrijving in: ");
                            string nieuweBeschrijving = Console.ReadLine();

                            if (!string.IsNullOrWhiteSpace(nieuweBeschrijving))
                            {
                                gekozenProject.Description = nieuweBeschrijving;
                                db.SaveChanges();
                                Console.WriteLine("\nBeschrijving succesvol aangepast.");
                            }
                            else
                            {
                                Console.WriteLine("Wijziging geannuleerd.");
                            }
                            Console.Write("Nog een beschrijving aanpassen? j/n: ");
                            adminkeuze3 = Console.ReadLine().ToLower();
                        } while (adminkeuze3 != "n");
                        break;
                    case 3:
                        ScientistVanProjectVerwijderen();
                        break;
                    case 4:
                        ScientistNaarProjectToewijzen();
                        break;
                    case 5:
                        HoursAanpassen();
                        break;
                    case 6:
                        break;



                } 
            } while (adminkeuze != 6);
        }

        public static void ScientistNaarProjectToewijzen()
        {
            Console.Clear();
            string keuze = "";
            do
            {
                var wetenschappers = db.Scientists.OrderBy(s => s.Id).ToList();

                Console.Clear();

                for (int i = 0; i < wetenschappers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {wetenschappers[i].Firstname} {wetenschappers[i].Lastname}");
                }

                int gekozenWetenschapperIndex = ReadInt("\nKies een wetenschapper om toe te voegen aan een project. Voer het nummer in van de wetenschapper: ", 1, wetenschappers.Count);
                var gekozenWetenschapper = wetenschappers[gekozenWetenschapperIndex - 1];

                var projecten = db.Projects.OrderBy(p => p.Id).ToList();

                Console.Clear();

                for (int i = 0; i < projecten.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {projecten[i].Name}");
                }

                int gekozenProjectIndex = ReadInt("\nKies een project. Voer het nummer van het project in: ", 1, projecten.Count);
                var gekozenProject = projecten[gekozenProjectIndex - 1];

                var assignedTos = db.AssignedTos
                    .Include(s => s.Scientist)
                    .Include(p => p.Project)
                    .ToList();

                foreach (var item in assignedTos)
                {
                    if(item.ScientistId == gekozenWetenschapper.Id && item.Project.Id == gekozenProject.Id)
                    {
                        Console.WriteLine("Wetenschapper is al toegevoegd aan deze project.");
                        Console.ReadKey();
                        return;
                    }                   
                }
                int aantalUren = ReadInt("\nVoer het aantal werkuren in: ", 1, 2000);

                db.AssignedTos.Add(new AssignedTo { ScientistId = gekozenWetenschapper.Id, ProjectId = gekozenProject.Id, Hours = aantalUren });
                db.SaveChanges();

                Console.WriteLine($"\nWetenschapper {gekozenWetenschapper.Firstname} {gekozenWetenschapper.Lastname} is succesvol toegevoegd aan project {gekozenProject.Name} met {aantalUren} werkuren.");
                Console.Write("\nNog een wetenschapper naar een project toewijzen? j/n: ");
                keuze = Console.ReadLine().ToLower();


            } while (keuze != "n");
        }

        public static void HoursAanpassen()
        {
            string keuze = "";
            
            do
            {
                Console.Clear();

                List<AssignedTo> scientists = db.AssignedTos
                          .Include(s => s.Scientist)
                          .Include(p => p.Project)
                          .ToList();

                var scientistList = db.Scientists.OrderBy(s => s.Id).ToList();

                for (int i = 0; i < scientistList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {scientistList[i]}");
                }


                int wetenschapper = ReadInt("\nWiens werkuren wil je wijzigen? Geef de nummer: ", 1, scientistList.Count());
                var gekozenScientist = scientistList[wetenschapper - 1];
                var assignedList = scientists
                    .Where(s => s.ScientistId == gekozenScientist.Id)
                    .ToList();

                if (assignedList.Count == 0)
                {
                    Console.WriteLine("\nGeen projecten gevonden voor deze wetenschapper.");
                    Console.WriteLine("Druk enter voor menu...");
                    Console.ReadKey();
                    return;
                }

                Console.Clear();
                Console.WriteLine($"Actuele projecten van {gekozenScientist}:\n");
                int pnr = 1;
                foreach (var assignment in assignedList)
                {
                    Console.WriteLine($"{pnr++}. {assignment.Project.Name}, aantal werkuren: {assignment.Hours}");
                }

                int gekozenIndex = ReadInt("\nGeef de nummer van de project om de werkuren van de wetenschapper aan te passen: ", 1, assignedList.Count);

                var aanTePassen = assignedList[gekozenIndex - 1];
                Console.WriteLine($"\nActuele werkuur: {aanTePassen.Hours}");
                Console.WriteLine();
                int werkuur = ReadInt("\nGeef de nieuwe werkuren: ", 1, 2000);
                aanTePassen.Hours = werkuur;
                db.SaveChanges();
                Console.WriteLine($"\nWerkuren van wetenschapper {gekozenScientist} gewijzigd.");
                Console.Write("\nNog werkuren aanpassen? j/n: ");
                keuze = Console.ReadLine().ToLower();
            } while (keuze != "n");
        }

        public static void ScientistVanProjectVerwijderen()
        {
            string keuze = "";
           
            do
            {
                Console.Clear();
                List<AssignedTo> scientists = db.AssignedTos
                          .Include(s => s.Scientist)
                          .Include(p => p.Project)
                          .ToList();

                var scientistList = db.Scientists.OrderBy(s => s.Id).ToList();

                for (int i = 0; i < scientistList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {scientistList[i]}");
                }


                int wetenschapper = ReadInt("\nWelke wetenschapper wil je verwijderen? Geef de nummer: ", 1, scientistList.Count());
                var gekozenScientist = scientistList[wetenschapper - 1];
                var assignedList = scientists
                    .Where(s => s.ScientistId == gekozenScientist.Id)
                    .ToList();

                if (assignedList.Count == 0)
                {
                    Console.WriteLine("\nGeen projecten gevonden voor deze wetenschapper.");
                    Console.WriteLine("Druk enter voor menu...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine($"\nActuele projecten van {gekozenScientist}:\n");
                int pnr = 1;
                foreach (var pScientist in scientists)
                {
                    if (gekozenScientist.Id == pScientist.ScientistId)
                    {
                        Console.WriteLine($"{pnr++}. {pScientist.Project.Name}");
                    }
                }

                int gekozenIndex = ReadInt("\nGeef de nummer van de project om de wetenschapper te verwijderen: ", 1, assignedList.Count);

                var teVerwijderen = assignedList[gekozenIndex - 1];
                db.AssignedTos.Remove(teVerwijderen);
                db.SaveChanges();
                Console.WriteLine($"\nWetenschapper {gekozenScientist} is verwijderd van het project: {teVerwijderen.Project.Name}.");
               
                Console.Write("\nNog een wetenschapper verwijderen van een project? j/n: ");
                keuze = Console.ReadLine().ToLower();
            } while (keuze != "n");
            return;
        }
        

        public static void ProjectVerwijderen()
        {
            List<AssignedTo> scientists = db.AssignedTos
                      .Include(s => s.Scientist)
                      .Include(p => p.Project)
                      .ToList();

            Console.Clear();
            string keuze2 = "";
            do
            {
                Console.Clear();
                var projectenLijst = db.Projects.OrderBy(p => p.Id).ToList();

                Console.WriteLine("Projecten:");

                for (int i = 0; i < projectenLijst.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {projectenLijst[i].Name}");
                }
                Console.WriteLine();

                int keuze = ReadInt("Welke project wil je verwijderen? Geef de nummer: ", 1, projectenLijst.Count);

                var teVerwijderen = projectenLijst[keuze - 1];

                foreach (var scientist in scientists)
                {
                    if (scientist.ProjectId == teVerwijderen.Id)
                    {
                        db.AssignedTos.Remove(scientist);
                    }
                }

                db.Projects.Remove(teVerwijderen);
                db.SaveChanges();
                Console.WriteLine($"\nProject {teVerwijderen.Name} is verwijderd.");
                Console.Write("\nNog een project verwijderen? j/n: ");
                keuze2 = Console.ReadLine().ToLower();
            } while (keuze2 != "n");
        }

        //public static void ToonProjectEnScientist()
        //{
        //    string keuze = "";
        //    do
        //    {

        //        List<AssignedTo> projects = db.AssignedTos
        //       .Include(pr => pr.Project)
        //       .Include(si => si.Scientist)
        //       .ToList();

        //        var grouped = projects
        //            .GroupBy(p => p.Project.Name);

        //        Console.WriteLine("PROJECTEN");
        //        Console.WriteLine("~~~~~~~~~");

        //        int i = 1;
        //        foreach (var group in grouped)
        //        {                  
        //            var project = group.First().Project;
        //            Console.WriteLine($"{i++}. Titel: {group.Key}");
        //            Console.WriteLine("-----");
        //            Console.WriteLine($"Beschrijving: {project.Description}");
        //            Console.WriteLine("-------------");
        //            Console.WriteLine("Wetenschappers die eraan werken:");

        //            foreach (var item in group)
        //            {
        //                Console.WriteLine($"- {item.Scientist.Firstname} {item.Scientist.Lastname}, (werkuren: {item.Hours})");
        //            }
                    

        //            Console.WriteLine();
        //        }
        //        Console.WriteLine("Druk m voor menu...");               
                
        //      keuze = Console.ReadLine().ToLower();
        //    } while (keuze != "m");
        //}

       

        public static void ToonProjectEnScientist(string username)
        {
            var filteredProjects = db.AssignedTos
            .Include(pr => pr.Project)
            .Include(si => si.Scientist)
            .Where(a => a.Scientist.Lastname == username)
            .ToList();
            Console.Clear();
            Console.WriteLine("JOUW PROJECTEN:");
            Console.WriteLine();

            if (filteredProjects.Count == 0)
            {
                Console.WriteLine("Je hebt geen lopende projecten.");
            }
            else
            {
                int i = 1;
                foreach (var item in filteredProjects)
                {
                    Console.WriteLine($"{i++}. Titel: {item.Project.Name}:\n-----\n- Beschrijving: {item.Project.Description}\n- Aantal werkuren: {item.Hours}\n\n");
                }
            }           
        }

        public static void ToonProjectEnScientist()
        {
            string keuze = "";
            do
            {
                List<Project> projecten = db.Projects
                    .Include(p => p.AssignedTos)
                        .ThenInclude(at => at.Scientist)
                    .ToList();

                Console.WriteLine("PROJECTEN EN WETENSCHAPPERS");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

                int i = 1;
                foreach (var project in projecten)
                {
                    Console.WriteLine($"{i++}. TITEL: {project.Name}\n");
                    Console.WriteLine($"BESCHRIJVING: {project.Description}\n");

                    if (project.AssignedTos != null && project.AssignedTos.Any())
                    {
                        Console.WriteLine("Wetenschappers die eraan werken:");

                        foreach (var at in project.AssignedTos)
                        {
                            var scientist = at.Scientist;
                            Console.WriteLine($"- {scientist} (werkuren: {at.Hours})");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Geen wetenschappers toegewezen.");
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
