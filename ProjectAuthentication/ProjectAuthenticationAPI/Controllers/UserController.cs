using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectAuthenticationAPI.Models;
using ProjectAuthenticationAPI.Services;
using ProjectAuthenticationAPI.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace ProjectAuthenticationAPI.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest("Invalid User Data");
            }
            if(await _userService.GetUserByEmailOrUsername(newUser.Email, newUser.UserName) != null)
            {
                return Conflict("User Already Exists");
            }
            newUser.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(newUser.Password, 12);
            newUser.CreatedDate = DateTime.UtcNow;
            try
            {
                await _userService.SignUp(newUser);
            } catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex });
            }

            return CreatedAtAction(nameof(SignUp), new { Message = "User Created", User = newUser });
        }

        [HttpGet("sign-in")]
        public async Task<IActionResult> SignIn(string emailOrUsername, string Password)
        {
            Console.WriteLine("User Signed In");
            String token;
            if(string.IsNullOrEmpty(emailOrUsername) || string.IsNullOrEmpty(Password))
            {
                return BadRequest("Required Field Missing");
            }
            try
            {
                token = await _userService.SignIn(emailOrUsername, Password);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
            if(token == null)
            {
                return Unauthorized("Invalid Credentials");
            }
            return Ok(new { Message = "Success", Token = token });
        }

        [HttpGet("get-user")]
        public async Task<IActionResult> GetUserDetails([FromHeader(Name = "Authorization")] string authorizationHeader)
        {
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return BadRequest("Authorization header is missing");
            }
            string token = authorizationHeader.Replace("Bearer ", "");
            User user;
            try
            {
                JwtSecurityToken decodedToken = JwtDecoder.DecodeToken(token);
                int id = Convert.ToInt32(JwtDecoder.GetClaimValue(decodedToken, "nameid"));
                if (id == null)
                {
                    return BadRequest("Id missing");
                }
                user = await _userService.GetUserDetails(id);
            } catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
            return Ok(new { Message = "Successfully fetched user data", User = user });
        }

        [HttpPatch("process-payment")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest request)
        {
            try
            {
                var (Success, Message) = await _userService.ProcessPayment(request.UserId, request.Price);
                if (Success)
                {
                    return Ok(Message);
                }
                else
                {
                    return BadRequest(Message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the payment");
            }
        }
    }
}
