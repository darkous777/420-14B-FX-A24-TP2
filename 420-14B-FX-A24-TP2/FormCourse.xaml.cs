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
using System.Windows.Shapes;
using _420_14B_FX_A24_TP2.classes;

namespace _420_14B_FX_A24_TP2
{
    /// <summary>
    /// Logique d'interaction pour FormCourse.xaml
    /// </summary>
    public partial class FormCourse : Window
    {
      
        
        public FormCourse()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnClick_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAjoutCoureurs_Click(object sender, RoutedEventArgs e)
        {
            FormCoureur formCoureur = new FormCoureur();
            formCoureur.ShowDialog();
        }

        private void btnModifierCoureurs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSupprimerCoureurs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lstCoureurs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
