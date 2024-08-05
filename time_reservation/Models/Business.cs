using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace time_reservation.Models
{
    public class Business
    {


        [Key]
        public int id { get; set; }

        public string? Name { get; set; }  

        public string? OwnerName { get; set; }  

        public string? Description { get; set; }  

        public string PictureFileName { get; set; }  

        public string? Possibilities { get; set; }  

        public string? Gender { get; set; }  

        public string? City { get; set; }  
        public string? SubCity { get; set; }  

        public string? Address { get; set; }  

        public int TimeUnit { get; set; }  

        public TimeSpan StartTime { get; set; }  

        public TimeSpan EndTime { get; set; } 

        public decimal? Price { get; set; }  

        public bool State { get; set; }  

        public DateTime? DateCreated { get; set; }

        public int AdminIdB { get; set; }  

        [ForeignKey("AdminIdB")]
        public Admin? Admin { get; set; }  

        public ICollection<TTime>? TTime { get; set; } // رزروهای سالن


    }
}
