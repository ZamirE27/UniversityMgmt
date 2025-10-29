namespace UniversityMgmt.Api.Dtos.Course;

public class UpdateCourseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}