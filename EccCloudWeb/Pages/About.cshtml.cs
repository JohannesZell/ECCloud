using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace EccCloudWeb.Pages

{
    public class AboutModel : PageModel
    {
       

        public string Message { get; set; }

        public void OnGet()
        {
            string server = "localhost";
            string database = "eccloud";
            string uid = "Eccloud";
            string password = "eccloud";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "Select * From user";
            MySqlCommand cmd = new MySqlCommand(query, connection);

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string someStringFromColumnZero = reader.GetString(0);
                string someStringFromColumnOne = reader.GetString(1);
                Message = reader.GetString(0) + reader.GetString(1);
            }
            connection.Close();
        }


        [WebService(Namespace = "http://xmlforasp.net")]
        public static string GetCurrentTime(string name)
        {
            return "Hello " + name + Environment.NewLine + "The Current Time is: "
                + DateTime.Now.ToString();
        }

    }
}
