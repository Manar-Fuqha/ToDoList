using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using toDoList.Core.IGenaricRepository;
using toDoList.Repository.Data;

namespace toDoList.Repository.GenaricRepository
{
    public class GenaricRepo<T> : IGenaricRepo<T> where T : class
    {
        private readonly ToDoListDbContext dbContext;

        public GenaricRepo(ToDoListDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
        }

        public async Task<int> SaveAsync()
        {
           return await dbContext.SaveChangesAsync();
        }
    }
}
