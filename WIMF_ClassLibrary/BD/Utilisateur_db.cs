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


        //Constructeur
        public  Utilisateur_db() :  base()
        {
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
