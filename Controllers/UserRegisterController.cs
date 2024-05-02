using DemoProject.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoProject.Controllers
{
    public class UserRegisterController : Controller
    {
        // GET: UserRegister
        public ActionResult Index()
        {
            return View("SignUp");
        }
        AppSettingsReader aps = new AppSettingsReader();
        public string SignUpform(SignUPData lgdata)
        {
            SqlConnection con = new SqlConnection(aps.GetValue("ConnectionString",typeof(System.String)).ToString());
            //var FullName = lgdata.FullName;
            con.Open();
            string sql = "insert into tblUsers(Fname, LName, Phone, Gender, Usertype, Username, Email, Password, CreatedDate, UpdatedDate) Values (@Fname, @LName, @Phone, @Gender, @Usertype, @Username, @Email, @Password, @CreatedDate, @UpdatedDate)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Fname", lgdata.FullName);
            cmd.Parameters.AddWithValue("@LName", lgdata.FullName);
            cmd.Parameters.AddWithValue("@Phone", lgdata.PhoneNumber);
            cmd.Parameters.AddWithValue("@Gender", lgdata.gender);
            cmd.Parameters.AddWithValue("@Usertype", lgdata.Usertype);
            cmd.Parameters.AddWithValue("@Username", lgdata.Username);
            cmd.Parameters.AddWithValue("@Email", lgdata.Email);
            cmd.Parameters.AddWithValue("@Password", lgdata.Password);
            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
            cmd.ExecuteNonQuery();
            con.Close();
            if (lgdata.Password != lgdata.rePassword)
            {
                return "false";
            }
            byte[] encData_byte = new byte[lgdata.Password.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(lgdata.Password);
            string encodedData = Convert.ToBase64String(encData_byte);

            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);

            if (lgdata.Username.Length > 0)
            {
                return "true";
            }
            else
            {
                return "false";
            }
            //return View("~/Views/Home/Index.cshtml", newddata);
        }

        [HttpPost]
        public string AJAXTEST(string UserName, string Password)
        {
                var FullName = UserName;
            var passd = Password;
            if (UserName.Length > 0 && passd.Length > 0)
            {
                return "Got Username And Password";
            }
            else
                return "Didnt Got Username And Password";
            //return View("~/Views/Home/Index.cshtml", newddata);
        }
    }
}