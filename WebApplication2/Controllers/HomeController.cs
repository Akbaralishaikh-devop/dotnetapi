using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        string Baseurl = "http://192.168.56.149:9082/DepartmentController1/";
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<ActionResult> Index()

        {

            List<Department> DepInfo = new List<Department>();

            using (var client = new HttpClient())

            {

                //Passing service base url

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();

                //Define request data format

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient

                HttpResponseMessage Res = await client.GetAsync("GetDepartmentListData");

                //Checking the response is successful or not which is sent using HttpClient

                if (Res.IsSuccessStatusCode)

                {

                    //Storing the response details recieved from web api

                    var DepResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list

                    DepInfo = JsonConvert.DeserializeObject<List<Department>>(DepResponse);

                }

                //returning the employee list to view

                return View(DepInfo);

            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
