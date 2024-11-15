using _420_14B_FX_A24_TP2.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _420_14B_FX_A24_TP2.classes
{
    public class GestionCourse
    {
        /// <summary>
        /// Liste des courses
        /// </summary>
		private List<Course> _courses;
        /// <summary>
        /// Obtient ou défini la liste des courses
        /// </summary>
		public List<Course> Courses
        {
            get { return _courses; }
            set { _courses = value; }
        }
        /// <summary>
        /// Constructeur de la classe GestionCourse
        /// </summary>
        /// <param name="cheminFichierCoursesCsv">Chemin CSV permettant d'acceder au informations des courses</param>
        /// <param name="cheminFichierCoureursCsv">Chemin CSV permettant d'acceder au informations des coureurs</param>
        public GestionCourse(string cheminFichierCoursesCsv, string cheminFichierCoureursCsv)
        {
            Courses = new List<Course>();
            ChargerCourse(cheminFichierCoursesCsv, cheminFichierCoureursCsv);
        }
        /// <summary>
        /// Méthode permettant de vérifier si une course est déjà existante dans la liste de courses
        /// </summary>
        /// <param name="course">Course à vérifier si existante</param>
        /// <returns>Si la course est existante dans la liste de course (true/false)</returns>
        /// <exception cref="ArgumentNullException">Excecption retournant une exception si nulle</exception>
        public bool Existe(Course course)
        {
            if (course is null)
                throw new ArgumentNullException(nameof(course), "Le Course ne peut être nul!");

            if (Courses.Contains(course))
                return true;

            return false;
        }
        /// <summary>
        /// Méthode permettanr d'ajouter une course dans la liste de course
        /// </summary>
        /// <param name="course">Course à vérifier si existante sinon ajouter dans la liste</param>
        /// <exception cref="ArgumentNullException">Excecption retournant une exception si nulle</exception>
        /// <exception cref="InvalidOperationException">Exception retournant une exception si existante</exception>
        public void AjouterCourse(Course course)
        {
            if (course is null)
                throw new ArgumentNullException(nameof(course), "La course ne peut être nul!");

            if (Existe(course))
                throw new InvalidOperationException("Impossible d'ajouter la course, car elle existe déjà dans la liste!");


            Courses.Add(course);
            Courses.Sort();
        }
        /// <summary>
        /// Méthode permettant charger les courses dans une liste
        /// </summary>
        /// <param name="cheminFichierCourses">Chemin CSV permettant d'acceder au informations des courses</param>
        /// <param name="cheminFichierCoureurs">Chemin CSV permettant d'acceder au informations des coureurs</param>
        private void ChargerCourse(string cheminFichierCourses, string cheminFichierCoureurs)
        {
            string[] vectLignesCourses = Utilitaire.ChargerDonnees(cheminFichierCourses);
            string[] champs;

            Course course;


            for (int i = 1; i < vectLignesCourses.Length; i++)
            {
                champs = vectLignesCourses[i].Split(';');
                course = new Course(Guid.Parse(champs[0]), champs[1], DateOnly.FromDateTime(DateTime.Parse(champs[4].ToString())), champs[2], (enums.Province)Convert.ToUInt16(champs[3]), (enums.TypeCourse)Convert.ToUInt16(champs[5]), (ushort)Convert.ToInt16(champs[6]));

                //int comparaison = Courses[i].CompareTo(course);

                ChargerCoureurs(course, cheminFichierCoureurs);
                Courses.Add(course);
                course.TrierCoureurs();

            }
            Courses.Sort();





        }
        /// <summary>
        /// Méthode permettant charger les coureurs dans une liste
        /// </summary>
        /// <param name="course">La course dans les coureurs seront ajoutés</param>
        /// <param name="cheminFichierCoureurs">Chemin CSV permettant d'acceder au informations des coureurs</param>
        private void ChargerCoureurs(Course course, string cheminFichierCoureurs)
        {
            string[] vectLignesCoureurs = Utilitaire.ChargerDonnees(cheminFichierCoureurs);
            string[] champs;

            for (int i = 1; i < vectLignesCoureurs.Length; i++)
            {
                if (vectLignesCoureurs[i] is not null)
                {
                    champs = vectLignesCoureurs[i].Split(';');
                    if (course.Id == Guid.Parse(champs[0]))
                    {
                        course.Coureurs.Add(new Coureur((ushort)Convert.ToInt16(champs[1]), champs[2], champs[3], Enum.Parse<Categorie>(champs[6]), champs[4], Enum.Parse<Province>(champs[5]), TimeSpan.Parse(champs[7]), Boolean.Parse(champs[8])));
                    }
                }
            }
        }
        /// <summary>
        /// Méthode permettanr de supprimer une course dans la liste de course
        /// </summary>
        /// <param name="course">Course à vérifier si non existante sinon supprimer de la liste</param>
        /// <returns>Vrai si la course a bien été supprimée sinon Faux</returns>
        /// <exception cref="ArgumentNullException">Excecption retournant une exception si nulle</exception>
        /// <exception cref="InvalidOperationException">Exception retournant une exception si existante</exception>
        public bool SupprimerCourse(Course course)
        {
            if (course is null)
                throw new ArgumentNullException(nameof(course), "La course ne peut être nul!");

            if (!Existe(course))
                throw new InvalidOperationException("Impossible de supprimer la course, car elle n'existe pas dans la liste!");

            Courses.Remove(course);
            Courses.Sort();

            return true;

        }
        /// <summary>
        /// Méthode permettant l'enregistrement des courses et coureurs dans un CSV
        /// </summary>
        /// <param name="cheminFichierCourses">Chemin CSV permettant d'acceder au informations des courses</param>
        /// <param name="cheminFichierCoureurs">Chemin CSV permettant d'acceder au informations des coureurs</param>
        /// <exception cref="ArgumentException">Exception retournant un message si le chemin d'un fichier est nulle, vide ou contient que des espaces</exception>
        public void EnregistrerCourses(string cheminFichierCourses, string cheminFichierCoureurs)
        {
            if (string.IsNullOrWhiteSpace(cheminFichierCoureurs))
            {
                throw new ArgumentNullException(nameof(cheminFichierCoureurs), "Le chemin du fichier Coureur ne peut être nul ou vide, ni contenir uniquement des espaces.");
            }

            if (string.IsNullOrWhiteSpace(cheminFichierCourses))
            {
                throw new ArgumentNullException(nameof(cheminFichierCourses), "Le chemin du fichier Course ne peut être nul ou vide, ni contenir uniquement des espaces.");
            }

            #region
            //if (cheminFichierCoureurs is null)
            //{
            //    throw new ArgumentNullException(nameof(cheminFichierCoureurs), "Le chemin du fichier Coureur ne peut être nul!");
            //}
            //else if (cheminFichierCoureurs == "")
            //{
            //    throw new ArgumentException("Le chemin du fichier ne peut être vide!");
            //}
            //else if (cheminFichierCoureurs.Trim().Contains(' '))
            //{
            //    throw new ArgumentException("Le chemin du fichier ne peut être rempli d'espace!");
            //}

            //if (cheminFichierCourses is null)
            //{
            //    throw new ArgumentNullException(nameof(cheminFichierCourses), "Le chemin du fichier Course ne peut être nul!");
            //}
            //else if (cheminFichierCourses == "")
            //{
            //    throw new ArgumentException("Le chemin du fichier ne peut être vide!");
            //}
            //else if (cheminFichierCourses.Trim().Contains(' '))
            //{
            //    throw new ArgumentException("Le chemin du fichier ne peut être rempli d'espace!");
            //}
            #endregion

            string infoCourses = "Id;nom;ville;province;date;type;distance\n";
            string infoCoureurs = "IdCourse;dossard;nom;prenom;ville;province;categorie;temps;abandon\n";

            foreach (Course course in Courses)
            {
                infoCourses += $"{course.Id};{course.Nom};{course.Ville};{Convert.ToInt32(course.Province)};{course.Date};{Convert.ToInt32(course.TypeCourse)};{course.Distance}\n";
                foreach (Coureur coureur in course.Coureurs)
                {
                    infoCoureurs += $"{course.Id};{coureur.Dossard};{coureur.Nom};{coureur.Prenom};{coureur.Ville};{Convert.ToInt32(coureur.Province)};{Convert.ToInt32(coureur.Categorie)};{coureur.Temps};{coureur.Abandon}\n";

                }
            }

            Utilitaire.EnregistrerDonnees(cheminFichierCourses, infoCourses);
            Utilitaire.EnregistrerDonnees(cheminFichierCoureurs, infoCoureurs);

        }
    }
}

