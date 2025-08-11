
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using motorak.dal.Entites;
using motorak.dal.Entities;
using Motorak.DAL.Entites;
using Motorak.DAL.Entities;


namespace motorak.DAL.DataBase
{
    public class MotorakDbContext : IdentityDbContext<User>
    {
        public MotorakDbContext(DbContextOptions<MotorakDbContext> options)
                 : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceReview> ServiceReviews { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Rent> Rents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Purchase>().HasBaseType<Transactions>();
            modelBuilder.Entity<Rent>().HasBaseType<Transactions>();



            modelBuilder.Entity<Service>()
        .HasOne(s => s.Customer)
        .WithMany()
        .HasForeignKey(s => s.CustomerId)
        .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Service>()
                .HasOne(s => s.Mechanic)
                .WithMany()
                .HasForeignKey(s => s.MechanicId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Service>()
                .HasOne(s => s.Car)
                .WithMany()
                .HasForeignKey(s => s.CarId)
                .OnDelete(DeleteBehavior.Restrict);
        }



    }
}
