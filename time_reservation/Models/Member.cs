using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace time_reservation.Models
{
    public class Member
    {

        [Key]
        public int id { get; set; }

      
        [MaxLength(50)]
        public string? FullName { get; set; }  
       
        [MaxLength(100)]
        public string? Email { get; set; }  
      
        [MaxLength(11)]
        public string? Phone { get; set; }  
       
        [MaxLength(30)]
        public string? Password { get; set; } 

        public DateTime? DateCreated { get; set; }

        public ICollection<TTime>? TTime { get; set; } // رزروهای کاربر

    }
}
