using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace time_reservation.Models
{
    public class AdminProfileViewModel
    {


        public Admin Admin { get; set; }

        public TTime TTime { get; set; }

        public Business Business { get; set; }

        public ChangePasswordViewModel ChangePasswordViewModel { get; set; }



       

    }

    public class AddTTimeViewModel
    {



        public Admin Admin { get; set; }

        public TTime TTime { get; set; }
 
        public int id { get; set; }



        [Display(Name = "سالن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public int SelectBusiness { get; set; }




        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]

        public string? FullName { get; set; }  

     



        [Display(Name = "ساعت رزرو")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public TimeSpan ReservationHour { get; set; }  

      





        [Display(Name = "تاریخ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public DateTime ReservationDate { get; set; }  






        [Display(Name = "پرداخت انجام شده است.")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public bool IsPaymentCompleted { get; set; }  









    }


    public class AddBusinessViewModel
    {
        public Admin Admin { get; set; }

        public int id { get; set; }


        [Display(Name = "اسم مجموعه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public string? Name { get; set; } // اسم مجموعه





        public IFormFile? Picture { get; set; }


     


        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public string? Description { get; set; } // توضیحات











        [Display(Name = "امکانات")]
      
        public string? Possibilities { get; set; } // امکانات



        [Display(Name = "مخصوص")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public string? Gender { get; set; } // آقایان - بانوان - آقایان و بانوان


        [Display(Name = "استان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public string? City { get; set; } // استان


        [Display(Name = "شهر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public string? SubCity { get; set; } // شهر


        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public string? Address { get; set; } // آدرس


        [Display(Name = "زمان هر سانس (برحسب دقیقه)")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public int TimeUnit { get; set; } // زمان هر سانس


        [Display(Name = "زمان شروع سانس ها")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public TimeSpan StartTime { get; set; } // زمان شروع سانس‌ها


        [Display(Name = "زمان پایان سانس ها")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public TimeSpan EndTime { get; set; } // زمان پایان سانس‌ها


        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public decimal? Price { get; set; } // قیمت


        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public bool State { get; set; } // وضعیت

        






    }



    public class EditBusinessViewModel
    {
        public int id { get; set; }
        public Admin Admin { get; set; }

        [Display(Name = "اسم مجموعه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public string? Name { get; set; }


        public string PictureFileName { get; set; }

        public IFormFile? Picture { get; set; }



        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public string? Description { get; set; }

        [Display(Name = "امکانات")]
        public string? Possibilities { get; set; }

        [Display(Name = "مخصوص")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public string? Gender { get; set; }

        [Display(Name = "استان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public string? City { get; set; }

        [Display(Name = "شهر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public string? SubCity { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public string? Address { get; set; }

        [Display(Name = "زمان هر سانس (برحسب دقیقه)")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public int TimeUnit { get; set; }

        [Display(Name = "زمان شروع سانس ها")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public TimeSpan StartTime { get; set; }

        [Display(Name = "زمان پایان سانس ها")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public TimeSpan EndTime { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public decimal? Price { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public bool State { get; set; }


    }



}
