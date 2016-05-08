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

        //Insert statement
        public bool Insert(List<String> columns, List<String> values)
        {
            string columns_string="";
            if (columns.Count() >0 )
            {
                columns_string += "(";
                int i = 1;
                foreach (string column in columns)
                {
                    columns_string += column;
                    if (i < columns.Count)
                    {
                        columns_string +=",";
                    }
                    i++;
                }
                columns_string += ")";
            }
            string values_string="";
            values_string += "(";
            int j = 1;
            foreach (string value in values)
            {
                values_string += value;
                if (j < values.Count)
                {
                    values_string += ",";
                }
                j++;
            }
            values_string += ")";
            return base.Insert(this.table, columns_string, values_string);
        }

        //Update statement
        public bool Update(List<String> columns_values, string where)
        {
            string columns_values_string = "";
            columns_values_string += "(";
            int i = 1;
            foreach (string s in columns_values)
            {
                columns_values_string += s;
                if (i < columns_values.Count)
                {
                    columns_values_string += ",";
                }
                i++;
            }
            columns_values_string += ")";
           return base.Update(this.table, columns_values_string, where);
        }

        //Delete statement
        public bool Delete(string where)
        {
            return base.Delete(this.table, where);
        }
    }
}
