using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolDasboard.Model
{
    public class Lecture
    {
        public Lecture()
        {
            Users = new HashSet<UserLecture>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string LectureName { get; set; }
        [Required]
        public string LectureDescription { get; set; }
        public string LectureLecturerName { get; set; }
        public ICollection<UserLecture> Users { get; set; }
    }
}