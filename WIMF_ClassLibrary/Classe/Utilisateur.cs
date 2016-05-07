using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIMF_ClassLibrary
{
    public class Utilisateur
    {
        //Champs
        //object
        protected int idU;
        protected string nom;
        protected string tel;
        protected string gps;

        private string password;
        private DateTime dateCrea;
        private DateTime dateMaj;

        protected List<Ami> amis;
        //class
        private static int utilisateur_count = 0;

        //GET & SET
        public int IdU {
          get {return idU;}
          set {idU = value;}
        }
        public string Nom {
          get {return nom;}
          set {nom = value;}
        }
        public string Tel {
          get {return tel;}
          set {tel = value;}
        }
        public string Gps {
          get {return gps;}
          set {gps = value;}
        }
        public string Password {
          get {return password;}
          set {password = value;}
        }
        public DateTime DateCrea {
          get {return dateCrea;}
          set {dateCrea = value;}
        }
        public DateTime DateMaj {
          get {return dateMaj;}
          set {dateMaj = value;}
        }

        //Constructeur

        public Utilisateur()
        {
          ++utilisateur_count;
        }

        public Utilisateur(int idU) : this()
        {
            this.IdU = idU;
        }

        public Utilisateur(int idU, string nom, string tel, string gps) : this()
        {
            this.IdU = idU;
            this.nom = nom;
            this.Tel = tel;
            this.Gps = gps;
        }

        public Utilisateur(int idU,string nom,string tel,string gps,string password,DateTime dateCrea,DateTime dateMaj) : this()
        {
          this.IdU = idU ;
          this.Nom = nom ;
          this.Tel = tel ;
          this.Gps = gps ;
          this.Password = password ;
          this.DateCrea = dateCrea ;
          this.DateMaj = dateMaj ;
        }
    }
}
