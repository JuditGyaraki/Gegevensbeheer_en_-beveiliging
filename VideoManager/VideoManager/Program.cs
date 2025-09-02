using System.Reflection;

namespace VideoManager
{
    public class Program
    {
        static List<Video> Videos = new List<Video>
            {
                 new Movie("Jaws", 124, "Steven Spielberg", 1975, "Thriller", 3 ),
                 new Movie("The Lobster", 119, "Yorgos Lanthimos", 2015, "Comedy", 4 ),
                 new MusicVideo("Video killed the radio star", 240, "The Buggles", 5 )
            };
        static void Main(string[] args)
        {
            string keuze = "";
            while (keuze != "q")
            {
                Console.Clear();
               
                if (Videos.Count == 0)
                {
                    Console.WriteLine("Er zijn geen video's.");
                }
                else
                {
                    ToonVideos(Videos);
                }
                
                Console.WriteLine();
                Console.WriteLine("Druk 1 om een film toe te voegen");
                Console.WriteLine("Druk 2 om een videoklip toe te voegen");
                Console.WriteLine("Druk 3 om een video te verwijderen");
                Console.WriteLine("Druk q om te stoppen");
                Console.WriteLine("");
                Console.Write("Geef jouw keuze (1, 2, 3 of q): ");
                keuze = Console.ReadLine();
                if (keuze == "1")
                {
                    AddMovie(Videos);
                }
                else if (keuze == "2")
                {
                    AddMusicVideo();
                }
                else if (keuze == "3")
                {
                    RemoveVideo();
                }
               
            }

        }

        static void ToonVideos(List<Video> Videos)
        {
            Console.WriteLine("\n--- Lijst van video's ---");
            Console.WriteLine("");

            foreach (var video in Videos)
            {
                Console.WriteLine(video);
            }
        }
        static void AddMovie(List<Video> Videos)
        {
            Console.Write("Geef de titel van de film: ");
            string filmTitel = Console.ReadLine();
            Console.Write("Geef de duur van de film in minuten: ");
            int filmDuur = int.Parse(Console.ReadLine());
            Console.Write("Geef de director van de film: ");
            string filmDirector = Console.ReadLine();
            Console.Write("Geef het jaar van de film: ");
            int filmYear =int.Parse(Console.ReadLine());
            Console.Write("Geef de genre van de film: ");
            string filmGenre = Console.ReadLine();
            int filmRating = ReadInt("Geef de rating van de film: ");


            Videos.Add(new Movie(filmTitel, filmDuur, filmDirector, filmYear, filmGenre, filmRating));
        }
        static int ReadInt(string message)
        {
            Console.Write(message);
            string input = Console.ReadLine();
            int result = 0;

            while (!int.TryParse(input, out result))
            {
                Console.WriteLine(message);
                input = Console.ReadLine();
            }
            return result;
        }
        public static void AddMusicVideo()
        {
            Console.Write("Geef de titel van de videoclip: ");
            string vClipTitel = Console.ReadLine();
            int vClipDuur = ReadInt("Geef de duur van de videoclip: ");
            Console.Write("Geef de artist van de videoclip: ");
            string vClipArtist = Console.ReadLine();
            int vClipRating = ReadInt("Geef de rating van de videoclip: ");

            Videos.Add(new MusicVideo(vClipTitel, vClipDuur, vClipArtist, vClipRating ));
        }
        public static void RemoveVideo()
        {
            Console.Write("Geef de titel van de vieo: ");
            string teVerwijderenVideo = Console.ReadLine();
            Video videoToRemove = Videos.Find(v => v.Title == teVerwijderenVideo );
            if (videoToRemove != null)
            {
                Videos.Remove(videoToRemove);
                Console.WriteLine($"Video is verwijderd.");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"Video niet gevonden.");
            }
        }
    }
}
