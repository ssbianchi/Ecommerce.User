using Ecommerce.User.Application.User;
using Ecommerce.User.Application.User.Dto;
using Ecommerce.User.CrossCutting.Exception;
using Google.Protobuf;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace Ecommerce.User.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        #region Web API Methods
        [HttpGet("GetUserById")]

        public async Task<IActionResult> GetUserById(int userId)
        {
            var result = await _userService.GetUser(userId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers([FromQuery] string responseContentType = "application/x-protobuf")
        {
            var result = await _userService.GetAllUsers();
            if (result == null)
                return NotFound();

            //return Ok(result);
            if ("application/x-protobuf".Equals(responseContentType))
            {
                var protobufResult = ConvertToProtobufObject(result);
                return ProtobufFile(protobufResult);
            }
            else if ("application/json".Equals(responseContentType)) // any other is json
            {
                return Ok(result);
            }
            else
            {
                throw new BusinessException($"Unkown responseContentType='{responseContentType}'. Supported types are application/x-protobuf, application/json.");
            }
        }

        [HttpPost("SaveUser")]
        public async Task<IActionResult> SaveUser([FromBody] UserDto userDto)
        {
            var result = await _userService.SaveUser(userDto);

            if (result == null)
                return NotFound();

            return Created($"/{result.Id}", result);
        }
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DelteUser(int userId)
        {
            var result = await _userService.DeleteUser(userId);
            if (!result)
                return NotFound();

            return Ok(result);
        }
        #endregion

        #region Protobuf Helpers
        private FileContentResult ProtobufFile(IMessage message)
        {
            byte[] bytes;
            using (var stream = new MemoryStream())
            {
                message.WriteTo(stream);
                bytes = stream.ToArray();
            }

            return File(bytes, "application/x-protobuf", $"Google.Protobuf.proto");
        }
        private UserListProto ConvertToProtobufObject(IEnumerable<UserDto> list)
        {
            var result = new UserListProto();
            var concurrentBag = new ConcurrentBag<UserProto>();

            Parallel.ForEach(list, item =>
            // foreach (var item in list)
            {
                var user = new UserProto()
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Email = item.Email,
                    Login = item.Login,
                    Password = item.Password
                    //DateStart = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(item.DateStart.ToUniversalTime()),
                };

                concurrentBag.Add(user);
            });

            result.List.AddRange(concurrentBag.OrderBy(a => a.Id));

            return result;
        }
        #endregion
    }
}
