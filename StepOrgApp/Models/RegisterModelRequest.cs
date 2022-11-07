using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepOrgApp.Models
{
    public class RegisterModelRequest
    {
        public string displayName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
