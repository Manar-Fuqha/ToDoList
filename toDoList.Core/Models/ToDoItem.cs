using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toDoList.Core.Models
{
    public class ToDoItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "ItemName is required")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "ItemDescription is required")]
        public string ItemDescription { get; set; }

        [ForeignKey("User")]
        public string email { get; set; }
        public User User { get; set; }
    }
}
