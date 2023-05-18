using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyASP.Models
{
    public class DepartmentView
    {
        public Guid Id { get; set; }        
        public Guid? DepartmentId { get; set; }        
        public string Name { get; set; }      
        public string? Code { get; set; }        
        public string ParentDepartment { get; set; }
    }
}
