using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyASP.Models
{
    
    public class EmployeeView
    {
       
        public decimal Id { get; set; }
        
        public Guid DepartmentId { get; set; }
        public string Department { get; set; }
        
        public string SurName { get; set; }
        
        public string FirstName { get; set; }
        
        public string? Patronymic { get; set; }
        
        public DateTime DataOfBirth { get; set; }
        
        public string? DocSeries { get; set; }
        
        public string? DocNumber { get; set; }
        
        public string Position { get; set; }     
        
    }
}
