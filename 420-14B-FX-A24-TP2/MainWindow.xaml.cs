
using _420_14B_FX_A24_TP2.classes;
using _420_14B_FX_A24_TP2.enums;
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
        public GestionCourse _gestionCourse;





        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _gestionCourse = new GestionCourse(CHEMIN_FICHIER_COURSE_CSV, CHEMIN_FICHIER_COUREURS_CSV);

            AfficherListeCourses();
        }

        private void AfficherListeCourses()
        {
            lstCourses.Items.Clear();
            _gestionCourse.Courses.Sort();
            foreach (Course c in _gestionCourse.Courses)
                lstCourses.Items.Add(c);
        }

        private void btnNouveau_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FormCourse formCourse = new FormCourse();

                if (formCourse.ShowDialog() is true)
                {
                    _gestionCourse.AjouterCourse(formCourse.Course);

                    // _gestionCourse.EnregistrerCourses(CHEMIN_FICHIER_COURSE_CSV, CHEMIN_FICHIER_COUREURS_CSV);
                    AfficherListeCourses();

                    MessageBox.Show("La course a bien été ajouter!", "Ajout d'une nouvelle course");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produit : " + ex.Message, "Ajout d'un film", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lstCourses.SelectedItem != null)
                {
                    FormCourse formCourse = new FormCourse(EtatFormulaire.Modifier, lstCourses.SelectedItem as Course);

                    if (formCourse.ShowDialog() is true)
                    {
                        // _gestionCourse.EnregistrerCourses(CHEMIN_FICHIER_COURSE_CSV, CHEMIN_FICHIER_COUREURS_CSV);

                        AfficherListeCourses();

                        MessageBox.Show("La course a bien été modifier!");

                    }

                }
                else
                {
                    MessageBox.Show("Vous devez sélectionner une course dans la liste!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produit : " + ex.Message, "Ajout d'un film", MessageBoxButton.OK, MessageBoxImage.Error);

            }



        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (lstCourses.SelectedItem != null)
                {
                    Course courseSelect = lstCourses.SelectedItem as Course;

                    FormCourse formCourse = new FormCourse(EtatFormulaire.Supprimer, courseSelect);

                    if (formCourse.ShowDialog() is true)
                    {

                        _gestionCourse.SupprimerCourse(courseSelect);

                        // _gestionCourse.EnregistrerCourses(CHEMIN_FICHIER_COURSE_CSV, CHEMIN_FICHIER_COUREURS_CSV); 

                        AfficherListeCourses();

                        MessageBox.Show("La course a bien été supprimér!");
                    }

                }
                else
                {
                    MessageBox.Show("Vous devez sélectionner une course dans la liste!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produit : " + ex.Message, "Ajout d'un film", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }


    }
}