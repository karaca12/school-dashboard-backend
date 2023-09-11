﻿using SchoolDasboard.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolDashboard.Service.Abstract
{
    public interface ILectureService
    {
        Lecture CreateLecture(Lecture lecture);
        Lecture UpdateLectureById(int id, Lecture newLecture);
        Lecture GetLectureById(int id);
        List<Lecture> GetAllLectures();
        void DeleteLectureById(int id);
        List<User> GetAllUsersFromId(int id);
        void DeleteLectureByName(string lectureName);
        List<User> GetAllStudentsForLecture(int id);
    }
}
