using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using WIMF_ClassLibrary;

namespace WIMF_server_app_form
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Utilisateur_db usr_db = new Utilisateur_db();
            try
            {
                List<string> select=new List<string>();
                select.Add("idU");
                select.Add("nom");
                select.Add("tel");
                string from = "Utilisateur";
                string where = "idU > -1";
                string order_by = "idU asc";
                int limit1=0;
                int limit2=10;
                List<Dictionary<string, string>> resultat = usr_db.Select(select, from, where, order_by, limit1, limit2);
                foreach (Dictionary<string, string> line in resultat)
                {
                    string line_resultat = "";
                    foreach (KeyValuePair<string, string> entry in line)
                    {
                        line_resultat += entry.Key + " : " + entry.Value;
                        line_resultat += "\t";
                    }
                    MessageBox.Show(line_resultat);
                }
                    
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
            }

        }
    }
}
