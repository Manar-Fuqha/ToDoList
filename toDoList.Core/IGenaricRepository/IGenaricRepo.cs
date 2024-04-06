using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toDoList.Core.IGenaricRepository
{
    public interface IGenaricRepo<T> where T : class
    {
        public Task<int> SaveAsync();
        public Task CreateAsync(T entity);
    }
}
