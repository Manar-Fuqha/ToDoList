using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using toDoList.Core.GenaricRepository;
using toDoList.Core.IGenaricRepository;
using toDoList.Repository.Data;
using toDoList.Repository.GenaricRepository;

namespace toDoList.Repository.IGenaricRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ToDoListDbContext dbContext;

        public UnitOfWork(ToDoListDbContext dbContext , IConfiguration configuration)
        {
            ToDoItemRepository = new ToDoItemRepository(dbContext);
            UserRepository = new UserRepository(dbContext , configuration);
            this.dbContext = dbContext;
        }

        public IToDoItemRepository ToDoItemRepository { get; set; }
        public IUserRepository UserRepository { get; set; }

        
    }
}
