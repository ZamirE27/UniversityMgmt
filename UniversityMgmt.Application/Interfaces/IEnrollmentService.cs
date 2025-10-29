using UniversityMgmt.Domain.Models;

namespace UniversityMgmt.Application.Interfaces;

public interface IEnrollmentService<T> where T : Enrollment
{
    Task<IEnumerable<T>> GetAllEnrollmentsAsync();
    Task<T?> GetEnrollmentsByIdAsync(int id);
    
    Task<T> EnrollStudentInCourseAsync(int courseId, int studentId);
    Task<IEnumerable<T>> GetEnrollmentsByStudentAsync(int studentId);
    Task<IEnumerable<T>> GetEnrollmentsByCourseAsync(int courseId);
    
    Task<T> UpdateEnrollmentAsync(T enrollment);
    Task DeleteEnrollmentAsync(int id);
    
}