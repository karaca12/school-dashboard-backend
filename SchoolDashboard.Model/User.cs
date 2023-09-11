using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolDasboard.Model
{
    public class User
    {
        public User()
        {
            Lectures = new HashSet<UserLecture>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string UserNationalIdentity { get; set; }
        [Required]
        [StringLength(9,MinimumLength =9)]
        public string UserSchoolNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime UserBirthdate { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string UserEmail { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string UserPhone { get; set; }
        [Required]
        public string UserRole { get; set; }
        public ICollection<UserLecture> Lectures { get; set; }
    }
}
