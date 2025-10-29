using UniversityMgmt.Domain.Models;

namespace UniversityMgmt.Application.Interfaces;

public interface ICourseService<T> where T : Course
{
    Task<IEnumerable<T>> GetAllCoursesAsync();
    Task<T?> GetCoursesByIdAsync(int id);
    Task<T> AddCourseAsync(T course);
    Task<T> UpdateCourseAsync(T course);
    Task<T> DeleteCourseAsync(int id);
}