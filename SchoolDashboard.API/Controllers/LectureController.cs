using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDasboard.Model;
using SchoolDashboard.Service.Abstract;

namespace SchoolDashboard.API.Controllers
{
    [Route("lectures")]
    [ApiController]
    public class LectureController : Controller
    {
        private readonly ILectureService lectureService;
        public LectureController(ILectureService _lectureService)
        {
            lectureService = _lectureService;
        }
        //[Authorize(Policy = "LecturerOrStudent")]
        [HttpGet("getAll")]
        public List<Lecture> GetAllLectures()
        {
            return lectureService.GetAllLectures();
        }
        //[Authorize(Policy = "LecturerOrStudent")]
        [HttpGet("getById/{id}")]
        public Lecture GetLectureById(int id)
        {
            return lectureService.GetLectureById(id);
        }
        //[Authorize(Roles = "Lecturer")]
        [HttpPost("create")]
        public Lecture PostLecture([FromBody]Lecture lecture)
        {
            return lectureService.CreateLecture(lecture);
        }
        //[Authorize(Roles = "Lecturer")]
        [HttpDelete("deleteById/{id}")]
        public void DeleteLecture(int id)
        {
            lectureService.DeleteLectureById(id);
        }
        [HttpDelete("deleteByName/{lectureName}")]
        public IActionResult DeleteLectureByName(string lectureName)
        {
            lectureService.DeleteLectureByName(lectureName);
            return Ok("deleted");
        }


        //[Authorize(Roles = "Lecturer")]
        [HttpPut("updateById/{id}")]
        public Lecture UpdateLectureById(int id, [FromBody]Lecture newLecture)
        {
            return lectureService.UpdateLectureById(id, newLecture);
        }
        //[Authorize(Roles = "Lecturer")]
        [HttpGet("getAllUsersFrom/{id}")]
        public List<User> GetAllUsersFromId(int id)
        {
            return lectureService.GetAllUsersFromId(id);
        }

        [HttpGet("getAllStudentsForLecture/{id}")]
        public List<User> GetAllStudentsForLecture(int id)
        {
            return lectureService.GetAllStudentsForLecture(id);
        }

    }
}