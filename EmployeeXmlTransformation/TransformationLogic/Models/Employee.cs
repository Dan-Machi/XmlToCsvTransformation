namespace TransformationLogic.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public Address Address { get; set; } = default!;
        public DateTime? EmployedSince { get; set; } = default!;
    }
}
