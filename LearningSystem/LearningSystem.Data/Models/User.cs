namespace LearningSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    using static DataConstrants;

    public class User : IdentityUser
    {
        [Required]
        [MaxLength(UserNameMaxLength)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        public List<StudentCourse> Courses { get; set; } = new List<StudentCourse>();

        public List<Course> Trainings { get; set; } = new List<Course>();

        public List<Article> Articles { get; set; } = new List<Article>();
    }
}
