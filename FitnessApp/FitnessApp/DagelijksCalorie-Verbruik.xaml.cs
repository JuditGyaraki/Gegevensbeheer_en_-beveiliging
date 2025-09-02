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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FitnessApp
{
    /// <summary>
    /// Interaction logic for DagelijksCalorie_Verbruik.xaml
    /// </summary>
    public partial class DagelijksCalorie_Verbruik : Page
    {
        bool geslacht = true;
        double hoogte = 0;
        double gewicht = 0;
        double leeftijd = 0;
        double bmr = 0;
        public DagelijksCalorie_Verbruik()
        {
            InitializeComponent();
        }
        private void tb_gewicht_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(tb_hoogte.Text, out double hoogte) && double.TryParse(tb_gewicht.Text, out double gewicht) && double.TryParse(tb_leeftijd.Text, out double leeftijd))
            {
                if (rbt_Vrouw.IsChecked == true)
                {
                    bmr = Math.Round((655.1 + (9.563 * gewicht) + (1.850 * hoogte) - (4.676 * leeftijd)),0);
                    if (activiteitsniveau.SelectedItem == sedentaire)
                    {
                        tb_bmr.Text = (bmr * 1.2).ToString();
                    }
                    else if (activiteitsniveau.SelectedItem == licht)
                    {
                        tb_bmr.Text = (bmr * 1.375).ToString();
                    }
                    else if (activiteitsniveau.SelectedItem == medium)
                    {
                        tb_bmr.Text = (bmr * 1.55).ToString();
                    }
                    else if (activiteitsniveau.SelectedItem == zeer)
                    {
                        tb_bmr.Text = (bmr * 1.725).ToString();
                    }
                    else if (activiteitsniveau.SelectedItem == extreem)
                    {
                        tb_bmr.Text = (bmr * 1.9).ToString();
                    }
                }
                else if (rbt_Man.IsChecked == true)
                {
                    bmr = Math.Round((66.5 + (13.75 * gewicht) + (5.003 * hoogte) - (6.75 * leeftijd)), 0);
                    if (activiteitsniveau.SelectedItem == sedentaire)
                    {
                        tb_bmr.Text = (bmr * 1.2).ToString();
                    }
                    else if (activiteitsniveau.SelectedItem == licht)
                    {
                        tb_bmr.Text = (bmr * 1.375).ToString();
                    }
                    else if (activiteitsniveau.SelectedItem == medium)
                    {
                        tb_bmr.Text = (bmr * 1.55).ToString();
                    }
                    else if (activiteitsniveau.SelectedItem == zeer)
                    {
                        tb_bmr.Text = (bmr * 1.725).ToString();
                    }
                    else if (activiteitsniveau.SelectedItem == extreem)
                    {
                        tb_bmr.Text = (bmr * 1.9).ToString();
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Voer een geldig getal in");
            }

            

            //hoogte = double.Parse(tb_hoogte.Text);
            //gewicht = double.Parse(tb_gewicht.Text);
            //leeftijd = double.Parse(tb_leeftijd.Text);
        }
    }
}
