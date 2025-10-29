namespace UniversityMgmt.Domain.Models;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; } 
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();  // relacion de muchos
}