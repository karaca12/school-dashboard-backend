using Microsoft.AspNetCore.Mvc;
using SchoolDasboard.Model;
using SchoolDashboard.DataAccess.Abstract;
using SchoolDashboard.DataAccess.Repository;
using SchoolDashboard.Model;
using SchoolDashboard.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.Service.Concrete
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;
        public UserService()
        {
            userRepository = new UserRepository();
        }
        public User CreateUser(User user)
        {
            return userRepository.CreateUser(user);
        }

        public User UpdateUserById(int id, User newUser)
        {
            return userRepository.UpdateUserById(id, newUser);
        }

        public User GetUserById(int id)
        {
            return userRepository.GetUserById(id);
        }

        public List<User> GetAllUsers()
        {
            return userRepository.GetAllUsers();
        }

        public void DeleteUserById(int id)
        {
            userRepository.DeleteUserById(id);
        }
        public void EnrollUserToLecture(string userSchoolNum, string lectureName)
        {
            userRepository.EnrollUserToLecture(userSchoolNum, lectureName);
        }

        public List<Lecture> GetAllLecturesFromSchoolNum(string userSchoolNum)
        {
            return userRepository.GetAllLecturesFromSchoolNum(userSchoolNum);
        }

        public Task<User> ValidateUser(string schoolNumber, string password)
        {
            return userRepository.ValidateUser(schoolNumber, password);
        }

        public User SignInCheck(string userSchoolNum, string userPassword)
        {
            return userRepository.SignInCheck(userSchoolNum, userPassword);
        }

        public Task<User> GetUserBySchoolNum(string schoolnum)
        {
            return userRepository.GetUserBySchoolNum(schoolnum);
        }
        public List<Lecture> GetEnrollmentLectures(string userSchoolNum)
        {
            return userRepository.GetEnrollmentLectures(userSchoolNum);
        }

        public void DropLectureFrom(string userSchoolNum, string lectureName)
        {
            userRepository.DropLectureFrom(userSchoolNum,lectureName);
        }

        public int GetLectureCount(int id)
        {
            return userRepository.GetLectureCount(id);
        }

        public User ChangePasswordById(int id, PasswordChangeBody passwordData)
        {
            return userRepository.ChangePasswordById(id, passwordData);
        }
    }
}
