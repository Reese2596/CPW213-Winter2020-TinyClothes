using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    /// <summary>
    /// A single user account
    /// </summary>
    public class Account
    {
        [Key]
        public int AccountID { get; set; }

        [Required]
        [StringLength(60)]
        /// <summary>
        /// First and last name
        /// </summary>
        public string FullName { get; set; }

        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        [StringLength(150)]
        [DataType(DataType.Password)]   //input => type password
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public string Address { get; set; }
    }

    public class RegisterViewModel
    {
        [Required()]
        [Display(Name = "Full Name")]
        [StringLength(60)]
        public string FullName { get; set; }

        [Required()]
        [StringLength(20)]
        public string UserName { get; set; }

        [Required]
        [StringLength(150)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        public string UsernameOrEmail { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
