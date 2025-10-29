using AutoMapper;
using UniversityMgmt.Api.Dtos.Student;
using UniversityMgmt.Domain.Models;

namespace UniversityMgmt.Api.Profiles;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<Student, StudentDto>().ReverseMap();
        CreateMap<Student,  UpdateStudentDto>().ReverseMap();
        CreateMap<Student, CreateStudentDto>().ReverseMap();
    }
}