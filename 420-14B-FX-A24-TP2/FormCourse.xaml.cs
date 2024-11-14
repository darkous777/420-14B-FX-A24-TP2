using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using _420_14B_FX_A24_TP2.enums;
using Xceed.Wpf.Toolkit;

namespace _420_14B_FX_A24_TP2
{
    /// <summary>
    /// Logique d'interaction pour FormCourse.xaml
    /// </summary>
    public partial class FormCourse : Window
    {


        private Course _course;

        public Course Course
        {
            get { return _course; }
            set { _course = value; }
        }

        private EtatFormulaire _etat;
        public EtatFormulaire Etat
        {
            get
            {
                return _etat;
            }
            set
            {
                if (!(Enum.IsDefined(typeof(EtatFormulaire), value)))
                    throw new ArgumentOutOfRangeException(nameof(EtatFormulaire), $"La valeur {value} n'est pas existante dans les choix.");

                _etat = value;
            }

        }

        public FormCourse(EtatFormulaire etat = EtatFormulaire.Ajouter, Course course = null)
        {
            Etat = etat;
            Course = course;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            prvTitle.Text = $"{Etat} une course";
            btnAjoutCourse.Content = Etat;

            tableCoureurs.IsEnabled = false;

            if (Etat != EtatFormulaire.Ajouter && Course is not null)
            {
                tableCoureurs.IsEnabled = true;

                txtNom.Text = Course.Nom;
                txtVille.Text = Course.Ville;
                cboProvince.SelectedValue = Course.Province.GetDescription();
                dtpDateDepart.SelectedDate = Course.Date.ToDateTime(TimeOnly.MinValue);
                cboTypeCourse.SelectedValue = Course.TypeCourse.GetDescription();
                txtDistance.Text = Course.Distance.ToString();
                txtNbParticipants.Text = Course.NbParticipants.ToString();
                txtTempsCourseMoyen.Text = Course.TempCourseMoyen.ToString(@"hh\:mm\:ss");

                AfficherListeCoureurs();

                if (Etat == EtatFormulaire.Supprimer)
                {


                    txtNom.IsEnabled = false;
                    txtVille.IsEnabled = false;
                    cboProvince.IsEnabled = false;
                    dtpDateDepart.IsEnabled = false;
                    cboTypeCourse.IsEnabled = false;
                    txtDistance.IsEnabled = false;
                }
            }



            foreach (string province in UtilEnum.GetAllDescriptions<Province>())
            {
                cboProvince.Items.Add(province);
            }

            foreach (string typeCourse in UtilEnum.GetAllDescriptions<TypeCourse>())
            {
                cboTypeCourse.Items.Add(typeCourse);
            }

        }

        private bool ValiderCourse()
        {
            string message = "";

            if (string.IsNullOrWhiteSpace(txtNom.Text) || txtNom.Text.Length < Course.NOM_NB_CAR_MIN)
                message += $"- Le nom de la course doit contenir au moins {Course.NOM_NB_CAR_MIN} caractères.\n";

            if (string.IsNullOrWhiteSpace(txtVille.Text) || txtVille.Text.Length < Course.VILLE_NB_CAR_MIN)
                message += $"- Le nom de la ville d'une course doit contenir au moins {Course.VILLE_NB_CAR_MIN} caractères.\n";

            if (cboProvince.SelectedItem is null)
                message += $"- La province doit être sélectionné.\n";

            if (Etat == EtatFormulaire.Ajouter && (dtpDateDepart.SelectedDate is null || dtpDateDepart.SelectedDate < DateTime.Now))
                message += $"- La date doit être sélectionné et doit être précédente à la date d'aujourd'hui.\n";

            if (cboTypeCourse.SelectedItem is null)
                message += $"- Le type de course doit être sélectionné.\n";

            byte distance;
            if (!byte.TryParse(txtDistance.Text, out distance) || distance < Course.DISTANCE_VAL_MIN)
                message += $"La distance doit être plus grande que {Course.DISTANCE_VAL_MIN}.\n";

            if (message != "")
            {
                System.Windows.MessageBox.Show($"Veuillez corriger les erreurs suivantes : \n\n{message}", "Validation d'une course");
                return false;
            }

            return true;
        }

        private void AfficherListeCoureurs()
        {
            lstCoureurs.Items.Clear();
            Course.Coureurs.Sort();
            foreach (Coureur c in Course.Coureurs)
                lstCoureurs.Items.Add(c);
        }

        private void btnClick_Click(object sender, RoutedEventArgs e)
        {

            switch (Etat)
            {

                case EtatFormulaire.Ajouter:

                    if (ValiderCourse())
                    {
                        Course = new Course(Guid.NewGuid(), txtNom.Text, DateOnly.FromDateTime(dtpDateDepart.SelectedDate.Value), txtVille.Text, (enums.Province)cboProvince.SelectedIndex, (enums.TypeCourse)cboTypeCourse.SelectedIndex, byte.Parse(txtDistance.Text));

                        DialogResult = true;
                    }
                    else
                    {
                        DialogResult = null;
                    }

                    break;

                case EtatFormulaire.Modifier:

                    if (ValiderCourse())
                    {
                        Course.Nom = txtNom.Text;
                        Course.Ville = txtVille.Text;
                        Course.Province = (enums.Province)cboProvince.SelectedIndex;
                        Course.Date = DateOnly.FromDateTime(dtpDateDepart.SelectedDate.Value);
                        Course.TypeCourse = (enums.TypeCourse)cboTypeCourse.SelectedIndex;
                        Course.Distance = ushort.Parse(txtDistance.Text);

                        DialogResult = true;

                    }
                    else
                    {
                        DialogResult = null;
                    }

                    break;

                case EtatFormulaire.Supprimer:

                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show($"Êtes-vous sûre de vouloir supprimer la course?", "Suppression d'une course", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        DialogResult = true;
                    }
                    else
                    {
                        DialogResult = null;
                    }

                    break;
            }

        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnAjoutCoureurs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FormCoureur formCoureur = new FormCoureur();

                if (formCoureur.ShowDialog() is true)
                {
                    Course.AjouterCoureur(formCoureur.Coureur);

                    AfficherListeCoureurs();

                    System.Windows.MessageBox.Show("Le coureur à bien été ajouter!", "Ajout d'un nouveau coureur");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Une erreur s'est produit : " + ex.Message, "Ajout d'un coureur", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void btnModifierCoureurs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lstCoureurs.SelectedItem != null)
                {
                    FormCoureur formCoureur = new FormCoureur(EtatFormulaire.Modifier, lstCoureurs.SelectedItem as Coureur);

                    if (formCoureur.ShowDialog() is true)
                    {

                        AfficherListeCoureurs();

                        System.Windows.MessageBox.Show("Le coureur à bien été modifier!", "Modification d'un coureur");
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Vous devez sélectionner un coureur dans la liste!");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Une erreur s'est produit : " + ex.Message, "Modification d'un coureur", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void btnSupprimerCoureurs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lstCoureurs.SelectedItem != null)
                {
                    Coureur coureur = lstCoureurs.SelectedItem as Coureur;

                    FormCoureur formCoureur = new FormCoureur(EtatFormulaire.Supprimer, coureur);

                    if (formCoureur.ShowDialog() is true)
                    {
                        Course.SupprimerCoureur(coureur);

                        AfficherListeCoureurs();

                        System.Windows.MessageBox.Show("Le coureur à bien été supprimmé!", "Suppression d'un coureur");
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Vous devez sélectionner un coureur dans la liste!");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Une erreur s'est produit : " + ex.Message, "Suppression d'un coureur", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void lstCoureurs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
