using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using toDoList.Core.IGenaricRepository;

namespace toDoList.Core.GenaricRepository
{
    public interface IUnitOfWork
    {
        public IToDoItemRepository ToDoItemRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
    }
}
