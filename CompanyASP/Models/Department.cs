using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyASP.Models
{
    public class Department
    {

        [Column("Id")]
        public Guid Id { get; set; }
        [Column("ParentDepartmentID")]
        public Guid? DepartmentId { get; set; }        
        [Column("Name")]
        public string Name { get; set; }
        [Column("Code")]
        public string? Code { get; set; }
        
       
    }
}
