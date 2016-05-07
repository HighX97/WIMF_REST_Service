using System.Collections.Generic;
using MySql.Data.MySqlClient;
//Add MySql Library

namespace WIMF_ClassLibrary
{

    public class Abstract_db
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public Abstract_db()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "localhost";
            database = "db_wimf";
            uid = "wimf_db_admin";
            password = "password";   
        }

        //open connection to database
        protected bool OpenConnection()
        {
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
          database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            return true;
        }

        //Close connection
        protected bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        //Insert statement
        public bool Insert(string table, string columns, string values)
        {
            string query = "INSERT INTO " + table + " " + columns + " VALUES" + values;
            return ExecuteQuery(query);
        }

        //Update statement
        public bool Update(string table, string set, string where)
        {
            string query = "UPDATE " + table + " SET " + set + "' WHERE " + where;
            return ExecuteQuery(query);
        }

        //Delete statement
        public bool Delete(string table, string where)
        {
            string query = "DELETE FROM " + table + " WHERE " + where;
            return ExecuteQuery(query);
        }

        public bool ExecuteQuery(string query)
        {
            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;
                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
                return true;
            }
            else
            {
                return false;
            }
        }

        //Select statement
        public List<Dictionary<string, string>> Select(List<string> select, string from, string where, string order_by, int limit1, int limit2)
        {
            string select_string = "";
            int i = 1;
            foreach (string s in select)
            {
                select_string += s;
                if (i < select.Count)
                {
                    select_string += ",";
                }
                i++;
            }
            string query = "select " + select_string
                + " from " + from
                + " where " + where
                + " order by " + order_by
                + " limit "+ limit1 + ","+ limit2;

            //Create a list to store the result
            List<Dictionary<string, string>> resultat = new List<Dictionary<string, string>>();

            //Open connection
            bool con = this.OpenConnection();
            if (con == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    Dictionary<string, string> line_resultat = new Dictionary<string, string>();
                    foreach (string s in select)
                    {
                        string value = dataReader[s].ToString();
                        line_resultat.Add(s, dataReader[s] + "");
                    }
                    resultat.Add(line_resultat);
                }
                

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return resultat;
            }
            else
            {
                return resultat;
            }
        }

        /*
        //Count statement
        public int Count()
        {
            string query = "SELECT Count(*) FROM tableinfo";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        //Backup
        public void Backup()
        {
        }

        //Restore
        public void Restore()
        {
        }
        */
    }
}
