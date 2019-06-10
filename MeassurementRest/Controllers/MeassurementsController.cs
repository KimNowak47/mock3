using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MeassurementRest.Models;
using System.Data;

namespace MeassurementRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeassurementsController : ControllerBase
    {
        string connectionString = "Server=tcp:kims-testdb.database.windows.net,1433;Initial Catalog=test-db;Persist Security Info=False;User ID=kim;Password=Guest123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

    // GET: api/Meassurements
    [HttpGet]
        public List<Meassurement> GetAll()
        {
            List<Meassurement> mList = new List<Meassurement>();

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("SELECT * from Meassurement", conn);
            command.Connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        int id = reader.GetInt32(0);
                        double pressure = reader.GetDouble(1);
                        double humidity = reader.GetDouble(2);
                        double temperatur = reader.GetDouble(3);
                        DateTime time = reader.GetDateTime(4);


                        var Meas = new Meassurement()
                        {
                            Id = id,
                            Pressure = pressure,
                            Humidity = humidity,
                            Temperature = temperatur,
                            TimeStamp = time
                        };

                        mList.Add(Meas);
                    }
                }
            }

                return mList;
        }

        // GET: api/Meassurements/5
        [HttpGet("{id}", Name = "Get")]
        public Meassurement Get(int id)
        {
            string sqlQuery = $"SELECT * from Meassurement Where id = {id} ";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    int mId = reader.GetInt32(0);
                    double pressure = reader.GetDouble(1);
                    double humidity = reader.GetDouble(2);
                    double temperatur = reader.GetDouble(3);
                    DateTime time = reader.GetDateTime(4);

                    var Meas = new Meassurement()
                    {
                        Id = mId,
                        Pressure = pressure,
                        Humidity = humidity,
                        Temperature = temperatur,
                        TimeStamp = time
                    };

                    return Meas;
                }
                else return null;

            }    


                
        }

        // POST: api/Meassurements
        [HttpPost]
        public void Post([FromBody] Meassurement value)
        {
            string sqlQuery = "INSERT INTO dbo.Meassurement (Preassure, Humidity, Temperatur) VALUES (@pressure, @humidity, @temperature)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.Add("@pressure", SqlDbType.Float).Value = value.Pressure;
                command.Parameters.Add("@humidity", SqlDbType.Float).Value = value.Humidity;
                command.Parameters.Add("@temperature", SqlDbType.Float).Value = value.Temperature;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        // POST: api/Meassurements/add
        [HttpPost]
        [Route("add")]
        public string add([FromBody] test objekt)
        {
            return objekt.testvalue;
        }


        // DELETE: api/delete/5
        [HttpDelete("{id}")]
        [Route("delete/{id}")]
        public void Delete(int id)
        {
            string sqlQuery = $"DELETE from Meassurement Where id = {id} ";

            SqlConnection connection = new SqlConnection(connectionString);
            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {    command.Connection.Open();

                command.ExecuteNonQuery();
            }
        }
    }
}
