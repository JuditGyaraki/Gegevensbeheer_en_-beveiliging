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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            
        }
        

        private void bmi_btn_click(object sender, EventArgs e)
        {
            Bmi bmi = new Bmi();
            frame.Navigate(bmi);
        }
        private void hartslagzones_btn_click(object sender, EventArgs e)
        {
            Hartslagzones hartslagzones = new Hartslagzones();
            frame.Navigate(hartslagzones);
        }
        private void daCaVe_btn_click(object sender, EventArgs e)
        {
            DagelijksCalorie_Verbruik dagelijksCalorie_Verbruik = new DagelijksCalorie_Verbruik();
            frame.Navigate(dagelijksCalorie_Verbruik);
        }
    }
}
