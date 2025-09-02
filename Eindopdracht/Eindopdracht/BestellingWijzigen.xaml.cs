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
    /// Interaction logic for BestellingWijzigen.xaml
    /// </summary>
    public partial class BestellingWijzigen : Page
    {
        static private List<Bestelling> Bestellingen { get; set; } = new List<Bestelling> { };
        private List<string> LijstBestellingen { get; set; } = new List<string> { };
        private List<Bestelling> LijstWijzigen { get; set; } = new List<Bestelling> { };
        static private List<Drank> Dranken { get; set; } = new List<Drank>
        {

        };

        private List<string> LijstDranken { get; set; } = new List<string> { };

        public BestellingWijzigen()
        {
            Dranken.Clear();
            Bestellingen.Clear();
            InitializeComponent();

            try
            {
                LijstDranken = File.ReadAllLines("Menu.csv").ToList();

            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.Message);
                return;
            }            
            foreach (var ld in LijstDranken)
            {
                string[] parts = ld.Split(',');
                Dranken.Add(new Drank(parts[0], double.Parse(parts[1])));
            }
            lb_Menu.ItemsSource = Dranken;

            try
            {
                LijstBestellingen = File.ReadAllLines("Bestelling.csv").ToList();

            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.Message);
                return;
            }            
            foreach (var wijzigen in LijstBestellingen)
            {
                string[] parts = wijzigen.Split(',');
                Bestellingen.Add(new Bestelling(new Drank(parts[0], double.Parse(parts[1])), int.Parse(parts[2])));
            }
            LijstBestellingen.Clear();
        }

        private void Tafelnummer_TextChanged(object sender, EventArgs e)
        {
            LijstWijzigen.Clear();

            List<Bestelling> bestellingenVanTafel = Bestellingen.FindAll(b => b.TafelNr == int.Parse(tbx_tafelnrWijzigen.Text));

            if (bestellingenVanTafel.Count == 0)
            {
                MessageBox.Show("Deze tafel heeft geen bestellingen.");
            }
            if (tbx_tafelnrWijzigen.Text != string.Empty)
            {
                foreach (var b in Bestellingen)
                {

                    if (int.Parse(tbx_tafelnrWijzigen.Text) == b.TafelNr)
                    {
                        LijstWijzigen.Add(new Bestelling(new Drank(b.Drank.Naam, b.Drank.Prijs), b.TafelNr));
                    }
                }

                lb_BestellingWijzigen.ItemsSource = LijstWijzigen;
                lb_BestellingWijzigen.Items.Refresh();
            }
            else
            {

                MessageBox.Show("Geef een tafelnummer in");
            }

        }

        private void menu_MouseDoubleClick (object sender, MouseEventArgs e)
        {
            Bestelling selecteditem = (Bestelling)lb_BestellingWijzigen.SelectedItem;
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

            LijstWijzigen.Remove(selecteditem);
            

            Drank selectedDrank = (Drank)lb_Menu.SelectedItem;

            Bestelling nieuweBestelling = new Bestelling(selectedDrank, int.Parse(tbx_tafelnrWijzigen.Text));

            LijstWijzigen.Add(nieuweBestelling);
            Bestellingen.Add(nieuweBestelling);
            lb_BestellingWijzigen.Items.Refresh();

            foreach (var b in Bestellingen)
            {
                LijstBestellingen.Add(NaarCSVRegel(b));
            }

            File.WriteAllLines("Bestelling.csv", LijstBestellingen);

        }

        static string NaarCSVRegel(Bestelling b)
        {
            return $"{b.Drank.Naam},{b.Drank.Prijs},{b.TafelNr}";
        }
    }
}
