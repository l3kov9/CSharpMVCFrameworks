namespace LearningSystem.Web.Models.Home
{
    using System.ComponentModel.DataAnnotations;

    public class SearchFormModel
    {
        public string Search { get; set; }

        [Display(Name = "Search in Users")]
        public bool SearchInUsers { get; set; } = true;

        [Display(Name = "Search in Courses")]
        public bool SearchInCourses { get; set; } = true;
    }
}
