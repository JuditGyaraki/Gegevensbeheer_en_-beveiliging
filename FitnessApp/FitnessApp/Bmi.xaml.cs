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
    /// Interaction logic for Bmi.xaml
    /// </summary>
    public partial class Bmi : Page
    {
        double bmi = 0;
        public Bmi()
        {
            InitializeComponent();
            bmi = 0;
        }
        private void GewichtBox_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(GewichtBox.Text, out double gewicht))
            {
                BmiBlock.Text = $"BMI: {bmi}";
                if (bmi < 18.5)
                {
                    BmiBlock.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                }
                else if (bmi > 18.5 && bmi < 25)
                {
                    BmiBlock.Background = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                }
                else if (bmi > 24.9 && bmi < 30)
                {
                    BmiBlock.Background = new SolidColorBrush(Color.FromRgb(255, 165, 0));
                }
                else if (bmi > 30)
                {
                    BmiBlock.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));

                }
            }
            else
            {
                MessageBox.Show("Voer een geldig getal in voor gewicht.");
            }
        }
        private void LengteBox_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(LengteBox.Text, out double lengte) && double.TryParse(GewichtBox.Text, out double gewicht))
            {
                lengte = lengte / 100;
                bmi = Math.Round(gewicht / (lengte * lengte), 2);
                BmiBlock.Text = $"BMI: {bmi}";
                if (bmi < 18.5)
                {
                    BmiBlock.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                }
                else if (bmi > 18.5 && bmi < 25)
                {
                    BmiBlock.Background = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                }
                else if (bmi > 24.9 && bmi < 30)
                {
                    BmiBlock.Background = new SolidColorBrush(Color.FromRgb(255, 165, 0));
                }
                else if (bmi > 30)
                {
                    BmiBlock.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));

                }
            }
            else
            {
                MessageBox.Show("Voer een geldig getal in voor lengte.");
            }
        }
    }
}
