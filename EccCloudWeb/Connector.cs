using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace EccCloudWeb
{
    public class Connector
    {
        public int checkUser(string user, char hash[])
        {
            //Prüfe ob User in Datenbank enthalten

            //Überprüfe Hash-Wert

            int sessionId = 0;
            return sessionId;
        }

        private void connect()
        {


            string myConnectionString = "SERVER=localhost;" +
                                        "DATABASE=mydatabase;" +
                                        "UID=user;" +
                                        "PASSWORD=mypassword;";

            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM mytable";      // @@@ SQLAbfrage Beispiel
            MySqlDataReader Reader;
            connection.Open();
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                    row += Reader.GetValue(i).ToString() + ", ";
                Console.WriteLine(row);
            }
            connection.Close();


        }
    }
}