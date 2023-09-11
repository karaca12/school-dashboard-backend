using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolDasboard.Model;
using SchoolDashboard.DataAccess.Abstract;
using SchoolDashboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private SchoolDbContext schoolDbContext;
        public UserRepository()
        {
            schoolDbContext = new SchoolDbContext();
        }
        public User CreateUser(User user)
        {
            var tempUser= schoolDbContext.Users.SingleOrDefault(u => u.UserSchoolNumber == user.UserSchoolNumber);
            if (tempUser==null) {
                schoolDbContext.Users.Add(user);
                schoolDbContext.SaveChanges();
                return user;
            }
            else
            {
                throw new Exception("This school number is already on use!");
            }
        }

        public User UpdateUserById(int id, User newUser)
        {
            var existingUser = schoolDbContext.Users.Find(id);
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }
            existingUser.UserName = newUser.UserName;
            existingUser.UserNationalIdentity = newUser.UserNationalIdentity;
            existingUser.UserSchoolNumber = newUser.UserSchoolNumber;
            existingUser.UserBirthdate = newUser.UserBirthdate;
            existingUser.UserEmail = newUser.UserEmail;
            existingUser.UserPhone = newUser.UserPhone;
            existingUser.UserRole = newUser.UserRole;
            schoolDbContext.SaveChanges();
                return existingUser;
        }

        public User GetUserById(int id)
        {
            var user = schoolDbContext.Users.Find(id);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            return user;
        }

        public List<User> GetAllUsers()
        {
            return schoolDbContext.Users.ToList();
        }

        public void DeleteUserById(int id)
        {
            var deletedUser = schoolDbContext.Users.Find(id);
            if (deletedUser == null)
            {
                throw new Exception("User not found.");
            }
            schoolDbContext.Users.Remove(deletedUser);
            schoolDbContext.SaveChanges();
        }

        public void EnrollUserToLecture(string userSchoolNum,string lectureName)
        {
            var user = schoolDbContext.Users.SingleOrDefault(u => u.UserSchoolNumber == userSchoolNum);
            var lecture = schoolDbContext.Lectures.SingleOrDefault(l=>l.LectureName==lectureName);
            if (user == null || lecture == null)
            {
                throw new Exception("User or lecture not found.");
            }
            var userLecture = new UserLecture
            {
                UserId = user.Id,
                LectureId = lecture.Id
            };
            user.Lectures.Add(userLecture);
            lecture.Users.Add(userLecture);
            schoolDbContext.UserLectures.Add(userLecture);
            schoolDbContext.SaveChanges();
        }

        public List<Lecture> GetAllLecturesFromSchoolNum(string userSchoolNum)
        {
            var user = schoolDbContext.Users.SingleOrDefault(u => u.UserSchoolNumber == userSchoolNum);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }
            var userLecture = schoolDbContext.UserLectures
        .Where(u => u.UserId == user.Id)
        .Select(u => u.LectureId)
        .ToList();

            var lectures = schoolDbContext.Lectures
                .Where(lecture => userLecture.Contains(lecture.Id))
                .ToList();
            return lectures;
        }

        public List<Lecture> GetEnrollmentLectures(string userSchoolNum)
        {
            var user = schoolDbContext.Users.SingleOrDefault(u => u.UserSchoolNumber == userSchoolNum);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }
            var userLecture = schoolDbContext.UserLectures
        .Where(u => u.UserId == user.Id)
        .Select(u => u.LectureId)
        .ToList();
            var alllectures = schoolDbContext.Lectures.ToList();
            var lectures = schoolDbContext.Lectures
                .Where(lecture => userLecture.Contains(lecture.Id))
                .ToList();
            var lecturesExcept = alllectures.Except(lectures).ToList();
            return lecturesExcept;
        }

        public void DropLectureFrom(string userSchoolNum,string lectureName)
        {
            var user = schoolDbContext.Users.SingleOrDefault(u => u.UserSchoolNumber == userSchoolNum);
            var lecture = schoolDbContext.Lectures.SingleOrDefault(l => l.LectureName == lectureName);
            if (user == null || lecture == null)
            {
                throw new Exception("User or lecture not found.");
            }
            var userLecture = new UserLecture
            {
                UserId = user.Id,
                LectureId = lecture.Id
            };
            user.Lectures.Remove(userLecture);
            lecture.Users.Remove(userLecture);
            schoolDbContext.UserLectures.Remove(userLecture);
            schoolDbContext.SaveChanges();


        }





        public async Task<User> ValidateUser(string schoolNumber, string password)
        {
            var user = await Task.Run(() => schoolDbContext.Users.SingleOrDefault(u => u.UserSchoolNumber == schoolNumber));
            if (user != null)
            {
                
                if (BCrypt.Net.BCrypt.Verify(password, user.UserPassword))
                {
                    return user;
                }
            }
            return null;
        }

        public User SignInCheck(string userSchoolNum, string userPassword)
        {
            var userTask =  Task.Run(() => schoolDbContext.Users.SingleOrDefault(u => u.UserSchoolNumber == userSchoolNum));
            var user = userTask.Result;
            if (user != null)
            {

                if (BCrypt.Net.BCrypt.Verify(userPassword, user.UserPassword))
                {
                    return user;
                }
            }
            return null;
        }

        public async Task<User> GetUserBySchoolNum(string schoolnum)
        {
            var user = await Task.Run(() => schoolDbContext.Users.SingleOrDefault(u => u.UserSchoolNumber == schoolnum));
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public int GetLectureCount(int id)
        {
            var user = schoolDbContext.Users.Find(id);
            var userLecture = schoolDbContext.UserLectures
        .Where(u => u.UserId == user.Id)
        .Select(u => u.LectureId)
        .ToList();
            var lectures = schoolDbContext.Lectures
                .Where(lecture => userLecture.Contains(lecture.Id))
                .ToList();
            return lectures.Count();
        }

        public User ChangePasswordById(int id, PasswordChangeBody passwordData)
        {
            var existingUser = schoolDbContext.Users.Find(id);
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }
            existingUser.UserPassword = passwordData.UserPassword;
            schoolDbContext.SaveChanges();
            return existingUser;
        }
    }
}
