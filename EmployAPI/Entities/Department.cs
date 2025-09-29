using System.ComponentModel.DataAnnotations;

namespace EmployAPI.Entities
{
    public class Department
    {
        public int Id { get; set; }

        public required string Name { get; set; }
    }
}
