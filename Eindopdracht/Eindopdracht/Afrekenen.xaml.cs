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
    /// Interaction logic for Afrekenen.xaml
    /// </summary>
    public partial class Afrekenen : Page
    {
        static private List<Bestelling> Bestellingen { get; set; } = new List<Bestelling> { };
        private List<string> LijstBestellingen { get; set; } = new List<string> { };
        private List<Bestelling> LijstAfrekenen { get; set; } = new List<Bestelling> { };
        private List<Bestelling> AfrekenenPerProduct { get; set; } = new List<Bestelling> { };
        double totaalIndividueel = 0;
        double totaal = 0;






        public Afrekenen()
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
            foreach (var afrekenen in LijstBestellingen)
            {
                string[] parts = afrekenen.Split(',');
                Bestellingen.Add(new Bestelling(new Drank(parts[0], double.Parse(parts[1])), int.Parse(parts[2])));
            }
        }

        private void Click_Afrekenen(object sender, EventArgs e)
        {
            LijstAfrekenen.Clear();
            
            if (tbx_TafelnrAfrekenen.Text == string.Empty)
            {
                MessageBox.Show("Geef een tafelnummer.");
                return;
            }
            else
            {
                List<Bestelling> bestellingenVanTafel = Bestellingen.FindAll(b => b.TafelNr == int.Parse(tbx_TafelnrAfrekenen.Text));
                if (bestellingenVanTafel.Count == 0)
                {
                    MessageBox.Show("Deze tafel heeft geen bestellingen.");
                    
                }
            }
            foreach (var b  in Bestellingen)
            {
                
                if (int.Parse(tbx_TafelnrAfrekenen.Text) == b.TafelNr)
                {
                    LijstAfrekenen.Add(new Bestelling(new Drank(b.Drank.Naam, b.Drank.Prijs), b.TafelNr));
                    totaal += b.Drank.Prijs;
                    tbk_Totaalprijs.Text = $"Totaalprijs: {totaal} euro";
                }
                
            }

            lb_Afrekenen.ItemsSource = LijstAfrekenen;
            lb_Afrekenen.Items.Refresh();


        }

        private void bestelling_MouseDoubleClick (object sender, MouseEventArgs e)
        {
            if (lb_Afrekenen.SelectedItem != null)
            {
                Bestelling selectedBestelling = (Bestelling)lb_Afrekenen.SelectedItem;
                AfrekenenPerProduct.Add(selectedBestelling);
                Bestellingen.Remove(selectedBestelling);
                LijstAfrekenen.Remove(selectedBestelling);
                lb_Afrekenen.Items.Refresh();
                totaal -= selectedBestelling.Drank.Prijs;
                tbk_Totaalprijs.Text = $"Totaalprijs: {totaal} euro";

            }
            totaalIndividueel = 0;
            foreach (var item in AfrekenenPerProduct)
            {
                totaalIndividueel += item.Drank.Prijs;
                tbk_TotaalprijsIndividueel.Text = $"Totaalprijs: {totaalIndividueel} euro";
            }
           
            lb_AfrekenenPerProduct.ItemsSource = AfrekenenPerProduct;
            lb_AfrekenenPerProduct.Items.Refresh();
            

        }

        private void click_Betalen(object sender, EventArgs e)
        {
            LijstBestellingen.Clear();
            foreach(var item in AfrekenenPerProduct)
            {
                var bestellingOmTeVerwijderen = Bestellingen.FirstOrDefault(b =>
                    b.TafelNr == item.TafelNr &&
                    b.Drank.Naam == item.Drank.Naam &&
                    b.Drank.Prijs == item.Drank.Prijs);

                Bestellingen.Remove(bestellingOmTeVerwijderen);

            }

            foreach (var b in Bestellingen)
            {
                LijstBestellingen.Add(NaarCSVRegel(b));
            }

            File.WriteAllLines("Bestelling.csv", LijstBestellingen);

            AfrekenenPerProduct.Clear();
            lb_AfrekenenPerProduct.Items.Refresh();
            tbk_TotaalprijsIndividueel.Text = $"Totaalprijs: 0 euro";

        }

        static string NaarCSVRegel(Bestelling b)
        {
            return $"{b.Drank.Naam},{b.Drank.Prijs},{b.TafelNr}";
        }




    }
}
