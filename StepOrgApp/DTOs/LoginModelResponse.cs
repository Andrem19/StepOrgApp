using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepOrgApp.DTOs
{
    public class LoginModelResponse
    {
        public bool IsAuthSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string InviteCode { get; set; }
    }
}
