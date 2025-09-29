using Microsoft.EntityFrameworkCore;
using EmployAPI.Entities;

namespace EmployAPI.Data
{
    public class EmployContext(DbContextOptions<EmployContext> options) : DbContext(options)
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        //Method to seed data into our database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "HR" },
                new Department { Id = 2, Name = "IT" },
                new Department { Id = 3, Name = "Payroll" },
                new Department { Id = 4, Name = "Admin" },
                new Department { Id = 5, Name = "Finance" }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "John Doe",
                    Salary = 5000,
                    BirthDate = new DateTime(1990, 1, 1),
                    DepartmentId = 1
                },
                new Employee
                {
                    Id = 2,
                    Name = "Jane Smith",
                    Salary = 6000,
                    BirthDate = new DateTime(1985, 5, 15),
                    DepartmentId = 2
                },
                new Employee
                {
                    Id = 3,
                    Name = "Sam Brown",
                    Salary = 5500,
                    BirthDate = new DateTime(1992, 3, 10),
                    DepartmentId = 3
                },
                new Employee
                {
                    Id = 4,
                    Name = "Lisa White",
                    Salary = 7000,
                    BirthDate = new DateTime(1988, 7, 20),
                    DepartmentId = 4
                },
                new Employee
                {
                    Id = 5,
                    Name = "Tom Green",
                    Salary = 6500,
                    BirthDate = new DateTime(1991, 11, 30),
                    DepartmentId = 5
                },
                new Employee
                {
                    Id = 6,
                    Name = "Emily Black",
                    Salary = 7200,
                    BirthDate = new DateTime(1987, 9, 25),
                    DepartmentId = 1
                }
            );
        }
    }
}
