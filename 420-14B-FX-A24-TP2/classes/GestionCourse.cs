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
        /// Méthode permettant de vérifier si une course est déjà existante dans la liste de course
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
                if (c.Id == course.Id)
                    return true;
            }

            return false;
        }
       
    }
}

