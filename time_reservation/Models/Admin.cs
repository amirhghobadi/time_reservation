using System.ComponentModel.DataAnnotations;

namespace time_reservation.Models
{
    public class Admin
    {

        public int id { get; set; }

        public string? FullName { get; set; } 
        [MaxLength(10)]
        public string? NationalCode { get; set; } 

        public string? Email { get; set; }

        [MaxLength(11)]
        public string? Phone { get; set; } 


        public string? Password { get; set; }  


        public bool? State { get; set; }  

        public DateTime? DateCreated { get; set; }


        // ارتباط با سالن‌های ورزشی که این ادمین تعریف کرده است
        public ICollection<Business>? Businesses { get; set; } // سالن‌های ایجاد شده توسط ادمین

        public ICollection<TTime>? TTime { get; set; } // رزروهایی که شامل این ادمین هستند


    }
}
