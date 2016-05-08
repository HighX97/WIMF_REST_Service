using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text.RegularExpressions;
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
            catch (MySqlException)
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

        public bool Insert(string table,List<String> columns, List<String> values)
        {
            string columns_string="";
            if (columns.Count >0 )
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
            return this.Insert(table, columns_string, values_string);
        }

        //Update statement
        public bool Update(string table, string set, string where)
        {
            string query = "UPDATE " + table + " SET " + set + " WHERE " + where;
            return ExecuteQuery(query);
        }

        public bool Update(string table, List<String> columns_values, string where)
        {
            string columns_values_string = "";
            int i = 1;
            foreach (string s in columns_values)
            {
                columns_values_string += s;
                if (i < columns_values.Count)
                {
                    columns_values_string += ", ";
                }
                i++;
            }
           return this.Update(table, columns_values_string, where);
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
            string query = "select " + select_string;
            query += " from " + from;
            if (where != null)
            {
                query += " where " + where;
            }
            if (order_by != null)
            {
                query += " order by " + order_by;
            }
            query += " limit " + limit1 + "," + limit2;




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
                        string  new_s = Regex.Replace(s, "^[A-Z].", "");
                        object temp = dataReader[new_s];
                        string value = dataReader[new_s].ToString();
                        line_resultat.Add(new_s, value);
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
