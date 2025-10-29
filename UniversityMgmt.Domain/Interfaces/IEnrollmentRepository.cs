using UniversityMgmt.Domain.Models;

namespace UniversityMgmt.Domain.Interfaces;

public interface IEnrollmentRepository<T> where T : Enrollment
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int enrollmentId);
    Task<T> AddAsync(T enrollment);
    Task<T> UpdateAsync(T enrollment);
    Task<T> DeleteAsync(T enrollment);
}