using SchoolDasboard.Model;
using SchoolDashboard.DataAccess.Abstract;
using SchoolDashboard.DataAccess.Repository;
using SchoolDashboard.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolDashboard.Service.Concrete
{
    public class LectureService : ILectureService
    {
        private ILectureRepository lectureRepository;
        public LectureService()
        {
            lectureRepository = new LectureRepository();
        }
        public Lecture CreateLecture(Lecture lecture)
        {
            return lectureRepository.CreateLecture(lecture);
        }

        public Lecture UpdateLectureById(int id, Lecture newLecture)
        {
            return lectureRepository.UpdateLectureById(id, newLecture);
        }

        public Lecture GetLectureById(int id)
        {
            return lectureRepository.GetLectureById(id);
        }

        public List<Lecture> GetAllLectures()
        {
            return lectureRepository.GetAllLectures();
        }

        public void DeleteLectureById(int id)
        {
            lectureRepository.DeleteLectureById(id);
        }

        public List<User> GetAllUsersFromId(int id)
        {
            return lectureRepository.GetAllUsersFromId(id);
        }

        public void DeleteLectureByName(string lectureName)
        {
            lectureRepository.DeleteLectureByName(lectureName);
        }

        public List<User> GetAllStudentsForLecture(int id)
        {
            return lectureRepository.GetAllStudentsForLecture(id);
        }

        public void UpdateLectureByName(string lectureName,Lecture newLecture)
        {
            lectureRepository.UpdateLectureByName(lectureName,newLecture);
        }
    }
}
