using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnidaysFront.Models;

namespace UnidaysFront.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddUser(CreatedUser user)
        {


            var Client = new HttpClient();
            var content = JsonConvert.SerializeObject(user);
            var data = new StringContent(content, Encoding.UTF8, "application/json");
            var response = Client.PostAsync("https://localhost:44374/Users/Add", data).Result;
            var UserResp= JsonConvert.DeserializeObject<CreateUserResponse>(response.Content.ReadAsStringAsync().Result);



            ViewBag.Message = UserResp.Message;

            return View("Index");
        }

    }
}
