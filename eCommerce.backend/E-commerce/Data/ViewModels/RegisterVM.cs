using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [StringLength(30, MinimumLength =1)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        [Required]
        public int BranchId { get; set; }
        [Required]
        public string RoleId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        public RegisterVM()
        {
            Password = NewPassword(8);
        }

        private string NewPassword(int length)
        {
            string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lowercase = uppercase.ToLower();
            string special = "!@#$%^&*()_+=-";
            string numbers = "0123456789";
            return Random(numbers, 1) + Random(uppercase, 1) + Random(special, 1)
                + Random(lowercase, length - 3);
        }

        private string Random(string text, int length)
        {
            Random random = new Random();
            string result = "";
            for (int i = 0; i < length; i++)
            {
                result += text[random.Next(text.Length)];
            }
            return result;
        }
    }
}
