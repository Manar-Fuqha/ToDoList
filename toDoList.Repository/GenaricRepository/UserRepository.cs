using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using toDoList.Core.IGenaricRepository;
using toDoList.Core.GenaricRepository;
using toDoList.Core.Models;
using toDoList.Repository.Data;
using toDoList.Core.Models.DTO.Login;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Bc = BCrypt.Net.BCrypt;
using Microsoft.Extensions.Configuration;
using toDoList.Core.Models.DTO.Register;

namespace toDoList.Repository.GenaricRepository
{
    public class UserRepository : GenaricRepo<User>,   IUserRepository
    {
        private readonly ToDoListDbContext dbContext;
        private readonly string secretKey;

        public UserRepository(ToDoListDbContext dbContext , IConfiguration configuration):base(dbContext)
        {
            this.dbContext = dbContext;
            secretKey = configuration.GetSection("Authentication")["SecretKey"];
        }


        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            var user = AuthenticateUser(loginRequestDTO);

            if (user == null)
            {
                return new LoginResponseDTO
                {
                    User = null,
                    Token = ""
                };
            }

            var token = GenerateJwtToken(user);

            return new LoginResponseDTO
            {
                User = user,
                Token = token
            };
        }

        

        private User AuthenticateUser(LoginRequestDTO loginRequestDTO)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.email == loginRequestDTO.email);

            if (user != null && Bc.Verify(loginRequestDTO.password, user.password))
            {
                return user;
            }

            return null;
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Email, user.email),
            new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



        public async Task<User> RegisterAsync(RegisterRequestDTO registerRequestDTO)
        {
            var HassedPassword = Bc.HashPassword(registerRequestDTO.password);
            User user = new User()
            {
                UserName=registerRequestDTO.UserName,
                email=registerRequestDTO.email,
                password= HassedPassword,
                Role=registerRequestDTO.Role.ToLower()
            };
            await dbContext.Users.AddAsync(user);
            return user;
        }
    }
}
