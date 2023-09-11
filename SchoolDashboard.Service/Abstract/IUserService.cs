using Microsoft.AspNetCore.Mvc;
using SchoolDasboard.Model;
using SchoolDashboard.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.Service.Abstract
{
    public interface IUserService
    {
        User CreateUser(User user);
        User UpdateUserById(int id, User user);
        User GetUserById(int id);
        List<User> GetAllUsers();
        void DeleteUserById(int id);
        void EnrollUserToLecture(string userSchoolNum, string lectureName);
        List<Lecture> GetAllLecturesFromSchoolNum(string userSchoolNum);
        Task<User> ValidateUser(string schoolNumber, string password);
        User SignInCheck(string userSchoolNum, string userPassword);
        Task<User> GetUserBySchoolNum(string schoolnum);
        List<Lecture> GetEnrollmentLectures(string userSchoolNum);
        void DropLectureFrom(string userSchoolNum, string lectureName);
        int GetLectureCount(int id);
        User ChangePasswordById(int id, PasswordChangeBody passwordData);
    }
}
