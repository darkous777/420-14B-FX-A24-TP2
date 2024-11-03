﻿
using _420_14B_FX_A24_TP2.classes;
using System.Windows;

namespace _420_14B_FX_A24_TP2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string CHEMIN_FICHIER_COUREURS_CSV = "C:\\data-420-14B-FX\\TP2\\coureurs.csv";
        public const string CHEMIN_FICHIER_COURSE_CSV = "C:\\data-420-14B-FX\\TP2\\courses.csv";
        List<Course> courses = new List<Course>();
        GestionCourse _gestionCourse;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _gestionCourse = new GestionCourse(CHEMIN_FICHIER_COURSE_CSV, CHEMIN_FICHIER_COUREURS_CSV);

            foreach (Course c in _gestionCourse.Courses)
                lstCourses.Items.Add(c);
        }

        private void AfficherListeCourses()
        {
           
        }

        private void btnNouveau_Click(object sender, RoutedEventArgs e)
        {
            FormCourse formCourse = new FormCourse();
            formCourse.ShowDialog();    
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}