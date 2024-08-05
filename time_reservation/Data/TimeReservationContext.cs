using Microsoft.EntityFrameworkCore;
using time_reservation.Models;

namespace time_reservation.Data
{
    public class TimeReservationContext:DbContext
    {



        public TimeReservationContext(DbContextOptions<TimeReservationContext> options ):base(options)
        {

        }




        public DbSet<TTime> TTims { get; set; }

        public DbSet<Member> Members { get; set; }


        public DbSet<Admin> Admins { get; set; }
  
        public DbSet<Business> Businesses { get; set; }

  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

          


            // Relationship between Admin and Business (one-to-many)
            modelBuilder.Entity<Admin>()
                .HasMany(a => a.Businesses)
                .WithOne(b => b.Admin)
                .HasForeignKey(b => b.AdminIdB)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship between Member and Reserve (one-to-many)
            modelBuilder.Entity<Member>()
                .HasMany(m => m.TTime)
                .WithOne(r => r.Member)
                .HasForeignKey(r => r.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship between Business and Reserve (one-to-many)
            modelBuilder.Entity<Business>()
                .HasMany(b => b.TTime)
                .WithOne(r => r.Business)
                .HasForeignKey(r => r.BusinessId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship between Admin and Reserve (one-to-many)
            modelBuilder.Entity<Admin>()
                .HasMany(a => a.TTime)
                .WithOne(r => r.Admin)
                .HasForeignKey(r => r.AdminIdT)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Business>(i =>
            {
                i.Property(w => w.Price).HasColumnType("Money");
                i.HasKey(w => w.id);
            });

       



            base.OnModelCreating(modelBuilder);
        }
    }
}
