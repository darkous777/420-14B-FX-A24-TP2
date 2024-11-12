
using _420_14B_FX_A24_TP2.enums;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace _420_14B_FX_A24_TP2.classes
{
    /// <summary>
    /// Classe représentant une course à pied
    /// </summary>
    public class Course : IComparable<Course>
    {

        public const byte NOM_NB_CAR_MIN = 3;
        public const byte VILLE_NB_CAR_MIN = 4;
        public const byte DISTANCE_VAL_MIN = 1;
        public const byte DOSSARD_VAL_MIN = 1;

        /// <summary>
        /// Identifiant unique de la course
        /// </summary>
        private Guid _id;


        /// <summary>
        /// Nom de la course
        /// </summary>
        private string _nom;

        /// <summary>
        /// Date de la course
        /// </summary>
        private DateOnly _date;

        /// <summary>
        /// Ville où a lieu la course
        /// </summary>
        private string _ville;

        /// <summary>
        /// Province où a lieu la course
        /// </summary>
        private Province _province;

        /// <summary>
        /// Type de course
        /// </summary>
        private TypeCourse _typeCourse;


        /// <summary>
        /// Distance de la course
        /// </summary>
        private ushort _distance;


        /// <summary>
        /// Liste des coureurs 
        /// </summary>
        private List<Coureur> _coureurs;




        /// <summary>
        /// Obtient ou définit l'identifiant unique d'une course
        /// </summary>
        public Guid Id
        {
            get { return _id; }
            set
            {

                if (value == Guid.Empty)
                {
                    throw new ArgumentException("Le id ne peut etre null!");
                }
                _id = value;
            }
        }


        /// <summary>
        ///Obtien ou modifie le nom de la course.
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _nom.</value>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque que le nom est nul ou n'a aucune valeur.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancé lors que le nom a moins de NOM_NB_CAR_MIN caractères.</exception>

        public string Nom
        {
            get { return _nom; }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(Nom), $"Le nom ne peut être null ou vide.");

                if (value.Trim().Length < NOM_NB_CAR_MIN)
                    throw new ArgumentOutOfRangeException(nameof(Nom), $"Le nom doit contenir au moins {NOM_NB_CAR_MIN} caractères");



                _nom = value.Trim().ToUpper();
            }
        }


        /// <summary>
        ///Obtien ou modifie la date de la course
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _date.</value>
        public DateOnly Date
        {
            get { return _date; }
            set { _date = value; }
        }


        /// <summary>
        ///Obtien ou modifie la ville où a lieu la course
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _ville.</value>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque que la ville est nulle ou n'a aucune valeur.</exception>
        /// <exception cref="System.ArgumentException">Lancé lors que la ville a moins de VILLE_NB_CAR_MIN caractères.</exception>
        public string Ville
        {
            get { return _ville; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(Ville), $"La ville ne peut être null ou vide.");

                if (value.Trim().Length < VILLE_NB_CAR_MIN)
                    throw new ArgumentOutOfRangeException(nameof(Ville), $"La ville doit contenir au moins {VILLE_NB_CAR_MIN} caractères.");

                _ville = value.Trim();
            }
        }




        /// <summary>
        ///Obtien ou modifie la province où a lieu la course
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _province.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lorsque la valeur de la province n'est pas dans les valeurs de l'énumériationL.</exception>
        public Province Province
        {
            get { return _province; }
            set
            {
                if (!(Enum.IsDefined(typeof(Province), value)))
                    throw new ArgumentOutOfRangeException(nameof(Province), $"La valeur {value} n'est pas existante dans les choix.");

                _province = value;
            }
        }


        /// <summary>
        ///Obtien ou modifie le type de course.
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _type.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lorsque que le type de course n'est pas entre TYPE_COURSE_MIN_VAL et TYPE_COURSE_MAX_VAL.</exception>
        public TypeCourse TypeCourse
        {
            get { return _typeCourse; }
            set
            {
                if (!(Enum.IsDefined(typeof(TypeCourse), value)))
                    throw new ArgumentOutOfRangeException(nameof(TypeCourse), $"La valeur {value} n'est pas existante dans les choix.");

                _typeCourse = value;
            }
        }

        /// <summary>
        ///Obtien ou modifie la distance de la course en km
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _distance.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lorsque que la distance est inférieure à DISTANCE_VAL_MIN.</exception>
        public ushort Distance
        {
            get { return _distance; }
            set
            {
                if (value < DISTANCE_VAL_MIN)
                    throw new ArgumentOutOfRangeException(nameof(Distance), $"La distance doit être supérieur à {DISTANCE_VAL_MIN}");

                _distance = value;
            }
        }

        /// <summary>
        ///Obtien ou modifie la liste des coureurs
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _coureurs.</value>
        public List<Coureur> Coureurs
        {
            get { return _coureurs; }
            set { _coureurs = value; }
        }




        /// <summary>
        ///Obtien le nombre de coureurs participants à la course
        /// </summary>
        /// <value>Obtien la valeur de l'attribut :  _coureurs.Count.</value>
        public int NbParticipants
        {
            get
            {
                return _coureurs.Count;
            }

        }

        /// <summary>
        ///Obtien le temps de course moyen
        /// </summary>
        /// <value>Obtien la valeur retourné par la méthode : CalculerTempsCourseMoyen() </value>
        public TimeSpan TempCourseMoyen
        {
            get
            {
                return CalculerTempsCourseMoyen();
            }

        }




        /// <summary>
        /// Permet de constuire un objet de type Course
        /// </summary>
        /// <param name="nom">Nom de la course</param>
        /// <param name="date">Date à laquelle a lieu la course</param>
        /// <param name="ville">Ville où a lieu la course</param>
        /// <param name="province">Province ou a lieu la course</param>
        /// <param name="typeCourse">Type de course</param>
        /// <param name="distance">Distance de la course</param>
        /// <remarks>Initialise une liste de coureurs vide</remarks>
        public Course(Guid id, string nom, DateOnly date, string ville, Province province, TypeCourse typeCourse, ushort distance)
        {
            Coureurs = new List<Coureur>();

            Id = id;
            Nom = nom;
            Date = date;
            Ville = ville;
            Province = province;
            TypeCourse = typeCourse;
            Distance = distance;

        }
        /// <summary>
        /// Méthode permettant de vérifier si une course est déjà existante dans la liste de courses
        /// </summary>
        /// <param name="course">Course à vérifier si existante</param>
        /// <returns>Si la course est existante dans la liste de course (true/false)</returns>
        /// <exception cref="ArgumentNullException">Excecption retournant une exception si nulle</exception>
        public bool Existe(Coureur coureur)
        {
            if (coureur is null)
                throw new ArgumentNullException(nameof(coureur), "Le coureur ne peut être nul!");

            if (Coureurs.Contains(coureur))
                return true;

            return false;
        }
        /// <summary>
        /// Ajoute un coureur à la liste des participants après vérification de son unicité.
        /// </summary>
        /// <param name="coureur">Le coureur à ajouter.</param>
        /// <exception cref="ArgumentNullException">Lancée si le coureur est null.</exception>
        /// <exception cref="InvalidOperationException">Lancée si le coureur ou son numéro de dossard existe déjà dans la liste.</exception>
        public void AjouterCoureur(Coureur coureur)
        {
            if (coureur is null)
                throw new ArgumentNullException(nameof(coureur), "Le coureur ne peut être nul!");

            if (Existe(coureur))
                throw new InvalidOperationException("Impossible d'ajouter le coureur, car les informations du coureur existe  déjà dans la liste!");

            if (ObtenirCoureurParNoDossard(coureur.Dossard) is not null)
                throw new InvalidOperationException("Impossible d'ajouter le coureur, car le dossard du coureur existe  déjà dans la liste!");

            Coureurs.Add(coureur);
            TrierCoureurs();
        }
        /// <summary>
        /// Recherche et retourne un coureur en fonction de son numéro de dossard.
        /// </summary>
        /// <param name="noDossard">Numéro de dossard du coureur recherché.</param>
        /// <returns>Le coureur correspondant, ou <c>null</c> s'il n'est pas trouvé.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Lancée si le numéro de dossard est inférieur à la valeur minimale autorisée.</exception>
        public Coureur ObtenirCoureurParNoDossard(ushort noDossard)
        {
            if (noDossard < DOSSARD_VAL_MIN)
                throw new ArgumentOutOfRangeException(nameof(noDossard), "Le numero de dossard doit etre superieur a 1!");
            foreach (Coureur coureur in Coureurs)
            {
                if (coureur.Dossard == noDossard)
                    return coureur;
            }


            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coureur"></param>
        public void SupprimerCoureur(Coureur coureur)
        {
            if (coureur is null)
                throw new ArgumentNullException(nameof(coureur), "Le coureur ne peut être nul!");

            if (!Existe(coureur))
                throw new InvalidOperationException("Impossible de supprimer le coureur, car les informations du coureur n'existe pas dans la liste!");

            Coureurs.Remove(coureur);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private TimeSpan CalculerTempsCourseMoyen()
        {
            double tempsTotalMs = 0;
            double compteur = 0;

            foreach (Coureur coureur in Coureurs)
            {
                if (!coureur.Abandon && coureur.Temps != TimeSpan.Zero)
                {
                    tempsTotalMs += coureur.Temps.TotalMilliseconds;
                    compteur++;
                }
            }

            TimeSpan tempsMoyen = TimeSpan.FromMilliseconds(tempsTotalMs / compteur);

            return tempsMoyen;
        }
        /// <summary>
        /// 
        /// </summary>
        public void TrierCoureurs()
        {
            Coureurs.Sort();
        }
        /// <summary>
        /// Format d'affichage sur WPF
        /// </summary>
        /// <returns>L'affichage attendu de chaque course</returns>
        public override string ToString()
        {
            return $"{Nom,-37} {Ville,-27} {Province,-20} {Date}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="course1"></param>
        /// <param name="course2"></param>
        /// <returns></returns>
        public static bool operator ==(Course course1, Course course2)
        {
            if (object.ReferenceEquals(course1, course2))
                return true;

            if ((object)course1 == null || (object)course2 == null)
                return false;

            if (course1.Nom.ToLower().Trim() == course2.Nom.ToLower().Trim() && course1.Date == course2.Date && course1.Ville == course2.Ville && course1.Province == course2.Province && course1.TypeCourse == course2.TypeCourse && course1.Distance == course2.Distance)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="course1"></param>
        /// <param name="course2"></param>
        /// <returns></returns>
        public static bool operator !=(Course course1, Course course2)
        {
            return !(course1 == course2);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj is null || obj is not Course)
                return false;

            return this == (Course)obj;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Course? other)
        {
            if (other is null)
                return 1;

            int reseltatComparaison = Date.CompareTo(other.Date);

            if (reseltatComparaison != 0)
                return reseltatComparaison * -1;

            //Tri basé sur le nom
            return String.Compare(Nom, other.Nom,
                CultureInfo.InvariantCulture,
                CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase);
        }
    }
}
