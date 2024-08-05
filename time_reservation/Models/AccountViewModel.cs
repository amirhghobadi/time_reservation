using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace time_reservation.Models
{
   
        public class RegisterViewModel
        {


        [MaxLength(300)]
      
        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
     
        public string? FullName { get; set; }


        [MaxLength(300)]
            [EmailAddress]
            [Display(Name = "ایمیل")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
            [Remote("VerifyEmail", "Account")]
            public string? Email { get; set; }


       
     
        [Display(Name = "تلفن")]
      
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
    
        public string? Phone { get; set; }


        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
            [MaxLength(50)]
            [DataType(DataType.Password)]
            [Display(Name = "کلمه عبور")]
            [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,20}$", ErrorMessage = "کلمه عبور باید شامل حرف و عدد باشد")]
            public string? Password { get; set; }

            [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
            [MaxLength(50)]
            [DataType(DataType.Password)]
            [Compare("Password")]
            [Display(Name = "تکرار کلمه عبور")]
            public string? RePassword { get; set; }
        }

        public class LoginViewModel
        {
            [MaxLength(300)]
            [EmailAddress]
            [Display(Name = "ایمیل")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
            public string? Email { get; set; }

            [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
            [MaxLength(50)]
            [DataType(DataType.Password)]
            [Display(Name = "کلمه عبور")]
            public string? Password { get; set; }

            [Display(Name = "مرا به خاطر بسپار ")]
            public bool RememberMe { get; set; }
        }






    public class RegisterAdminViewModel
    {


        [MaxLength(300)]

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]

        public string? FullName { get; set; }



        [Display(Name ="کد ملی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
       
        public string? NationalCode { get; set; }


        [MaxLength(300)]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [Remote("VerifyEmail", "Account")]
        public string? Email { get; set; }




        [Display(Name = "تلفن")]

        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]

        public string? Phone { get; set; }


        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,20}$", ErrorMessage = "کلمه عبور باید شامل حرف و عدد باشد")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "تکرار کلمه عبور")]
        public string? RePassword { get; set; }
    }

}
