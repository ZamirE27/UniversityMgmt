using AutoMapper;
using UniversityMgmt.Api.Dtos.Enrollment;
using UniversityMgmt.Domain.Models;

namespace UniversityMgmt.Api.Profiles;

public class EnrollmentProfile : Profile
{
    public EnrollmentProfile()
    {
        CreateMap<Enrollment, EnrollmentDto>().ReverseMap();
        CreateMap<Enrollment, CreateEnrollmentDto>().ReverseMap();
        CreateMap<Enrollment, UpdateEnrollmentDto>().ReverseMap();
    }
}