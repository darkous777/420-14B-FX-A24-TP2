﻿
using _420_14B_FX_A24_TP2.enums;

namespace _420_14B_FX_A24_TP2.classes
{
    /// <summary>
    /// Classe représentant un coureur
    /// </summary>
    public class Coureur
    {
        public const ushort DOSSARD_VAL_MIN = 1;
        public const byte NOM_NB_CARC_MIN = 3;
        public const byte PRENOM_NB_CARC_MIN = 3;
        public const byte VILLE_NB_CARC_MIN = 4;

        /// <summary>
        /// Numéro du dossard
        /// </summary>
        private ushort _dossard;

        /// <summary>
        /// Nom du coureur
        /// </summary>
        private string _nom;

        /// <summary>
        /// Prénom du coureur
        /// </summary>
        private string _prenom;

     
        /// <summary>
        /// Catégorie d'âge du coureur
        /// </summary>
        private Categorie _categorie;

        /// <summary>
        /// Ville d'origine du courreur
        /// </summary>
        private string _ville;

        /// <summary>
        /// Province d'origine du coureur.
        /// </summary>
        private Province _province;


        /// <summary>
        /// Temps de course
        /// </summary>
        private TimeSpan _temps;

        /// <summary>
        /// Rang du coureur
        /// </summary>
        private ushort _rang;

       
        /// <summary>
        /// Indicateur d'abandon de la course
        /// </summary>
        private bool _abandon;


       

        /// <summary>
        ///Obtien ou modifie le numéro du dossard.
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _dossard.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lorsque que le numéro du dossard est inférieur à 1.</exception>
        public ushort Dossard
        {
            get { return _dossard; }
            set 
            {                
                _dossard = value; 
            
            }
        }

        /// <summary>
        ///Obtien ou modifie le nom du coureur.
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _nom.</value>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque que le nom est nul ou n'a aucune valeur.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lors que le nom a moins de caractères que NOM_NB_CARC_MIN .</exception>
        public string Nom
        {
            get { return _nom; }
            set 
            {

                _nom = value.Trim(); 
            }
        }



        /// <summary>
        ///Obtien ou modifie le prénom du coureur.
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _prenom.</value>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque que le prénom est nul ou n'a aucune valeur.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lors que le prénom a moins de caractères que PRENON_NB_CARC_MIN .</exception>

        public string Prenom
        {
            get { return _prenom; }
            set 
            {
                

                _prenom = value.Trim(); 
            }
        }


        /// <summary>
        ///Obtien ou modifie la catégorie du coureur.
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _categorie.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lorsque la valeur de la catégorie n'existe pas dans les plages de valeurs possibles.</exception>
        public Categorie Categorie
        {
            get { return _categorie; }
            set 
            {
               

                _categorie = value; 
            }
        }

        /// <summary>
        ///Obtien ou modifie la ville d'origine du coureur.
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _ville.</value>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque que la ville est nul ou n'a aucune valeur.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lors que la ville a moins de caractères que VILLE_NB_CARC_MIN .</exception>
        public string Ville
        {
            get { return _ville; }
            set 
            {
                
                _ville = value.Trim(); 
            }
        }

        /// <summary>
        ///Obtien ou modifie la province d'origine du coureur
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _province.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lorsque la valeur de la province n'est pas entre PROVINCE_MIN_VAL et PROVINCE_MAX_VAL.</exception>
        public Province Province
        {
            get { return _province; }
            set 
            {

                _province = value; 
            }
        }


        /// <summary>
        ///Obtien ou modifie la temps de course du coureur
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _temps.</value>
        public TimeSpan Temps
        {
            get { return _temps; }
            set  { _temps = value; }
        }
        /// <summary>
        /// Obtient ou défini le rang du coureur
        /// </summary>
        public ushort Rang
        {
            get { return _rang; }
            private set { _rang = value; }
        }


        /// <summary>
        ///Obtien ou modifie l'indicateur d'abandon du coureur.
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _abandon.</value>
        public bool Abandon
        {
            get { return _abandon; }
            set { _abandon = value; }
        }

 

        /// <summary>
        /// Permet de construire un objet Coureur
        /// </summary>
        /// <param name="dossard">No. de dossard du coureur</param>
        /// <param name="nom">Nom du coureur</param>
        /// <param name="prenom">Prénom du coureur</param>
        /// <param name="categorie">Catégorie à laquelle appartient le coureur</param>
        /// <param name="ville">Ville du coureur</param>
        /// <param name="province">Province du coureur</param>
        /// <param name="temps">Temps de course du coureur</param>
        /// <param name="abandon">Indicateur d'abandon de la course. Faux par défaut</param>
     
        public Coureur(ushort dossard, string nom, string prenom, Categorie categorie, string ville, Province province, TimeSpan temps, bool abandon = false)
        {
           Dossard = dossard;
            Nom = nom;
            Prenom = prenom;
            Categorie = categorie;
            Ville = ville;
            Province = province;
            Temps = temps;
            Abandon = abandon;
            
        }






    }
}
