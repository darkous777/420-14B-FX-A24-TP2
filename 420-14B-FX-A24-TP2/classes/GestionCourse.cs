using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _420_14B_FX_A24_TP2.classes.Utilitaire;

namespace _420_14B_FX_A24_TP2.classes
{
    public class GestionCourse
    {
        /// <summary>
        /// 
        /// </summary>
		private Course _courses;
        /// <summary>
        /// 
        /// </summary>
		public Course Courses
		{
			get { return _courses; }
			set { _courses = value; }
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cheminFichierCoursesCsv"></param>
        /// <param name="cheminFichierCoureursCsv"></param>
        public GestionCourse(string cheminFichierCoursesCsv, string cheminFichierCoureursCsv)
        {

        }
    }
}
