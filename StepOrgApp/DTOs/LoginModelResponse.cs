using StepOrg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepOrgApp.DTOs
{
    public class LoginModelResponse
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string InviteCode { get; set; }
        public List<UserGroupDTO>? UserGroup { get; set; }
    }
}
