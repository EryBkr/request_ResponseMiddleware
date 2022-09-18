using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingTestApi.Models
{
    public class UserLoginResponseModel
    {
        public bool Success { get; set; }
        public string UserEmail { get; set; }
    }
}
