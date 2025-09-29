using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace EmployAPI.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }

        public DateTime BirthDate { get; set; }

        //Navigation Property
        [ValidateNever]
        public Department? Department { get; set; }

        //Foreign key property
        public int DepartmentId { get; set; }
    }
}
