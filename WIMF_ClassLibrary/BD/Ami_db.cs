using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIMF_ClassLibrary
{
    public class Ami_db: Abstract_db
    {
        /*
        protected int idU;
        protected string nom;
        protected string tel;
        protected string gps;

        protected int idPos;
        protected int etat;

        private DateTime dateDemande;
        private DateTime dateAmitie;
        */
        //Champs
        public readonly string table = "Ami";
        public List<string> columns;
     

        //Constructeur
        public  Ami_db() :  base()
        {
            columns = new List<string>();
            columns.Add("idU1");
            columns.Add("idU2");
            columns.Add("etat");
            columns.Add("dateCrea");
            columns.Add("dateMaj");

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
    }

}
