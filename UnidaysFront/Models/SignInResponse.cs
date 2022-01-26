using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnidaysFront.Models
{
    public class SignInResponse
    {
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
    }
}
