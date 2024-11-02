using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            foreach (Course c in Courses)
            {
                if (c.Nom == course.Nom && c.Date == course.Date)
                    return true;
            }

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
            Courses = new List<Course>();
            Course course;
            try
            {


                for (int i = 1; i < vectLignesCourses.Length; i++)
                {
                    champs = vectLignesCourses[i].Split(';');
                    course = new Course(Guid.Parse(champs[0]), champs[1], DateOnly.FromDateTime(DateTime.Parse(champs[4].ToString())), champs[2], (enums.Province)Convert.ToUInt16(champs[3]), (enums.TypeCourse)Convert.ToUInt16(champs[5]), (ushort)Convert.ToInt16(champs[6]));

                    ChargerCoureurs(course, cheminFichierCoureurs);

                    Courses.Add(course);
                }
            }
            catch (Exception ex)
            {
            }



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
            course.Coureurs = new List<Coureur>();

            for (int i = 1; i < vectLignesCoureurs.Length; i++)
            {
                if (vectLignesCoureurs[i] is not null)
                {
                    champs = vectLignesCoureurs[i].Split(';');
                    if (course.Id == Guid.Parse(champs[0]))
                    {
                        course.Coureurs.Add(new Coureur((ushort)Convert.ToInt16(champs[1]), champs[2], champs[3], (enums.Categorie)Convert.ToUInt16(champs[6]), champs[4], (enums.Province)Convert.ToUInt16(champs[5]), TimeSpan.Parse(champs[7]), Boolean.Parse(champs[8])));
                    }
                }
            }
        }
    }
}

