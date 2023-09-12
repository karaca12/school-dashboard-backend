using SchoolDasboard.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolDashboard.DataAccess.Abstract
{
    public interface ILectureRepository
    {
        Lecture CreateLecture(Lecture lecture);
        Lecture UpdateLectureById(int id, Lecture newLecture);
        Lecture GetLectureById(int id);
        List<Lecture> GetAllLectures();
        void DeleteLectureById(int id);
        List<User> GetAllUsersFromId(int id);
        void DeleteLectureByName(string lectureName);
        List<User> GetAllStudentsForLecture(int id);
        void UpdateLectureByName(string lectureName,Lecture newLecture);
    }
}
