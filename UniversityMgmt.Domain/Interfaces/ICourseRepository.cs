using UniversityMgmt.Domain.Models;

namespace UniversityMgmt.Domain.Interfaces;

public interface ICourseRepository<T> where T : Course
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T course);
    Task<T> UpdateAsync(T course);
    Task<T> DeleteAsync(T course);
}