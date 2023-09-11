using SchoolDasboard.Model;
using SchoolDashboard.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolDashboard.DataAccess.Repository
{
    public class LectureRepository : ILectureRepository
    {
        private SchoolDbContext schoolDbContext;
        public LectureRepository()
        {
            schoolDbContext = new SchoolDbContext();
        }
        public Lecture CreateLecture(Lecture lecture)
        {

            schoolDbContext.Lectures.Add(lecture);
            schoolDbContext.SaveChanges();
            return lecture;
        }

        public Lecture UpdateLectureById(int id, Lecture newLecture)
        {
            var existingLecture = schoolDbContext.Lectures.Find(id);
            if (existingLecture == null)
            {
                throw new Exception("Lecture not found.");
            }
            existingLecture.LectureName = newLecture.LectureName;
            existingLecture.LectureDescription = newLecture.LectureDescription;
            schoolDbContext.SaveChanges();
            return existingLecture;
        }

        public Lecture GetLectureById(int id)
        {
            var lecture = schoolDbContext.Lectures.Find(id);
            if (lecture == null) { throw new Exception("Lecture not found."); }
            return lecture;
        }

        public List<Lecture> GetAllLectures()
        {
            return schoolDbContext.Lectures.ToList();
        }

        public void DeleteLectureById(int id)
        {
            var deletedLecture = schoolDbContext.Lectures.Find(id);
            if (deletedLecture == null) { throw new Exception("Lecture not found."); }
            schoolDbContext.Lectures.Remove(deletedLecture);
            schoolDbContext.SaveChanges();
        }

        public List<User> GetAllUsersFromId(int lectureId)
        {
            var lecture = schoolDbContext.Lectures.Find(lectureId);
            if (lecture == null)
            {
                throw new InvalidOperationException("User not found.");
            }
            var userLecture = schoolDbContext.UserLectures
        .Where(u => u.LectureId == lectureId)
        .Select(u => u.UserId)
        .ToList();

            var users = schoolDbContext.Users
                .Where(user => userLecture.Contains(user.Id))
                .ToList();
            return users;
        }

        public void DeleteLectureByName(string lectureName)
        {
            var lecture = schoolDbContext.Lectures.SingleOrDefault(l => l.LectureName == lectureName);
            schoolDbContext.Lectures.Remove(lecture);
            schoolDbContext.SaveChanges();
        }

        public List<User> GetAllStudentsForLecture(int id)
        {
            var lecture = schoolDbContext.Lectures.Find(id);
            if (lecture == null)
            {
                throw new InvalidOperationException("User not found.");
            }
            var userLecture = schoolDbContext.UserLectures
        .Where(u => u.LectureId == id)
        .Select(u => u.UserId)
        .ToList();

            var users = schoolDbContext.Users
                .Where(user => userLecture.Contains(user.Id))
                .ToList();
            return users.Where(u=>u.UserRole=="Student").ToList();
        }
    }
}
