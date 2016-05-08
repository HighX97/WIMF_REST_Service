using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIMF_REST_Service;

namespace WIMF_ClassLibrary
{
    public class Message_db: Abstract_db
    {
        //Champs
        public readonly string table = "Message";
        public List<string> columns;


        //Constructeur
        public  Message_db() :  base()
        {
            columns = new List<string>();
            columns.Add("idMsg");
            columns.Add("valeur");
            columns.Add("idU1");
            columns.Add("idU2");
            columns.Add("etat");
            columns.Add("dateCrea");
            columns.Add("dateMaj");
        }

        //
        public Message getMessage(int id)
        {
            Message message = null;
            List<String> select = this.columns;
            string from = table;
            string where = "idMsg = " + id;
            string order_by = null;
            int limit1 = 0;
            int limit2 = 50;
            List<Dictionary<string, string>> resultat = base.Select(select, from, where, order_by, limit1, limit2);
            foreach (Dictionary<string, string> line_resultat in resultat)
            {
                message = new Message();
                string idMsg;
                line_resultat.TryGetValue("idMsg", out idMsg);
                message.IdMsg = Int32.Parse(idMsg);
                string valeur;
                line_resultat.TryGetValue("valeur", out valeur);
                message.Valeur = valeur;
                string idU1;
                line_resultat.TryGetValue("idU1", out idU1);
                message.IdU1 = Int32.Parse(idU1);
                string idU2;
                line_resultat.TryGetValue("idU2", out idU2);
                message.IdU2 = Int32.Parse(idU2);
                string etat;
                line_resultat.TryGetValue("etat", out etat);
                message.Etat = Int32.Parse(etat);
                string dateCrea;
                line_resultat.TryGetValue("dateCrea", out dateCrea);
                message.DateCrea = Convert.ToDateTime(dateCrea);
                string dateMaj;
                line_resultat.TryGetValue("dateMaj", out dateMaj);
                message.DateMaj = Convert.ToDateTime(dateMaj);

            }
            return message;
        }

        public List<Message> getUser_messages(int idUser)
        {
            List<Message> user_messages = new List<Message>();
            Message message = null;
            List<String> select = this.columns;
            string from = table;
            string where = "idU1 = " + idUser + " OR idU2 = " + idUser;
            string order_by = null;
            int limit1 = 0;
            int limit2 = 50;
            List<Dictionary<string, string>> resultat = base.Select(select, from, where, order_by, limit1, limit2);
            foreach (Dictionary<string, string> line_resultat in resultat)
            {
                message = new Message();
                string idMsg;
                line_resultat.TryGetValue("idMsg", out idMsg);
                message.IdMsg = Int32.Parse(idMsg);
                string valeur;
                line_resultat.TryGetValue("valeur", out valeur);
                message.Valeur = valeur;
                string idU1;
                line_resultat.TryGetValue("idU1", out idU1);
                message.IdU1 = Int32.Parse(idU1);
                string idU2;
                line_resultat.TryGetValue("idU2", out idU2);
                message.IdU2 = Int32.Parse(idU2);
                string etat;
                line_resultat.TryGetValue("etat", out etat);
                message.Etat = Int32.Parse(etat);
                string dateCrea;
                line_resultat.TryGetValue("dateCrea", out dateCrea);
                message.DateCrea = Convert.ToDateTime(dateCrea);
                string dateMaj;
                line_resultat.TryGetValue("dateMaj", out dateMaj);
                message.DateMaj = Convert.ToDateTime(dateMaj);
                user_messages.Add(message);

            }
            return user_messages;
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
