namespace TransformationLogic.Models
{
    public class Employer
    {
        public string CompanyName { get; set; } = default!;
        public List<Employee> Employees { get; set; } = new();
    }
}
