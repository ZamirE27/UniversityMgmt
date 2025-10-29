namespace UniversityMgmt.Api.Dtos.Enrollment;

public class UpdateEnrollmentDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int StudentId { get; set; }
}