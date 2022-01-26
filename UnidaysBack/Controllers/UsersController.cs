using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
            
            var response = new CreateUserResponse();
            try
            {
                var code = await DB.Insert(value);
                if (code==1)
                    response.Message = "User succesfully created";
                else
                    response.Message = "User with that email already exists";
                response.ResponseCode = code;
            }
            catch (Exception ex)
            {
                response.ResponseCode = 3;
                response.Message = ex.Message;
            }

            return new JsonResult(response);

        }



        [HttpGet("SignIn")]
        public JsonResult Get([FromBody] SignInUser user)
        {
            var response = new SignInResponse();
            try
            {
                
                if (DB.SignInValidation(user) == true)
                {
                    response.ResponseCode = 1;
                    response.Token = "skdjklosdjflksjflksdjf";
                }
                else
                {
                    response.ResponseCode = 2;
                    response.Message = "Invalid User and/or Password";
                }

            }
            catch (Exception ex)
            {
                response.ResponseCode = 3;
                response.Message = ex.Message;
            }

            return new JsonResult(response);
        }

    }
}
