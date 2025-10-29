namespace UniversityMgmt.Api.Dtos.Student;

public class CreateStudentDto //to be used, petitions as a post
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}