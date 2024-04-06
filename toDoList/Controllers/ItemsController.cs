using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using toDoList.Core.GenaricRepository;
using toDoList.Core.Models;
using toDoList.Core.Models.DTO.Items;

namespace toDoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        private readonly ApiResponse _response;
        public ItemsController(IUnitOfWork _unitOfWork , IMapper mapper)
        {
            this._unitOfWork = _unitOfWork;
            this.mapper = mapper;
            _response = new ApiResponse();
        }


        [HttpGet("GetAllItems")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ApiResponse>> GetAllItemsForAUser()
        {
            
            try
            {
                var GetItems = await _unitOfWork.ToDoItemRepository.GetAllItemsAsync();
                if (GetItems.Count() == 0)
                {
                    _response.Status = HttpStatusCode.NotFound;
                    _response.Messages.Add("You don't write any item yet.");
                    return NotFound(_response);
                }
                var itemsMapper = mapper.Map<IReadOnlyList<ToDoItem>, IReadOnlyList<ItemForRetriveDTO>>(GetItems);
                _response.Status = HttpStatusCode.OK;
                _response.result = itemsMapper;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.Status = HttpStatusCode.BadRequest;
                return NotFound(_response);
            }
        }

        [HttpGet("GetItemById{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ApiResponse>> GetAnItemById(int id)
        {
            if(id<=0)
            {
                _response.Status=HttpStatusCode.BadRequest;
                _response.Messages.Add("The Id is less or equal 0");
                return BadRequest(_response);
            }
            ToDoItem GetItem = await _unitOfWork.ToDoItemRepository.GetItemById(id);
            if(GetItem == null)
            {
                _response.Status = HttpStatusCode.NotFound;
                _response.Messages.Add($"The Item with id = {id} is not found");
                return NotFound(_response);
            }
            var itemMapper = mapper.Map<ToDoItem, ItemForRetriveDTO>(GetItem);
            _response.Status = HttpStatusCode.OK;
            _response.result = itemMapper;
            return Ok(_response);
        }


        [HttpPost("CreateNewItem")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ApiResponse>> AddNewToDoItem(string email ,[FromForm] ItemForRetriveDTO item)
        {
            if(!ModelState.IsValid)
            {
                _response.Status=HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var itemMapper = mapper.Map<ItemForRetriveDTO, ToDoItem>(item);
            if(itemMapper == null)
            {
                _response.Status=HttpStatusCode.BadRequest;
                _response.Messages.Add("All inputs is null ");
                return BadRequest(_response);
            }
            itemMapper.email= email;
            await _unitOfWork.ToDoItemRepository.CreateAsync(itemMapper);
           var success= await _unitOfWork.ToDoItemRepository.SaveAsync();
            if (success > 0)
            {
                _response.Status = HttpStatusCode.Created;
                _response.result = item;
                return Ok(_response);
            }
            _response.Status=HttpStatusCode.BadRequest;
            return BadRequest( _response);
        }

        [HttpPut("EditAnItem{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ApiResponse>> EditAnToDoItem(int id , [FromForm] ItemForEditDTO item)
        {
            if (!ModelState.IsValid)
            {
                _response.Status = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            if (id <= 0)
            {
                _response.Status=HttpStatusCode.BadRequest;
                _response.Messages.Add("The id is less or equal 0 ");
                return BadRequest(_response);
            }
            var getitem = await _unitOfWork.ToDoItemRepository.GetItemById(id);
            if (getitem == null)
            {
                _response.Status=HttpStatusCode.NotFound;
                _response.Messages.Add($"The To DO Item with id = {id} is not found ");
                return NotFound( _response);
            }
            mapper.Map(item, getitem);
            await _unitOfWork.ToDoItemRepository.UpdateItem(getitem);
            var success = await _unitOfWork.ToDoItemRepository.SaveAsync();
            if (success > 0)
            {
                _response.Status = HttpStatusCode.OK;
                _response.Messages.Add($"The item with id = {id} is edited");
                _response.result = getitem;
                return Ok( _response );
            }
            _response.Status=HttpStatusCode.BadRequest;
            return BadRequest(_response );
        }


        [HttpDelete("RemoveItem{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ApiResponse>> DeleteToDoListItem( int id)
        {
            if (id <= 0)
            {
                _response.Status=HttpStatusCode.BadRequest;
                _response.Messages.Add("The id is less or equal 0");
                return BadRequest(_response);
            }
            var getItem = await _unitOfWork.ToDoItemRepository.GetItemById(id);
            if (getItem == null)
            {
                _response.Status=HttpStatusCode.NotFound;
                _response.Messages.Add($"The To Do List Item with id = {id} is not found");
                return NotFound(_response);
            }
            await _unitOfWork.ToDoItemRepository.DeleteItemById(id);
            if(await _unitOfWork.ToDoItemRepository.SaveAsync() > 0)
            {
                _response.Status=HttpStatusCode.OK;
                _response.Messages.Add($"The item with id = {id} is deleted");
                return Ok(_response);
            }
            _response.Status=HttpStatusCode.BadRequest;
            _response.Messages.Add("An error occurred and the item was not deleted");
            return BadRequest(_response);
        }
    }
}
