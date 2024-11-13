﻿using System;
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

            if (Etat != EtatFormulaire.Ajouter && Course is not null)
            {
                txtNom.Text = Course.Nom;
                txtVille.Text = Course.Ville;
                lstProvince.Text = Course.Province.GetDescription();
                dtpDateDepart.SelectedDate = Course.Date.ToDateTime(TimeOnly.MinValue);
                lstType.Text = Course.TypeCourse.GetDescription();
                txtDistance.Text = Course.Distance.ToString();
                txtNbParticipants.Text = Course.NbParticipants.ToString();
                txtTempsCourseMoyen.Text = Course.TempCourseMoyen.ToString(@"hh\:mm\:ss");

                if (Etat == EtatFormulaire.Supprimer)
                {
                    txtNom.IsEnabled = false;
                    txtVille.IsEnabled = false;
                    lstProvince.IsEnabled = false;
                    dtpDateDepart.IsEnabled = false;
                    lstType.IsEnabled = false;
                    txtDistance.IsEnabled = false;
                }
            }
        }

        private bool ValiderCourse()
        {
            string message = "";

            if (string.IsNullOrWhiteSpace(txtNom.Text) || txtNom.Text.Length < Course.NOM_NB_CAR_MIN)
                message += $"- Le nom de la course doit contenir au moins {Course.NOM_NB_CAR_MIN} caractères.\n";

            if (string.IsNullOrWhiteSpace(txtVille.Text) || txtVille.Text.Length < Course.VILLE_NB_CAR_MIN)
                message += $"- Le nom de la ville d'une course doit contenir au moins {Course.VILLE_NB_CAR_MIN} caractères.\n";

            //ushort annee;
            //if (!ushort.TryParse(txtAnnee.Text, out annee) || annee < Film.ANNEE_MIN || annee > DateTime.Now.Year)
            //    message += $"- L'anné doit être une valeur numérique comprise entre {Film.ANNEE_MIN} et {DateTime.Now.Year}\n";

            if (message != "")
            {
                System.Windows.MessageBox.Show($"Veuillez corriger les erreurs suivantes : \n\n{message}", "Validation d'une course");
                return false;
            }

            return true;
        }

        private void btnClick_Click(object sender, RoutedEventArgs e)
        {
            if (ValiderCourse())
            {

            }
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
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
