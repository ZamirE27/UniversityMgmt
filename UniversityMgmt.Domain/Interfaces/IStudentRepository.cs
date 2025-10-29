using UniversityMgmt.Domain.Models;

namespace UniversityMgmt.Domain.Interfaces;

public interface IStudentRepository<T> where T : Student
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T student);
    Task<T> UpdateAsync(T student);
    Task<T> DeleteAsync(T student);
}