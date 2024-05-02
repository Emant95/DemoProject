using DemoProject.Models;
using DemoProject.Models.HomeModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DemoProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string username)
        {
            LoginData loginData = new LoginData();
            loginData.Username = username;
            return View(loginData);
            //postback
        }

        public ActionResult CHECKLOGIN(LoginData loginData)
        {
            string Uname = loginData.Username;
            string Password = loginData.Password;
            string Usertype = loginData.Usertype;
            if (Uname == "suman" && Password == "12345")
            {
                return View("LandingPage", loginData);
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "helloworld .";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult GETMYEXERCISEstr(string exercisetype)
        {
            //return GETMYEXERCISE(exercisetype);
            string instructions = "";
            //database call
            //datecheck


            instructions = Task.Run(() => GETMYEXERCISE(exercisetype)).Result;

            //return instructions;

            return Json(instructions, JsonRequestBehavior.AllowGet);
        }

        static async Task<string> GETMYEXERCISE(string exercisetype)
        {
            //string animalname = Console.ReadLine();
            var myselectedexercise = JsonConvert.DeserializeObject<List<string>>(exercisetype);
            var exerciseDetailsall = new List<exerciseModel>();
            //for (int i = 0; i < myselectedexercise.Count(); i++)
            //{
            //    var exercise = myselectedexercise[0].ToString();
            //    instructions = Task.Run(() => GETMYEXERCISE(exercisetype)).Result;
            //}

            foreach (var items in myselectedexercise)
            {
                string apiUrl = "https://api.api-ninjas.com/v1/exercises?muscle=" + items;
                string apiKey = "d6EUgg//bUf/UiYp4RGUjw==viZGhUBssgqBS4Kn";
                var exerciseDetails = new List<exerciseModel>();
                //to add condition to call db
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
                    var response = await httpClient.GetAsync(apiUrl);
                    var a = (double)4;
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();

                        exerciseDetails = JsonConvert.DeserializeObject<List<exerciseModel>>(responseContent);
                        List<string> equipmentlist = new List<string>();
                        equipmentlist = exerciseDetails.Select(x => x.equipment.ToString()).Distinct().ToList();
                        exerciseModel em = new exerciseModel();
                        int inserttest = em.InsertexerciseData(exerciseDetails);
                        exerciseDetailsall.AddRange(exerciseDetails);
                    }
                    else
                    {
                        return "Nothing Found";

                    }
                }
            }
            string jsonData = JsonConvert.SerializeObject(exerciseDetailsall);
            return jsonData;

        }
        public class ExerciseDetails
        {
            public string name { get; set; }
            public string type { get; set; }
            public string muscle { get; set; }
            public string equipment { get; set; }
            public string difficulty { get; set; }
            public string instructions { get; set; }
        }



    }
}

//exerciseDetails = exerciseDetails.Where(x => (x.equipment == "dumbbell" || x.equipment == "barbell") && x.type=="strength").ToList();
//RootJokes facebookFriends = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<RootJokes>(responseContent);
//return exerciseDetails[0].instructions.ToString();
////Console.WriteLine(responseContent);
//Console.WriteLine("Instructions:");
//Console.WriteLine(exerciseDetails[0].instructions.ToString());
//Console.ReadLine();
// Serialize the list to JSON
//string jsonData = JsonConvert.SerializeObject(exerciseDetails);
//return jsonData;
// Return JSON data as AJAX response