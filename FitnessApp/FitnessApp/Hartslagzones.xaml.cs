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

namespace FitnessApp
{
    /// <summary>
    /// Interaction logic for Hartslagzones.xaml
    /// </summary>
    public partial class Hartslagzones : Page
    {
        public Hartslagzones()
        {
            InitializeComponent();
        }
        private void LeeftijdBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Probeer de leeftijd om te zetten naar een getal
            if (int.TryParse(LeeftijdBox.Text, out int leeftijd) && leeftijd > 0)
            {
                // Bereken de maximale hartslag
                int maximaleHartslag = 220 - leeftijd;

                // Bereken de hartslagzones
                double zone1Min = 0.50 * maximaleHartslag;
                double zone1Max = 0.60 * maximaleHartslag;

                double zone2Min = 0.60 * maximaleHartslag;
                double zone2Max = 0.70 * maximaleHartslag;

                double zone3Min = 0.70 * maximaleHartslag;
                double zone3Max = 0.80 * maximaleHartslag;

                double zone4Min = 0.80 * maximaleHartslag;
                double zone4Max = 0.90 * maximaleHartslag;

                double zone5Min = 0.90 * maximaleHartslag;
                double zone5Max = maximaleHartslag;

                // Toon de resultaten in de TextBlocks
                Zone1.Text = $"Zone 1: {zone1Min} - {zone1Max} bpm";
                Zone2.Text = $"Zone 2: {zone2Min} - {zone2Max} bpm";
                Zone3.Text = $"Zone 3: {zone3Min} - {zone3Max} bpm";
                Zone4.Text = $"Zone 4: {zone4Min} - {zone4Max} bpm";
                Zone5.Text = $"Zone 5: {zone5Min} - {zone5Max} bpm";
            }
            else
            {
                MessageBox.Show("Voer een geldig getal in voor leeftijd.");

                // Als de input geen geldige leeftijd is, leeg de zones
                //Zone1.Text = "Voer een geldige leeftijd in.";
                //Zone2.Text = "";
                //Zone3.Text = "";
                //Zone4.Text = "";
                //Zone5.Text = "";
            }
        }
    }
}
