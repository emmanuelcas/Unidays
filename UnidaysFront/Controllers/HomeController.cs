using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using UnidaysFront.Models;

namespace UnidaysFront.Controllers
{
    public class HomeController : Controller
    {

        string APIUrl;
        public HomeController(IConfiguration iConfig)
        {
            APIUrl = iConfig["APIUrl"];
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddUser(CreatedUser user)
        {
            var client = new HttpClient();
            var content = JsonConvert.SerializeObject(user);
            var data = new StringContent(content, Encoding.UTF8, "application/json");
            var response = client.PostAsync(APIUrl + "/Add", data).Result;
            var errorMessage= JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);


            if (errorMessage != null)
                ViewBag.Message = errorMessage;

            return View("Index");
        }

    }
}
