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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

       

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClickBestellingToevoegen(object sender, RoutedEventArgs e)
        {
            BestellingToevoegen bestellingToevoegen = new BestellingToevoegen();
            frame.Navigate(bestellingToevoegen);
        }

        private void ClickAfrekenen(object sender, RoutedEventArgs e)
        {
            Afrekenen afrekenen = new Afrekenen();
            frame.Navigate(afrekenen);
        }

        private void ClickBestellingVerwijderen(object sender, RoutedEventArgs e)
        {
            BestellingVerwijderen bestellingVerwijderen = new BestellingVerwijderen();
            frame.Navigate(bestellingVerwijderen);
        }

        private void ClickBestellingWijzigen(object sender, RoutedEventArgs e)
        {
            BestellingWijzigen bestellingWijzigen = new BestellingWijzigen();
            frame.Navigate(bestellingWijzigen);
        }


    }
}

