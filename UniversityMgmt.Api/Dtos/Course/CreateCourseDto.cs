namespace UniversityMgmt.Api.Dtos.Course;

public class CreateCourseDto
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

}