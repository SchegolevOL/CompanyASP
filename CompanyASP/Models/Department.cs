namespace CompanyASP.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        public Guid? ParentDepartmentID { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; }
    }
}
