using System;
using System.Collections.Generic;
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
using System.IO;

namespace Eindopdracht
{
    /// <summary>
    /// Interaction logic for BestellingVerwijderen.xaml
    /// </summary>
    public partial class BestellingVerwijderen : Page
    {
        static private List<Bestelling> Bestellingen { get; set; } = new List<Bestelling> { };
        private List<string> LijstBestellingen { get; set; } = new List<string> { };

        private List<Bestelling> LijstVerwijderen { get; set; } = new List<Bestelling> { };

        public BestellingVerwijderen()
        {
            Bestellingen.Clear();
            InitializeComponent();

            try
            {
                LijstBestellingen = File.ReadAllLines("Bestelling.csv").ToList();

            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.Message);
                return;
            }            
            foreach (var verwijderen in LijstBestellingen)
            {
                string[] parts = verwijderen.Split(',');
                Bestellingen.Add(new Bestelling(new Drank(parts[0], double.Parse(parts[1])), int.Parse(parts[2])));
            }
            LijstBestellingen.Clear();
        }

        private void Tafelnummer_TextChanged(object sender, EventArgs e)
        {
            LijstVerwijderen.Clear();

            List<Bestelling> bestellingenVanTafel = Bestellingen.FindAll(b => b.TafelNr == int.Parse(tbx_tafelnrVerwijderen.Text));

            if (bestellingenVanTafel.Count == 0)
            {
                MessageBox.Show("Deze tafel heeft geen bestellingen.");
            }

            if (tbx_tafelnrVerwijderen.Text != string.Empty)
            {
                foreach (var b in Bestellingen)
                {

                    if (int.Parse(tbx_tafelnrVerwijderen.Text) == b.TafelNr)
                    {
                        LijstVerwijderen.Add(new Bestelling(new Drank(b.Drank.Naam, b.Drank.Prijs), b.TafelNr));
                    }

                }

                lb_BestellingVerwijderen.ItemsSource = LijstVerwijderen;
                lb_BestellingVerwijderen.Items.Refresh();


            }
            else
            {

                MessageBox.Show("Geef een tafelnummer in");
            }
            
        }

        private void bestellingVerwijderen_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LijstBestellingen.Clear ();
            Bestelling selecteditem = (Bestelling)lb_BestellingVerwijderen.SelectedItem;
            if (selecteditem != null)
            {
                var bestellingOmTeVerwijderen = Bestellingen.FirstOrDefault(b =>
                    b.TafelNr == selecteditem.TafelNr &&
                    b.Drank.Naam == selecteditem.Drank.Naam &&
                    b.Drank.Prijs == selecteditem.Drank.Prijs);

                if (bestellingOmTeVerwijderen != null)
                {
                    Bestellingen.Remove(bestellingOmTeVerwijderen);
                }
                else
                {
                    MessageBox.Show("Bestelling niet gevonden in de lijst");
                }
                LijstVerwijderen.Remove(selecteditem);
                lb_BestellingVerwijderen.Items.Refresh();

                foreach(var b in Bestellingen)
                {
                    LijstBestellingen.Add(NaarCSVRegel(b));
                }

                File.WriteAllLines("Bestelling.csv", LijstBestellingen);
            }

        }

        static string NaarCSVRegel(Bestelling b)
        {
            return $"{b.Drank.Naam},{b.Drank.Prijs},{b.TafelNr}";
        }

        static void Save()
        {
            List<string> lines = new List<string>();

            foreach (Bestelling bestelling in Bestellingen)
            {
                lines.Add(NaarCSVRegel(bestelling));
            }

            File.WriteAllLines("Bestelling.csv", lines);
        }

    }
}
