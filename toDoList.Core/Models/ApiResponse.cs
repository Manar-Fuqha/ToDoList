using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace toDoList.Core.Models
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            Messages = new List<string>();
        }
        
        public HttpStatusCode Status { get; set; }
        public List<string> Messages { get; set; }
        public object result { get; set; }
    }
}
