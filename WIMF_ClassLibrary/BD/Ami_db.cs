using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIMF_ClassLibrary
{
    public class Ami_db: Abstract_db
    {

        //Champs
        public readonly string table = "Ami";
        public List<string> columns;


        //Constructeur
        public  Ami_db() :  base()
        {
            columns = new List<string>();
            columns.Add("U.idU");
            columns.Add("U.nom");
            columns.Add("U.tel");
            columns.Add("U.gps");
            columns.Add("A.idU1");
            columns.Add("A.etat");
            columns.Add("A.dateCrea");
            columns.Add("A.dateMaj");

        }

        //
        /*
     public Ami getAmi(int id)
     {

         Ami ami = null;
         List<String> select = this.columns;
         string from = table;
         string where = "idU = " + id;
         string order_by = null;
         int limit1 = 0;
         int limit2 = 50;
         List < Dictionary < string, string>>  resultat = base.Select(select, from, where, order_by, limit1, limit2);
         foreach (Dictionary<string, string> line_resultat in resultat)
         {
             ami = new Ami();
             string idU1;
             line_resultat.TryGetValue("idU1", out idU1);
             ami.IdU1 = Int32.Parse(idU1);
             string idU2;
             line_resultat.TryGetValue("idU2", out idU2);
             ami.IdU2 = idU2;
             string etat;
             line_resultat.TryGetValue("etat", out etat);
             ami.Etat = etat;

             string dateCrea;
             line_resultat.TryGetValue("dateCrea", out dateCrea);
             ami.DateCrea = Convert.ToDateTime(dateCrea);

             string dateMaj;
             line_resultat.TryGetValue("dateMaj", out dateMaj);
             ami.DateMaj = Convert.ToDateTime(dateMaj);

         }
         return ami;

    }
     */
        //Insert statement
        public bool Insert(List<String> columns, List<String> values)
        {
            return base.Insert(this.table, columns, values);
        }

        //Update statement
        public bool Update(List<String> columns_values, string where)
        {
           return base.Update(this.table, columns_values, where);
        }

        //Delete statement
        public bool Delete(string where)
        {
            return base.Delete(this.table, where);
        }

        public List<Ami> getUtilisateur_amis(int iutilisateurIdInt)
        {
            List<Ami> user_amis = new List<Ami>();
            Ami ami = null;
            List<String> select = this.columns;
            string from = "Utilisateur U, Amis A";
            string where = "(A.idU1 ="+ iutilisateurIdInt + " AND U.idU = A.idU2) OR (A.idU2 = " + iutilisateurIdInt + " AND U.idU = A.idU1)";
            string order_by = null;
            int limit1 = 0;
            int limit2 = 50;
            List<Dictionary<string, string>> resultat = base.Select(select, from, where, order_by, limit1, limit2);
            foreach (Dictionary<string, string> line_resultat in resultat)
            {
                ami = new Ami();
                string idU;
                line_resultat.TryGetValue("idU", out idU);
                ami.IdU = Int32.Parse(idU);
                string nom;
                line_resultat.TryGetValue("nom", out nom);
                ami.Nom = nom;
                string tel;
                line_resultat.TryGetValue("tel", out tel);
                ami.Tel = tel;
                string gps;
                line_resultat.TryGetValue("gps", out gps);
                ami.Gps = gps;
                string idPos;
                line_resultat.TryGetValue("idU1", out idPos);
                ami.IdPos = Int32.Parse(idPos);
                string etat;
                line_resultat.TryGetValue("etat", out etat);
                ami.Etat = Int32.Parse(etat);
                string dateDemande;
                line_resultat.TryGetValue("dateCrea", out dateDemande);
                ami.DateDemande = Convert.ToDateTime(dateDemande);
                string dateAmitie;
                line_resultat.TryGetValue("dateMaj", out dateAmitie);
                ami.DateAmitie = Convert.ToDateTime(dateAmitie);
                user_amis.Add(ami);

            }
            return user_amis;
        }
    }

}
