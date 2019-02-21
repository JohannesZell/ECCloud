using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebAPI.Controllers
{
    public class CloudAPIController : ApiController
    {
        HttpResponseMessage response;

        [HttpGet]
        public HttpResponseMessage GetFiles(string User)
        {
            ResultData result;

            try
            {
                IList<CloudData> cloudDataList = new List<CloudData>();

                for (int i = 0; i < 5; i++)
                {
                    CloudData cloudData = new CloudData();
                    cloudData.FileName = "File " + (i + 1);
                    cloudData.FileType = "docx";
                    cloudData.FileSize = 343443;

                    cloudDataList.Add(cloudData);
                }
                
                response = Request.CreateResponse(HttpStatusCode.OK, cloudDataList);                
            }
            catch (Exception ex)
            {
                result = new ResultData();
                result.Status = 0;
                result.Message = ex.Message;
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
            return response;
        }  

        private bool checkUser()
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
            return false;
        }
    }
}
