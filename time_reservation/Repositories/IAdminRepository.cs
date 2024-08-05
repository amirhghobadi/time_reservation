using Microsoft.EntityFrameworkCore;
using time_reservation.Data;
using time_reservation.Models;

namespace time_reservation.Repositories
{
    public interface IAdminRepository
    {


       



        Task<ICollection<TTime>> GetTTimesByBusinessIdAsync(int businessId);
        Admin GetAdminForLogin(string email, string password);

        Task<Admin> GetAdminByIdAsync(int id);

        Business GetBusinessById(int id);

        Task<Admin> GetAdminByTTimeId(int id);

        Task<Admin> GetAdminByBusinessId(int id);

        TTime GetTTimeById(int id);




        Task UpdateAdminAsync(Admin model);

        Task UpdateTTimeAsync(TTime model);

        Task UpdateBusinessAsync(Business model);







        void AddAdmin(Admin admin);
        void AddTTime(TTime ttime);
        void AddBusiness(Business business);
        void AddMember(Member member);


  
        void DeleteTTime(TTime ttime);
        void DeleteBusiness(Business business);


        bool IsExistUserByEmail(string email);

    }

    public class AdminRepository : IAdminRepository
    {
        private TimeReservationContext _context;

        public AdminRepository(TimeReservationContext context)
        {
            _context = context;
        }

        public void AddAdmin(Admin admin)
        {
            _context.Add(admin);
            _context.SaveChanges();
        }

        public void AddBusiness(Business business)
        {
            _context.Add(business);
            _context.SaveChanges();
        }

        public void AddMember(Member member)
        {
            _context.Add(member);
            _context.SaveChanges();
        }

        public void AddTTime(TTime ttime)
        {
            _context.Add(ttime);
            _context.SaveChanges();
        }

        public void DeleteBusiness(Business business)
        {
            _context.Remove(business);
            _context.SaveChanges();
        }

        public void DeleteTTime(TTime ttime)
        {
            _context.Remove(ttime);
            _context.SaveChanges();
        }

        public async Task<Admin> GetAdminByBusinessId(int id)
        {
            Business business = GetBusinessById(id);

            Admin admin = await GetAdminByIdAsync(business.AdminIdB);

            return  admin;
        }

        public async Task<Admin> GetAdminByIdAsync(int id)
        {

            return await _context.Admins
      .Include(a => a.Businesses) 
      .Include(a => a.TTime)
      .ThenInclude(t => t.Business)
      .FirstOrDefaultAsync(a => a.id == id);


     
        }

        public async Task<Admin>? GetAdminByTTimeId(int id)
        {
            TTime ttime =  GetTTimeById(id);

            return await _context.Admins.Include(b => b.Businesses).FirstOrDefaultAsync(x => x.id == ttime.AdminIdT);
        }

        public Admin GetAdminForLogin(string email, string password)
        {
            return _context.Admins.SingleOrDefault(a => a.Email == email && a.Password == password);
        }

        public  Business GetBusinessById(int id)
        {
            return _context.Businesses
                 .Include(b => b.TTime)
              
                 .FirstOrDefault(b => b.id == id);





        }

        public TTime GetTTimeById(int id)
        {
            return  _context.TTims.First(x => x.id == id);
        }

        public async Task<ICollection<TTime>> GetTTimesByBusinessIdAsync(int businessId)
        {
            return await _context.TTims.Where(t => t.BusinessId == businessId).ToListAsync();
        }

        public bool IsExistUserByEmail(string email)
        {
            return _context.Admins.Any(u => u.Email == email);
        }

        public async Task UpdateAdminAsync(Admin admin)
        {

            var existingAdmin = await _context.Admins.FirstOrDefaultAsync(x => x.id == admin.id);

            if (existingAdmin != null)
            {
                _context.Entry(existingAdmin).CurrentValues.SetValues(admin);

             
                var reservations = await _context.Businesses.Where(r => r.AdminIdB == admin.id).ToListAsync();

                foreach (var reservation in reservations)
                {
                    reservation.OwnerName = admin.FullName;
                }


                existingAdmin.FullName = admin.FullName;

                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateBusinessAsync(Business model)
        {
            _context.Businesses.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTTimeAsync(TTime model)
        {
            _context.TTims.Update(model);
            await _context.SaveChangesAsync();
        }

      
    }
}
