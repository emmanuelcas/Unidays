
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using UnidaysFront.Models;

namespace UnidaysFront.Controllers
{
    public class AccountController : Controller
    {
        string APIUrl;
        public AccountController(IConfiguration iConfig)
        {
            APIUrl = iConfig["APIUrl"];
        }

        public IActionResult Login(string ReturnUrl = "/")
        {
            LoginModel loginUser = new LoginModel();
            loginUser.ReturnUrl = ReturnUrl;
            return View(loginUser);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginUser)
        {
            if (ModelState.IsValid)
            {
                var SignInResp = await CallApiSignInAsync(loginUser);

                if (SignInResp.ErrorMessage != null)
                {
                    //Error message to show and pass to view
                    ViewBag.Message = SignInResp.ErrorMessage;
                    return View(loginUser);
                }
                else
                {
                    var claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier,Convert.ToString(loginUser.UserName)),
                    new Claim(ClaimTypes.Name,loginUser.UserName),
                    new Claim("Token",SignInResp.Token)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        principal, new AuthenticationProperties() { IsPersistent = loginUser.RememberLogin });

                    return LocalRedirect(loginUser.ReturnUrl);
                }
            }
            return View(loginUser);
        }

        public async Task<IActionResult> LogOut() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }

        private async Task<SignInResponse> CallApiSignInAsync(LoginModel loginUser)
        {
            var user = new SignInUser(loginUser.UserName, loginUser.Password);

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(APIUrl + "/SignIn"),
                Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"),
            };

            var response = await client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var SignInResp = JsonConvert.DeserializeObject<SignInResponse>(responseBody);

            return SignInResp;
        }
    }
}
