namespace UniversityMgmt.Api.Dtos.Student;

public class StudentDto // to be used in petition as a get or post responses
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}