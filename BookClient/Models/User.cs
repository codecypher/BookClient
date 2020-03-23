using System;
using System.Collections.Generic;
using System.Text;

namespace BookClient.Models
{
    // Fluent Validation With MVVM In Xamarin Forms Application
    // https://www.c-sharpcorner.com/article/fluent-validation-with-mvvm-in-xamarin-forms-application/
    public class User
    {
        //[Required(ErrorMessage = "{0} is required")]
        //[Display(Name = "User name")]
        public string UserName { get; set; }

        //[Required]
        //[StringLength(100, MinimumLength = 6, ErrorMessage = "The {0} must be at least {2} characters long.")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        //[StringLength(60, MinimumLength = 3)]
        //[MaxLength(24), MinLength(5)]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        //[EmailAddress]
        //[StringLength(50, ErrorMessage = "{0} cannot be more than {1} characters.")]
        public string Email { get; set; }

        //[Phone]
        //[StringLength(20, ErrorMessage = "{0} cannot be more than {1} characters.")]
        public string Phone { get; set; }

        //[Display(Name = "Last Login")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastLogin { get; set; }

        //[NotMapped]
        //public int FatherName { get; set; }

        // A class can only have one timestamp and can be used for concurrency checking.
        //[Timestamp]
        //public byte[] RowVersion { get; set; }
    }
}
