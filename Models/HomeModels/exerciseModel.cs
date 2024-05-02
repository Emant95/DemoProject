using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DemoProject.Models.HomeModels
{
    public class exerciseModel
    {
        public string name { get; set; }
        public string type { get; set; }
        public string muscle { get; set; }
        public string equipment { get; set; }
        public string difficulty { get; set; }
        public string instructions { get; set; }

        AppSettingsReader aps = new AppSettingsReader();
        public int InsertexerciseData(List<exerciseModel> em)
        {
            foreach (exerciseModel exercisesin in em)
            {            
            SqlConnection con = new SqlConnection(aps.GetValue("ConnectionString", typeof(System.String)).ToString());
            //var FullName = lgdata.FullName;
            con.Open();
            string sql = "insert into tblexercises(name, type, muscle, equipment, difficulty, instructions) Values (@name, @type, @muscle, @equipment, @difficulty, @instructions)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@name", exercisesin.name);
            cmd.Parameters.AddWithValue("@type", exercisesin.type);
            cmd.Parameters.AddWithValue("@muscle", exercisesin.muscle);
            cmd.Parameters.AddWithValue("@equipment", exercisesin.equipment);
            cmd.Parameters.AddWithValue("@difficulty", exercisesin.difficulty);
            cmd.Parameters.AddWithValue("@instructions", exercisesin.instructions);
            cmd.ExecuteNonQuery();
            con.Close();
            }
            return 0;
        }
    }
}