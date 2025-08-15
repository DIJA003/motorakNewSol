
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using motorak.dal.Entites;
using motorak.dal.Entities;
using Motorak.DAL;
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
        public DbSet<User>Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceReview> ServiceReviews { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                        .HasOne(c => c.User)
                        .WithOne(u => u.Customer)
                        .HasForeignKey<Customer>(c => c.UserId);

            modelBuilder.Entity<Mechanic>()
                .HasOne(m => m.User)
                .WithOne(u => u.Mechanic)
                .HasForeignKey<Mechanic>(m => m.UserId);

            modelBuilder.Entity<Purchase>().HasBaseType<Transactions>();
            modelBuilder.Entity<Rent>().HasBaseType<Transactions>();

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(c => c.Car)
                    .WithMany()
                    .HasForeignKey(c => c.CarId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.User)
                    .WithMany()
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(c => c.ItemType)
                    .HasConversion<string>();
            });



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
