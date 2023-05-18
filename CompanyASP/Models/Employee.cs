using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyASP.Models
{
    [Table("Employee")]
    public class Employee
    {
        
        [Column("ID")][Key]
        public decimal Id { get; set; }
        [Column("DepartmentID")]
        public Guid DepartmentId { get; set; }        
        [Column("SurName")]
        public string SurName { get; set; }
        [Column("FirstName")]
        public string FirstName { get; set; }
        [Column("Patronymic")]
        public string? Patronymic { get; set; }
        [Column("DateOfBirth")]
        public DateTime DataOfBirth { get; set; }
        [Column("DocSeries")]
        public string? DocSeries { get; set; }
        [Column("DocNumber")]
        public string? DocNumber { get; set; }
        [Column("Position")]
        public string Position { get; set; }     
        
    }
}
