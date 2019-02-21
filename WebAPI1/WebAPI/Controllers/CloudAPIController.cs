using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebAPI.Controllers
{
    public class CloudAPIController : ApiController
    {
        HttpResponseMessage response;
        MySqlConnection connection;
        const string server = "localhost";
        const string database = "eccloud";
        const string uid = "Eccloud";
        const string password = "eccloud";
        const string connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

        [HttpGet]
        public HttpResponseMessage GetFiles(string User, int PWHash)
        {
            HttpResponseMessage response;
            bool result = false;
            string statement = @"Select PWHash From User Where User Like """ + User + @""";";
            string PWHashFromDB = "";
            Debug.Print(statement);
            ActivateConnection();
            //result = DatenBankAbfrage(Statement);

            ResultData resultMessage;
            string Message = "";
            try
            { 
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(statement, connection);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PWHashFromDB = reader.GetString(0);
                    //string someStringFromColumnOne = reader.GetString(1);
                }
                connection.Close();

                if(PWHash == Convert.ToInt32(PWHashFromDB))
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, true);
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, false);
                }

            }
            catch (Exception ex)
            {
                resultMessage = new ResultData();
                resultMessage.Status = 0;
                resultMessage.Message = ex.Message;
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }


           // response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }


        private void ActivateConnection()
        {
            connection = new MySqlConnection(connectionString);
        }
        private bool DatenBankAbfrage(string pQuery)
        {

            string query = pQuery;
            
            return false;
        }
    }
}
