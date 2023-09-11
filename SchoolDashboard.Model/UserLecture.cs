using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SchoolDasboard.Model
{
    public class UserLecture
    {
        public int UserId { get; set; }
        public int LectureId { get; set; }
        public User User { get; set; }
        public Lecture Lecture { get; set; }
    }
}
