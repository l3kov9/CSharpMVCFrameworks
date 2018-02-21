namespace LearningSystem.Web.Models.ManageViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstrants;

    public class IndexViewModel
    {
        public string Username { get; set; }

        [Required]
        [MaxLength(UserNameMaxLength)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
