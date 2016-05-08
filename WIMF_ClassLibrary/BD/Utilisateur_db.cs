using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIMF_ClassLibrary
{
    public class Utilisateur_db : Abstract_db
    {
        //Champs
        public readonly string table = "Utilisateur";
        public List<string> columns;


        //Constructeur
        public  Utilisateur_db() :  base()
        {
            columns = new List<string>();
            columns.Add("idU");
            columns.Add("nom");
            columns.Add("tel");
            columns.Add("gps");
            columns.Add("password");
            columns.Add("dateCrea");
            columns.Add("dateMaj");
        }

        //
        public Utilisateur getUtilisateur(int id)
        {
            Utilisateur utilisateur = null;
            List<String> select = this.columns;
            string from = table;
            string where = "idU = " + id;
            string order_by = null;
            int limit1 = 0;
            int limit2 = 50;
            List < Dictionary < string, string>>  resultat = base.Select(select, from, where, order_by, limit1, limit2);
            foreach (Dictionary<string, string> line_resultat in resultat)
            {
                utilisateur = new Utilisateur();
                string idU;
                line_resultat.TryGetValue("idU", out idU);
                utilisateur.IdU = Int32.Parse(idU);
                string nom;
                line_resultat.TryGetValue("nom", out nom);
                utilisateur.Nom = nom;
                string tel;
                line_resultat.TryGetValue("tel", out tel);
                utilisateur.Tel = tel;
                string gps;
                line_resultat.TryGetValue("gps", out gps);
                utilisateur.Gps = gps;
                string password;
                line_resultat.TryGetValue("password", out password);
                utilisateur.Password = password;
                string dateCrea;
                line_resultat.TryGetValue("dateCrea", out dateCrea);
                utilisateur.DateCrea = Convert.ToDateTime(dateCrea);
                ///*
                string dateMaj;
                line_resultat.TryGetValue("dateMaj", out dateMaj);
                utilisateur.DateMaj = Convert.ToDateTime(dateMaj);
                //*/
                
            }
            return utilisateur;
        }

        public List<Utilisateur> getUtilisateur_all()
        {
            List<Utilisateur> all_utilisateur = new List<Utilisateur>();
            Utilisateur utilisateur = null;
            List<String> select = this.columns;
            string from = table;
            string where = "idU > -1";
            string order_by = null;
            int limit1 = 0;
            int limit2 = 50;
            List<Dictionary<string, string>> resultat = base.Select(select, from, where, order_by, limit1, limit2);
            foreach (Dictionary<string, string> line_resultat in resultat)
            {
                utilisateur = new Utilisateur();
                string idU;
                line_resultat.TryGetValue("idU", out idU);
                utilisateur.IdU = Int32.Parse(idU);
                string nom;
                line_resultat.TryGetValue("nom", out nom);
                utilisateur.Nom = nom;
                string tel;
                line_resultat.TryGetValue("tel", out tel);
                utilisateur.Tel = tel;
                string gps;
                line_resultat.TryGetValue("gps", out gps);
                utilisateur.Gps = gps;
                string password;
                line_resultat.TryGetValue("password", out password);
                utilisateur.Password = password;
                string dateCrea;
                line_resultat.TryGetValue("dateCrea", out dateCrea);
                utilisateur.DateCrea = Convert.ToDateTime(dateCrea);
                ///*
                string dateMaj;
                line_resultat.TryGetValue("dateMaj", out dateMaj);
                utilisateur.DateMaj = Convert.ToDateTime(dateMaj);
                //*/
                all_utilisateur.Add(utilisateur);

            }
            return all_utilisateur;
        }

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

        
    }
}
