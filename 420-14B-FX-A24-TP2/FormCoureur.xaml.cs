using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _420_14B_FX_A24_TP2
{
    /// <summary>
    /// Logique d'interaction pour FormCoureur.xaml
    /// </summary>
    public partial class FormCoureur : Window
    {


        private Coureur _coureur;

        public Coureur Coureur
        {
            get { return _coureur; }
            set { _coureur = value; }
        }

        private EtatFormulaire _etat;

        public EtatFormulaire Etat
        {
            get { return _etat; }
            private set { _etat = value; }
        }

        public FormCoureur(EtatFormulaire etat = EtatFormulaire.Ajouter, Coureur coureur = null)
        {
            Etat = etat;
            Coureur = coureur;


            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            



            


        }





        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }



        private bool ValiderFormulaireCoureur()
        {

            string messageErreur = "";

            ushort numDossard;
            if (!ushort.TryParse(txtDossard.Text, out numDossard) || numDossard < Coureur.DOSSARD_VAL_MIN)
            {
                messageErreur += $"Le numéro de dossard ne peut pas etre null et doit être plus grand que {Coureur.DOSSARD_VAL_MIN}. \n";
            }

            if (string.IsNullOrEmpty(txtNom.Text) || txtNom.Text.Trim().Length < Coureur.NOM_NB_CARC_MIN)
            {
                messageErreur += $"Le champ nom ne peut pas etre vide et doit avoir un minimum de {Coureur.NOM_NB_CARC_MIN} caractères.\n";
            }

            if (string.IsNullOrEmpty(txtPrenom.Text) || txtPrenom.Text.Trim().Length < Coureur.PRENOM_NB_CARC_MIN)
            {
                messageErreur += $"Le champ prénom ne peut pas être vide et doit avoir un minimum de {Coureur.PRENOM_NB_CARC_MIN} caractères.\n";
            }

            if (string.IsNullOrEmpty(txtVille.Text) || txtVille.Text.Trim().Length < Coureur.VILLE_NB_CARC_MIN)
            {
                messageErreur += $"Le champ ville ne peut pas être vide et doit avoir un minimum de {Coureur.VILLE_NB_CARC_MIN} caractères.\n";
            }

            if (cboProvince.SelectedIndex == -1)
            {
                messageErreur += $"Le champ province doit être rempli. \n";
            }

            if (cboCategorie.SelectedIndex == -1)
            {
                messageErreur += $"Le champ catégorie doit être rempli. \n";
            }

            if (messageErreur != "")
            {
                MessageBox.Show(messageErreur, "Enregistrement pas reussi");
            }

            return messageErreur == "";

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string categorie in UtilEnum.GetAllDescriptions<Categorie>())
            {
                cboCategorie.Items.Add(categorie);
            }

            foreach (string province in UtilEnum.GetAllDescriptions<Province>())
            {
                cboProvince.Items.Add(province);
            }
            titreFormCoureur.Text = $"{Etat} une course";
            SaveButton.Content = Etat;

            if (Etat != EtatFormulaire.Ajouter && Coureur is not null)
            {
                txtDossard.Text = Coureur.Dossard.ToString();
                txtNom.Text = Coureur.Nom;
                txtPrenom.Text = Coureur.Prenom;
                txtVille.Text = Coureur.Ville;
                cboProvince.SelectedValue = Coureur.Province.GetDescription();
                cboCategorie.SelectedValue = Coureur.Categorie.GetDescription();
                tsTemps.Text = Coureur.Temps.ToString();
                checkAbandon.IsChecked = Coureur.Abandon;

                if (Etat == EtatFormulaire.Supprimer)
                {
                    txtDossard.IsEnabled = false;
                    txtNom.IsEnabled = false;
                    txtPrenom.IsEnabled = false;
                    txtVille.IsEnabled = false;
                    cboProvince.IsEnabled = false;
                    cboCategorie.IsEnabled = false;
                    tsTemps.IsEnabled = false;
                    checkAbandon.IsChecked = false;


                }
            }



        }
    }
}
