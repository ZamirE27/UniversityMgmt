using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityMgmt.Api.Dtos.Enrollment;
using UniversityMgmt.Application.Interfaces;
using UniversityMgmt.Domain.Models;

namespace UniversityMgmt.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentController : ControllerBase
{
    private readonly IEnrollmentService<Enrollment> _enrollmentService;
    private readonly IMapper _mapper;

    public EnrollmentController(IEnrollmentService<Enrollment> enrollmentService, IMapper mapper)
    {
        _enrollmentService = enrollmentService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var enrollments = await _enrollmentService.GetAllEnrollmentsAsync();
        var enrollmentsDtos = _mapper.Map<IEnumerable<EnrollmentDto>>(enrollments);
        return Ok(enrollmentsDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var enrollments = await _enrollmentService.GetEnrollmentsByIdAsync(id);
        if (enrollments == null)
        {
            return NotFound($"Enrollment with id {id} not found.");
        }
        var dto = _mapper.Map<EnrollmentDto>(enrollments);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateEnrollmentDto CreateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var enrollment = _mapper.Map<Enrollment>(CreateDto);
        var created = await _enrollmentService.EnrollStudentInCourseAsync(CreateDto.StudentId, CreateDto.CourseId);
        var dto = _mapper.Map<EnrollmentDto>(created);
        return CreatedAtRoute(nameof(GetByIdAsync), new { id = created.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateEnrollmentDto UpdateDto)
    {
        if(id !=  UpdateDto.Id)
            return BadRequest("Id in URL doesn't match");
        var enrollment = _mapper.Map<Enrollment>(UpdateDto);
        var updated = await _enrollmentService.UpdateEnrollmentAsync(enrollment);
        var dto = _mapper.Map<EnrollmentDto>(updated);

        return Ok(dto);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _enrollmentService.DeleteEnrollmentAsync(id);
        return NoContent();
    }
}