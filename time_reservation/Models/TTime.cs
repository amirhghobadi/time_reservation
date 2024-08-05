using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace time_reservation.Models
{
    public class TTime
    {


        [Key]
        public int id { get; set; }

        public int AdminIdT { get; set; }  

        [ForeignKey("AdminIdT")]
        public Admin? Admin { get; set; }  

        public int MemberId { get; set; }  

        [ForeignKey("MemberId")]
        public Member? Member { get; set; }  

        public int BusinessId { get; set; }  

        public string PictureFileName { get; set; }  

        [ForeignKey("BusinessId")]
        public Business? Business { get; set; }  

        public string? FullName { get; set; }  

        public string? BusinessName { get; set; }  


        public string? BusinessAddress { get; set; }  

        public TimeSpan ReservationHour { get; set; }  

       

        public DateTime ReservationDate { get; set; }  

        public bool IsPaymentCompleted { get; set; }  

        public DateTime DateCreated { get; set; }  



    }
}
