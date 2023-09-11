using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDasboard.Model;
using SchoolDashboard.Handler.Concrete;
using SchoolDashboard.Model;
using SchoolDashboard.Service.Abstract;

namespace SchoolDashboard.API.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly CustomBasicAuthenticationHandler customAuthenticationHandler;
        public UserController(IUserService _userService, CustomBasicAuthenticationHandler _customAuthenticationHandler)
        {
            userService = _userService;
            customAuthenticationHandler = _customAuthenticationHandler;
        }
        //[Authorize(Policy = "LecturerOrStudent")]
        [HttpGet("getAll")]
        public List<User> GetAllUsers()
        {
            return userService.GetAllUsers();
        }
        //[Authorize(Policy = "LecturerOrStudent")]
        [HttpGet("getById/{id}")]
        public User GetUserById(int id)
        {
            return userService.GetUserById(id);
        }
        
        [HttpPost("create")]
        public IActionResult PostUser([FromBody]User user)
        {
            user.UserPassword = BCrypt.Net.BCrypt.HashPassword(user.UserPassword);
            var newUser=userService.CreateUser(user);
            return Ok(newUser);
        }
        //[Authorize(Policy = "LecturerOrStudent")]
        [HttpDelete("deleteById/{id}")]
        public void DeleteUser(int id)
        {
            userService.DeleteUserById(id);
        }
        //[Authorize(Policy = "LecturerOrStudent")]
        [HttpPut("updateById/{id}")]
        public IActionResult UpdateUserById(int id, [FromBody]User newUser)
        {
            newUser.UserPassword = BCrypt.Net.BCrypt.HashPassword(newUser.UserPassword);
            
            var user= userService.UpdateUserById(id, newUser); 
            return Ok(user);
        }
        //[Authorize(Policy = "LecturerOrStudent")]
        [HttpPost("enroll/{userSchoolNum}/to/{lectureName}")]
        public IActionResult EnrollToLecture(string userSchoolNum, string lectureName)
        {
            userService.EnrollUserToLecture(userSchoolNum, lectureName);
            return Ok("Enrolled successfully.");
        }
        //[Authorize(Policy = "LecturerOrStudent")]
        [HttpGet("getAllLecturesFrom/{userSchoolNum}")]
        public List<Lecture> GetAllLecturesFromSchoolNum(string userSchoolNum)
        {
            return userService.GetAllLecturesFromSchoolNum(userSchoolNum);
        }
        [HttpGet("getEnrollmentLectures/{userSchoolNum}")]
        public List<Lecture> GetEnrollmentLectures(string userSchoolNum)
        {
            return userService.GetEnrollmentLectures(userSchoolNum);
        }
        [HttpDelete("dropLecture/{lectureName}/from/{userSchoolNum}")]
        public IActionResult DropLectureFrom(string userSchoolNum,string lectureName)
        {
             userService.DropLectureFrom(userSchoolNum,lectureName);
            return Ok("Lecture droped.");
        }


        [HttpPost("signInCheck")]
        public IActionResult SignInCheck([FromBody]LoginBody loginCredentials)
        {
            var user= userService.SignInCheck(loginCredentials.UserSchoolNumber, loginCredentials.UserPassword);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest("School number or password wrong.");
            }
        }
        [HttpGet("getLectureCountOf/{id}")]
        public IActionResult GetLectureCount(int id)
        {
            var count = userService.GetLectureCount(id);
            return Ok(count);
        }
        [HttpPut("changepasswordbyid/{id}")]
        public IActionResult ChangePasswordById(int id,[FromBody]PasswordChangeBody passwordData)
        {
            passwordData.UserPassword = BCrypt.Net.BCrypt.HashPassword(passwordData.UserPassword);
            var user = userService.ChangePasswordById(id, passwordData);
            return Ok(user);
        }
    }
}