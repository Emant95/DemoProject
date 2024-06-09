using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DemoProject.Models.HomeModels
{
    public enum days
    {
        friday=6,
        monday =2,
        tuesday=3,
        wednesday=4,
        thursday=5,
        saturday=7,
        sunday=1
    }

    enum Month
    {
        Jan=1,
        Feb,
        Mar,
        Apr,
        May,
        June,
        July,
        Aug,
        Sep,
        Oct,
        Nov,
        Dec
    }
    public class exerciseModel
    {
        public string name { get; set; }
        public string type { get; set; }
        public string muscle { get; set; }
        public string equipment { get; set; }
        public string difficulty { get; set; }
        public string instructions { get; set; }
        public string updateddate { get; set; }

        AppSettingsReader aps = new AppSettingsReader();
        public int InsertexerciseData(List<exerciseModel> em)
        {
            foreach (exerciseModel exercisesin in em)
            {            
            SqlConnection con = new SqlConnection(aps.GetValue("ConnectionString", typeof(System.String)).ToString());
            //var FullName = lgdata.FullName;
            con.Open();
            string sql = "insert into tblexercises(name, type, muscle, equipment, difficulty, instructions, updateddate) Values (@name, @type, @muscle, @equipment, @difficulty, @instructions, @updateddate)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@name", exercisesin.name);
            cmd.Parameters.AddWithValue("@type", exercisesin.type);
            cmd.Parameters.AddWithValue("@muscle", exercisesin.muscle);
            cmd.Parameters.AddWithValue("@equipment", exercisesin.equipment);
            cmd.Parameters.AddWithValue("@difficulty", exercisesin.difficulty);
            cmd.Parameters.AddWithValue("@instructions", exercisesin.instructions);
            cmd.Parameters.AddWithValue("@updateddate", exercisesin.updateddate);
            cmd.ExecuteNonQuery();
            con.Close();
            }
            return 0;
        }
        public List<exerciseModel> GetExercises(string selectedexer)
        {
            var testmd = new List<exerciseModel>();
            SqlConnection con = new SqlConnection(aps.GetValue("ConnectionString", typeof(System.String)).ToString());
            //var FullName = lgdata.FullName;
            using (SqlCommand command = new SqlCommand("SP_GetExercise", con))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameter for the muscle list
                command.Parameters.Add("@MuscleList", SqlDbType.NVarChar, -1).Value = selectedexer;
                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        exerciseModel exercise = new exerciseModel();
                        exercise.name = reader["name"].ToString();
                        exercise.type = reader["type"].ToString();
                        exercise.muscle = reader["muscle"].ToString();
                        exercise.equipment = reader["equipment"].ToString();
                        exercise.difficulty = reader["difficulty"].ToString();
                        exercise.instructions = reader["instructions"].ToString();
                        // Map other properties as needed

                        testmd.Add(exercise);
                    }
                }
                //var testdata = command.ExecuteReader();
                //var testdatase= command.ExecuteNonQuery();

                //return Database.Query<List<exerciseModel>>("SP_GetExercise", prms, System.Data.CommandType.StoredProcedure);

            }
            return testmd;
        }
    }
}