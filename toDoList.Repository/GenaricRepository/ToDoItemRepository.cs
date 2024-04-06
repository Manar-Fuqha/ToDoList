using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using toDoList.Core.IGenaricRepository;
using toDoList.Core.Models;
using toDoList.Repository.Data;

namespace toDoList.Repository.GenaricRepository
{
    public class ToDoItemRepository : GenaricRepo<ToDoItem>, IToDoItemRepository
    {
        private readonly ToDoListDbContext dbContext;

        public ToDoItemRepository(ToDoListDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task DeleteItemById(int id)
        {
           dbContext.ToDoItems.Remove( await dbContext.Set<ToDoItem>().FirstOrDefaultAsync(x=>x.ItemId == id));
        }

        public async Task<IReadOnlyList< ToDoItem>> GetAllItemsAsync()
        {
            return await dbContext.ToDoItems.ToListAsync();
        }

        public async Task<ToDoItem> GetItemById(int id)
        {
           return await dbContext.ToDoItems.FirstOrDefaultAsync(x => x.ItemId == id);
        }

        public async Task UpdateItem(ToDoItem item)
        {
              dbContext.Set<ToDoItem>().Update(item);
        }
    }
}
