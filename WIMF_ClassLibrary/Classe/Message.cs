using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIMF_REST_Service
{
    public class Message
    {
        //Champs
        //object
        protected int idMsg;
        protected string valeur;
        protected int idU1;
        protected int idU2;
        protected int etat;
        protected DateTime dateCrea;
        protected DateTime dateMaj;
        //class
        private static int message_count = 0;

        //GET & SET
        public int IdMsg
        {
            get { return idMsg; }
            set { idMsg = value; }
        }
        public string Valeur
        {
            get { return valeur; }
            set { valeur = value; }
        }
        public int IdU1
        {
            get { return idU1; }
            set { idU1 = value; }
        }
        public int IdU2
        {
            get { return idU2; }
            set { idU2 = value; }
        }
        public int Etat
        {
            get { return etat; }
            set { etat = value; }
        }
        public DateTime DateCrea
        {
            get { return dateCrea; }
            set { dateCrea = value; }
        }
        public DateTime DateMaj
        {
            get { return dateMaj; }
            set { dateMaj = value; }
        }

        //Constructeur

        public Message()
        {
            ++message_count;
        }

        public Message(int p_idMsg) : this()
        {
            this.IdMsg = p_idMsg;
        }

        public Message(int idMsg, string valeur, int idU1, int idU2, int etat, DateTime dateCrea, DateTime dateMaj) : this()
        {
            this.IdMsg = idMsg;
            this.Valeur = valeur;
            this.IdU1 = idU1;
            this.IdU2 = idU2;
            this.Etat = etat;
            this.DateCrea = dateCrea;
            this.DateMaj = dateMaj;
        }
    }
}
