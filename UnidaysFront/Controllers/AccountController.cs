
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using UnidaysFront.Models;

namespace UnidaysFront.Controllers
{
    public class AccountController : Controller
    {


        public IActionResult Login(string ReturnUrl = "/")
        {
            LoginModel objLoginModel = new LoginModel();
            objLoginModel.ReturnUrl = ReturnUrl;
            return View(objLoginModel);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel objLoginModel)
        {
            if (ModelState.IsValid)
            {
                var user = new SignInUser(objLoginModel.UserName, objLoginModel.Password);

                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://localhost:44374/Users/SignIn"),
                    Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"),
                };

                var response = await client.SendAsync(request).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var SignInResp = JsonConvert.DeserializeObject<SignInResponse>(responseBody);


                //var user = users.Where(x => x.Username == objLoginModel.UserName && x.Password == objLoginModel.Password).FirstOrDefault();
                if (SignInResp.ResponseCode != 1)
                {
                    //Add logic here to display some message to user
                    //ViewBag.Message = "Invalid User and/or Password";
                    ViewBag.Message = SignInResp.Message;
                    return View(objLoginModel);
                }
                else
                {
                    //A claim is a statement about a subject by an issuer and
                    //represent attributes of the subject that are useful in the context of authentication and authorization operations.
                    var claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier,Convert.ToString(user.Username)),
                    new Claim(ClaimTypes.Name,user.Username),
                    
                    //new Claim(ClaimTypes.Role,user.Role),
                    new Claim("Token",SignInResp.Token)
                    };

                    //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity
                    var principal = new ClaimsPrincipal(identity);
                    //SignInAsync is a Extension method for Sign in a principal for the specified scheme.
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        principal, new AuthenticationProperties() { IsPersistent = objLoginModel.RememberLogin });

                    return LocalRedirect(objLoginModel.ReturnUrl);
                }
            }
            return View(objLoginModel);
        }

        public async Task<IActionResult> LogOut() {
            //SignOutAsync is Extension method for SignOut
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Redirect to home page
            return LocalRedirect("/");
        }
    }
}
