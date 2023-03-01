using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Contexts
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> context) : base(context) 
        {
            
        }
        
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Profiling> Profilings { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<University> Universities { get; set;}
        public DbSet<Employee> Employees { get; set; }

        //fluent API

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>().HasData(
             new Role
            {
               Id = 1,
               Name= "Admin",
            },
             new Role
             {
                 Id = 2,
                 Name = "User"
             });
            //membuat atribut unik
            modelBuilder.Entity<Employee>().HasAlternateKey(e => new
            {
                e.Email,
                e.PhoneNumber
            });
            modelBuilder.Entity<Employee>().HasIndex(e => new
            {
                e.Email,
                e.PhoneNumber
            }).IsUnique();

            //relasi one employee to one account sekaligus pk
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Account)
                .WithOne(a => a.Employee)
                .HasForeignKey<Account>(fk => fk.EmployeeNik);

            modelBuilder.Entity<Employee>()
               .HasMany(e => e.Employees)
               .WithOne(e => e.Manager)
               .HasForeignKey(fk => fk.ManagerId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
