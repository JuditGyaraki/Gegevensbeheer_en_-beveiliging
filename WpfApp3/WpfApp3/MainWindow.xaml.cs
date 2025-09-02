using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Movie> Movies { get; set; } = new List<Movie>
        {

        };
        private List<string> Films { get; set; } = new List<string> { };
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                Films = File.ReadAllLines("Movie.csv").ToList();

                foreach (var f in Films)
                {
                    string[] parts = f.Split(',');
                    Movies.Add(new Movie(parts[0], int.Parse(parts[1]), parts[2], parts[3], int.Parse(parts[4])));
                }

                lb_Movies.ItemsSource = Movies;
                Films.Clear();

            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return;
            }           
        }

        private void movies_MouseDubbelClick (object sender, MouseEventArgs e)
        {
            if (lb_Movies.SelectedItem != null)
            {
                Movie selectedMovie = (Movie)lb_Movies.SelectedItem;
                tbx_naam.Text = selectedMovie.Naam;
                tbx_releaseJaar.Text = selectedMovie.ReleaseJaar.ToString();
                tbx_genre.Text = selectedMovie.Genre;
                tbx_regisseur.Text = selectedMovie.Regisseur;
                tbx_speelduur.Text = selectedMovie.Speelduur.ToString();
            }
        }
        private void AddMovieButton_Click (object sender, EventArgs e)
        {
            string naam = tbx_naam.Text;
            int releaseJaar = int.Parse(tbx_releaseJaar.Text);          
            string genre = tbx_genre.Text;
            string regisseur = tbx_regisseur.Text;
            int speelduur = int.Parse(tbx_speelduur.Text);

            if (Movies.Exists(m=> m.Naam == tbx_naam.Text))
            {
                MessageBox.Show("De film bestaat al in de lijst.");
            }
            else
            {
                Movie newMovie = new Movie(naam, releaseJaar, genre, regisseur, speelduur);
                Movies.Add(newMovie);
                lb_Movies.Items.Refresh();
            }
            Films.Clear();
            foreach (var m in Movies)
            {
                Films.Add(NaarCSVRegel(m));
            }
            File.WriteAllLines("Movie.csv", Films);

            tbx_naam.Clear();
            tbx_releaseJaar.Clear();
            tbx_genre.Clear();
            tbx_regisseur.Clear();
            tbx_speelduur.Clear();
        }

        private void DeleteMovieButton_Click (object sender, EventArgs e)
        {
            if (lb_Movies.SelectedItem != null)
            {
                Movie selectedMovie = (Movie)lb_Movies.SelectedItem;
                Movies.Remove(selectedMovie);

                lb_Movies.Items.Refresh();
            }
            Films.Clear();
            foreach(var m in Movies)
            {
                Films.Add(NaarCSVRegel(m));
            }

            File.WriteAllLines("Movie.csv", Films);

            tbx_naam.Clear();
            tbx_releaseJaar.Clear();
            tbx_genre.Clear();
            tbx_regisseur.Clear();
            tbx_speelduur.Clear();

        }

        private void UpdateMovieButton_Click (object sender, EventArgs e)
        {
            if (lb_Movies.SelectedItem != null)
            {
                Movie selectedMovie = (Movie)lb_Movies.SelectedItem;

                selectedMovie.Naam = tbx_naam.Text;
                selectedMovie.ReleaseJaar = int.Parse(tbx_releaseJaar.Text);
                selectedMovie.Genre = tbx_genre.Text;
                selectedMovie.Regisseur = tbx_regisseur.Text;
                selectedMovie.Speelduur = int.Parse(tbx_speelduur.Text);

                lb_Movies.Items.Refresh();
            }
            Films.Clear();
            foreach (var m in Movies)
            {
                Films.Add(NaarCSVRegel(m));
            }
            File.WriteAllLines("Movie.csv", Films);

            tbx_naam.Clear();
            tbx_releaseJaar.Clear();
            tbx_genre.Clear();
            tbx_regisseur.Clear();
            tbx_speelduur.Clear();

        }

        static string NaarCSVRegel(Movie m)
        {
            return $"{m.Naam}, {m.ReleaseJaar}, {m.Genre}, {m.Regisseur}, {m.Speelduur}";
        }  
    }

   
}
