using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toDoList.Core.Models.DTO.Register
{
    public class RegisterRequestDTO
    {
        public string UserName { get; set; }
        public string email { get; set; }
        [MinLength(6, ErrorMessage = "Minimum six characters")]
        public string password { get; set; }
        public string Role { get; set; }
    }
}
