using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using toDoList.Core.Models;

namespace toDoList.Core.IGenaricRepository
{
    public interface IToDoItemRepository :IGenaricRepo<ToDoItem> 
    {
        public Task<IReadOnlyList<ToDoItem>> GetAllItemsAsync();
        public Task<ToDoItem> GetItemById(int id);
        public Task UpdateItem(ToDoItem item);
        public Task DeleteItemById(int id);
    }
}
