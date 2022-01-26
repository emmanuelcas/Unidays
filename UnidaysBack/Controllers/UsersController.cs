using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using UnidaysBack.Models;
using UnidaysBack.Services;

namespace UnidaysBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        public UsersController(IConfiguration iConfig)
        {
            DB.ConnectionString = iConfig.GetConnectionString("DefaultConnection");
        }


        [HttpPost("Add")]
        public async Task<JsonResult> Post([FromBody] CreatedUser value)
        {
            
            string response = null;
            try
            {
                await DB.Insert(value);
                response = "User succesfully created";
            }
            catch (Exception ex)
            {
                response = "ERROR: " + ex.Message;
            }

            return new JsonResult(response);
        }



        [HttpGet("SignIn")]
        public JsonResult Get([FromBody] SignInUser user)
        {
            var response = new SignInResponse();
            try
            {
                DB.SignInValidation(user);
                response.Token = "skdjklosdjflksjflksdjf";
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "ERROR: " + ex.Message;
            }

            return new JsonResult(response);
        }

    }
}
