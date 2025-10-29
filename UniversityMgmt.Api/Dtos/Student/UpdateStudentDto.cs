namespace UniversityMgmt.Api.Dtos.Student;

public class UpdateStudentDto //to be used in a petitions as a Put or Path
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}