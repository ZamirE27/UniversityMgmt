using AutoMapper;
using UniversityMgmt.Api.Dtos.Course;
using UniversityMgmt.Api.Dtos.Student;
using UniversityMgmt.Domain.Models;

namespace UniversityMgmt.Api.Profiles;

public class CourseProfile : Profile
{
   public CourseProfile()
   {
      CreateMap<Course, StudentDto>().ReverseMap();
      CreateMap<Course, CreateCourseDto>().ReverseMap();
      CreateMap<Course, UpdateCourseDto>().ReverseMap();
   }
}

