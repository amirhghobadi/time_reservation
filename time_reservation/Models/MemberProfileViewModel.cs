using System.ComponentModel.DataAnnotations;

namespace time_reservation.Models
{
    public class MemberProfileViewModel
    {
        public Member Member { get; set; }
        public ChangePasswordViewModel ChangePasswordViewModel { get; set; }





    }


        public class AddTTimeForMember
        {



            public int BusinessId { get; set; }



            public int MemberId { get; set; }


            public string UserName { get; set; }

            public TimeSpan ReservationHour { get; set; }


            public DateTime ReservationDate { get; set; }






        }


}
