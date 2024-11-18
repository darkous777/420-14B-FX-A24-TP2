using System.Windows;
using _420_14B_FX_A24_TP2.classes;
using _420_14B_FX_A24_TP2.enums;

namespace _420_14B_FX_A24_TP2
{
    /// <summary>
    /// Logique d'interaction pour FormCourse.xaml
    /// </summary>
    public partial class FormCourse : Window
    {

        /// <summary>
        /// Une course
        /// </summary>
        private Course _course;
        /// <summary>
        /// Obtient ou défini la course
        /// </summary>
        public Course Course
        {
            get { return _course; }
            set { _course = value; }
        }
        /// <summary>
        /// Etat du formulaire
        /// </summary>
        private EtatFormulaire _etat;
        /// <summary>
        /// Obtient ou defini l'etat du formulaire
        /// </summary>
        public EtatFormulaire Etat
        {
            get
            {
                return _etat;
            }
            set
            {
                if (!(Enum.IsDefined(typeof(EtatFormulaire), value)))
                    throw new ArgumentOutOfRangeException(nameof(Etat), $"La valeur {value} n'est pas existante dans les choix.");

                _etat = value;
            }

        }
        /// <summary>
        /// Constructeur du FormCourse
        /// </summary>
        /// <param name="etat">Etat dans lequel le formulaire changera</param>
        /// <param name="course">Course dans laquelle les manipulation seront effectuee</param>
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
            dtpDateDepart.SelectedDate = DateTime.Now;

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
                    tableCoureurs.IsEnabled = false ;

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
        /// <summary>
        /// Methode de validation des informations recu
        /// </summary>
        /// <returns>True si l'utilisateur respect les conditions sinon False</returns>
        private bool ValiderCourse()
        {
            string message = "";

            if (string.IsNullOrWhiteSpace(txtNom.Text) || txtNom.Text.Length < Course.NOM_NB_CAR_MIN)
                message += $"- Le nom de la course doit contenir au moins {Course.NOM_NB_CAR_MIN} caractères.\n";

            if (string.IsNullOrWhiteSpace(txtVille.Text) || txtVille.Text.Length < Course.VILLE_NB_CAR_MIN)
                message += $"- Le nom de la ville d'une course doit contenir au moins {Course.VILLE_NB_CAR_MIN} caractères.\n";

            if (cboProvince.SelectedItem is null)
                message += $"- La province doit être sélectionné.\n";

            if (cboTypeCourse.SelectedItem is null)
                message += $"- Le type de course doit être sélectionné.\n";

            byte distance;
            if (!byte.TryParse(txtDistance.Text, out distance) || distance < Course.DISTANCE_VAL_MIN)
                message += $"- La distance doit être plus grande que {Course.DISTANCE_VAL_MIN}.\n";

            if (message != "")
            {
                MessageBox.Show($"Veuillez corriger les erreurs suivantes : \n\n{message}", "Validation d'une course");
                return false;
            }

            return true;
        }
        /// <summary>
        /// Methode permettant l'affichage sur WPF
        /// </summary>
        private void AfficherListeCoureurs()
        {
            lstCoureurs.Items.Clear();
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

                    MessageBoxResult messageBoxResult = MessageBox.Show($"Êtes-vous sûre de vouloir supprimer la course?", "Suppression d'une course", MessageBoxButton.YesNo, MessageBoxImage.Question);

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

                    MessageBox.Show("Le coureur à bien été ajouter!", "Ajout d'un nouveau coureur");
                }
            }
            catch (ArgumentNullException nul)
            {
                MessageBox.Show("Une erreur s'est produit : " + nul.Message, "Ajout d'un coureur", MessageBoxButton.OK);
            }
            catch (ArgumentOutOfRangeException outRange)
            {
                MessageBox.Show("Une erreur s'est produit : " + outRange.Message, "Ajout d'un coureur", MessageBoxButton.OK);
            }
            catch (InvalidOperationException nonExistant)
            {
                MessageBox.Show("Une erreur s'est produit : " + nonExistant.Message, "Ajout d'un coureur", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produit : " + ex.Message, "Ajout d'un coureur", MessageBoxButton.OK);

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
                        Course.TrierCoureurs();

                        AfficherListeCoureurs();

                        MessageBox.Show("Le coureur à bien été modifier!", "Modification d'un coureur");
                    }
                }
                else
                {
                    MessageBox.Show("Vous devez sélectionner un coureur dans la liste!");
                }
            }
            catch (ArgumentNullException nul)
            {
                MessageBox.Show("Une erreur s'est produit : " + nul.Message, "Modification d'un coureur", MessageBoxButton.OK);
            }
            catch (ArgumentOutOfRangeException outRange)
            {
                MessageBox.Show("Une erreur s'est produit : " + outRange.Message, "Modification d'un coureur", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produit : " + ex.Message, "Modification d'un coureur", MessageBoxButton.OK);

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

                        MessageBox.Show("Le coureur à bien été supprimmé!", "Suppression d'un coureur");
                    }
                }
                else
                {
                    MessageBox.Show("Vous devez sélectionner un coureur dans la liste!");
                }
            }
            catch (ArgumentNullException nul)
            {
                MessageBox.Show("Une erreur s'est produit : " + nul.Message, "Suppression d'un coureur", MessageBoxButton.OK);
            }
            catch (ArgumentOutOfRangeException outRange)
            {
                MessageBox.Show("Une erreur s'est produit : " + outRange.Message, "Suppression d'un coureur", MessageBoxButton.OK);
            }
            catch (InvalidOperationException nonExistant)
            {
                MessageBox.Show("Une erreur s'est produit : " + nonExistant.Message, "Suppression d'un coureur", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produit : " + ex.Message, "Suppression d'un coureur", MessageBoxButton.OK);

            }
        }
    }
}
