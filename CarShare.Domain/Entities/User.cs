using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Web.Mvc;

namespace CarShare.Domain.Entities
{
    public class User
    {

        // pk
        [HiddenInput(DisplayValue = false)]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        [StringLength(50, ErrorMessage = "Name must be under 50 characters in length")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        [StringLength(50, ErrorMessage = "50 characters Email should suffice")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Not a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^(\d)*$", ErrorMessage = "Please enter only numbers")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter your CNIC")]
        [StringLength(15, ErrorMessage = "Maximum Length is 15 characters")]
        [RegularExpression(@"^(\d)*$", ErrorMessage = "Please enter only numbers")]
        public string NIC { get; set; }

        [Required(ErrorMessage = "Please enter your Drivers License")]
        [StringLength(15, ErrorMessage = "Maximum Length is 15 characters")]
        public string DriversLicense { get; set; }

        [DataType(DataType.Password)]
        [NotMapped]
        [Required(ErrorMessage = "Please re-enter your password")]
        public string PasswordAgain { get; set; }

        [StringLength(200, ErrorMessage = "Maximum Length is 200 characters")]
        [DataType(DataType.MultilineText)]
        public string PersonalDesc { get; set; }

        public string Image { get; set; }

        public string Status { get; set; }

        public User(int userID, string name, string email, string pwd, string nic, string driverLicense, string img, string desc, string addr, string status, string contactNum)
        {
            UserID = userID;
            Name = name;
            Password = pwd;
            NIC = nic;
            DriversLicense = driverLicense;
            Image = img;
            PersonalDesc = desc;
            Address = addr;
            Status = status;
            ContactNumber = contactNum;
        }

        public User() { }

    }
}
