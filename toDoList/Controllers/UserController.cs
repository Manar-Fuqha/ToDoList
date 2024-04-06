using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using toDoList.Core.GenaricRepository;
using toDoList.Core.Models;
using toDoList.Core.Models.DTO.Login;
using toDoList.Core.Models.DTO.Register;
using toDoList.Repository.IGenaricRepository;

namespace toDoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        private readonly ApiResponse _response;

        public UserController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
            _response = new ApiResponse();
        }


        [HttpPost("Register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ApiResponse>> Register(RegisterRequestDTO registerRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                _response.Status = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var userMapper = mapper.Map<RegisterRequestDTO, User>(registerRequestDTO);
            if(userMapper == null)
            {
                _response.Status = HttpStatusCode.BadRequest;
                _response.Messages.Add("All inputs is null ");
                return BadRequest(_response);
            }
            await _unitOfWork.UserRepository.RegisterAsync(registerRequestDTO);
            if(await _unitOfWork.UserRepository.SaveAsync() > 0)
            {
                _response.Status = HttpStatusCode.Created;
                _response.result = registerRequestDTO;
                return Ok(_response);
            }
            _response.Status = HttpStatusCode.BadRequest;
            _response.Messages.Add("An error occurred when adding the new user");
            return BadRequest(_response);

        }


        [HttpPost("Login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [AllowAnonymous]

        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {

            var loginResponse = await _unitOfWork.UserRepository.LoginAsync(loginRequestDTO);

            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.Status = HttpStatusCode.BadRequest;
                _response.Messages.Add("Email or password is incorrect");
                return BadRequest(_response);
            }

            _response.Status = HttpStatusCode.OK;
            _response.result = loginResponse;
            return Ok(_response);
        }
    }
}
