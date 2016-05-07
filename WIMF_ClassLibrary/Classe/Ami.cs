using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIMF_ClassLibrary
{
    public class Ami : Utilisateur
    {
        //Champs
        //object
        protected int idPos;
        protected int etat;

        private DateTime dateDemande;
        private DateTime dateAmitie;
        //class

        //GET & SET
        public int IdPos
        {
            get { return idPos; }
            set { idPos = value; }
        }
        public int Etat
        {
            get { return etat; }
            set { etat = value; }
        }
        public DateTime DateDemande
        {
            get { return dateDemande; }
            set { dateDemande = value; }
        }
        public DateTime DateAmitie
        {
            get { return dateAmitie; }
            set { dateAmitie = value; }
        }

        //Constructeur

        public Ami() : base()
        {
        }

        public Ami(int p_idU) : base(p_idU)
        {
        }

        public Ami(int idU,string nom,string tel,string gps,
            int idPos,int etat, DateTime dateDemande,DateTime dateAmitie) : base(idU, nom, tel, gps)
        {
          this.IdPos = idPos;
          this.Etat = etat;
          this.DateDemande = dateDemande;
          this.DateAmitie = dateAmitie;
        }
    }
}
