using Microsoft.AspNetCore.Mvc;
using UniversityMgmt.Application.Interfaces;
using UniversityMgmt.Domain.Models;

namespace UniversityMgmt.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly IStudentService<Student>  _studentService;
    readonly 
}