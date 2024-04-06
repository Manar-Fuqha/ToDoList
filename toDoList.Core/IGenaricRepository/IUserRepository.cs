using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using toDoList.Core.Models;
using toDoList.Core.Models.DTO.Login;
using toDoList.Core.Models.DTO.Register;

namespace toDoList.Core.IGenaricRepository
{
    public interface IUserRepository :IGenaricRepo<User>
    {
        public Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequestDTO);
        public Task<User> RegisterAsync(RegisterRequestDTO registerRequestDTO);
    }
}
