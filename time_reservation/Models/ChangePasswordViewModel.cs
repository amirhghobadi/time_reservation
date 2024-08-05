using System.ComponentModel.DataAnnotations;

namespace time_reservation.Models
{
    public class ChangePasswordViewModel
    {

        [Required]
        public int id { get; set; }

        [Required(ErrorMessage = "کلمه عبور فعلی ضروری است.")]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور قبلی")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "کلمه عبور جدید ضروری است.")]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور جدید")]
        [StringLength(100, ErrorMessage = "کلمه عبور باید حداقل {2} کاراکتر باشد.", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "تکرار کلمه عبور جدید ضروری است.")]
        [DataType(DataType.Password)]
        [Display(Name = "تکرار کلمه عبور جدید")]
        [Compare("NewPassword", ErrorMessage = "کلمه عبور جدید و تکرار آن مطابقت ندارند.")]
        public string ConfirmNewPassword { get; set; }
    }

}
