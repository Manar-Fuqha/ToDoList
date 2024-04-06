using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toDoList.Core.Models.DTO.Items
{
    public class ItemForRetriveDTO
    {
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
    }
}
