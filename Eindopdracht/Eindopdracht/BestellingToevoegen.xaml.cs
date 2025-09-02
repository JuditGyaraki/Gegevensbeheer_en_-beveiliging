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
    /// Interaction logic for BestellingToevoegen.xaml
    /// </summary>
    public partial class BestellingToevoegen : Page
    {
        static private List<Bestelling> Bestellingen { get; set; } = new List<Bestelling> { };

        static private List<Drank> Dranken { get; set; } = new List<Drank>
        {

        };

        private List<string> LijstDranken { get; set; } = new List<string> { };

        private List<string> LijstBestellingen { get; set; } = new List<string> { };

        private List<Bestelling> huidigeBestelling { get; set; } = new List<Bestelling>() { };

        public BestellingToevoegen()
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
            lb_MenuDranken.ItemsSource = Dranken;

            try
            {
                LijstBestellingen = File.ReadAllLines("Bestelling.csv").ToList();

            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.Message);
                return;
            }            
            foreach (var lb in LijstBestellingen)
            {
                string[] parts = lb.Split(',');
                Bestellingen.Add(new Bestelling(new Drank(parts[0], double.Parse(parts[1])), int.Parse(parts[2])));
            }
            LijstBestellingen.Clear();


        }

        private void btn_ToevoegenAanBestelling (object sender, EventArgs e)
        {
            
            if (lb_MenuDranken.SelectedItem != null && tbx_tafelnummer.Text != string.Empty)
            {
                Drank selectedDrank = (Drank)lb_MenuDranken.SelectedItem;  
                
                huidigeBestelling.Add(new Bestelling(new Drank(selectedDrank.Naam, selectedDrank.Prijs), int.Parse(tbx_tafelnummer.Text)));
                Bestellingen.Add(new Bestelling(new Drank(selectedDrank.Naam, selectedDrank.Prijs), int.Parse(tbx_tafelnummer.Text)));              
            }
            else
            {
                MessageBox.Show("Geef een tafelnummer");
            }

            tbx_HuidigeBestelling.Text = String.Empty;

            foreach (var b in huidigeBestelling)
            {
                if (b.TafelNr == int.Parse(tbx_tafelnummer.Text))
                {
                    tbx_HuidigeBestelling.Text += $"\n{b.Drank.Naam}, {b.Drank.Prijs}";
                }
            }

            

        }

        private void btn_BestellingOpslaan(object sender, EventArgs e)
        {
            LijstBestellingen.Clear();
            foreach (var b in Bestellingen)
            {
                LijstBestellingen.Add(NaarCSVRegel(b));
            }
            File.WriteAllLines("Bestelling.csv", LijstBestellingen);
        }




        static string NaarCSVRegel(Bestelling b)
        {
            return $"{b.Drank.Naam}, {b.Drank.Prijs}, {b.TafelNr}";
        }
    }
}
