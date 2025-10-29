namespace UniversityMgmt.Domain.Models;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}                          