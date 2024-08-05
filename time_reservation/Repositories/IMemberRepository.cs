using Microsoft.EntityFrameworkCore;
using time_reservation.Data;
using time_reservation.Models;

namespace time_reservation.Repositories
{
    public interface IMemberRepository
    {

  

        bool IsExistUserByEmail(string email);
        void AddUser(Member member);

        void AddTTime(TTime ttime);

        Member GetUserForLogin(string email, string password);

        Business GetBusinessById(int id);

        Task<Member> GetUserByIdAsync(int id);
   
        Task UpdateMemberAsync(Member model);






    }

    public class MemberRepository : IMemberRepository
    {
        private TimeReservationContext _context;

        public MemberRepository(TimeReservationContext context)
        {
            _context = context;
        }

        public bool IsExistUserByEmail(string email)
        {
            return _context.Members.Any(u => u.Email == email);
        }

        public void AddUser(Member member)
        {
            
                _context.Add(member);
                _context.SaveChanges();
           
          
          
        }

        public Member? GetUserForLogin(string email, string password)
        {
            return _context.Members
                .SingleOrDefault(u => u.Email == email && u.Password == password);
        }

        public async Task<Member> GetUserByIdAsync(int id)
        {
             
            return await _context.Members
           .Include(m => m.TTime)
           .ThenInclude(t => t.Business)
           .FirstOrDefaultAsync(m => m.id == id);

        }



 

      

        public async Task UpdateMemberAsync(Member member)
        {

       


            var existingMember = await _context.Members.FirstOrDefaultAsync(x => x.id == member.id);

            if (existingMember != null)
            {
                _context.Entry(existingMember).CurrentValues.SetValues(member);
 
                var reservations = await _context.TTims.Where(r => r.MemberId == member.id).ToListAsync();

                foreach (var reservation in reservations)
                {
                    reservation.FullName = member.FullName;
                }
             

                existingMember.FullName = member.FullName;

                await _context.SaveChangesAsync();
            }

        }

        public Business GetBusinessById(int id)
        {
            return _context.Businesses.SingleOrDefault(x => x.id == id);
        }

        public void AddTTime(TTime ttime)
        {
            _context.Add(ttime);
            _context.SaveChanges();
        }
    }





}
