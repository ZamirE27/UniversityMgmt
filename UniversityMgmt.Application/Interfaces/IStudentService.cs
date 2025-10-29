using UniversityMgmt.Domain.Models;

namespace UniversityMgmt.Application.Interfaces;

public interface IStudentService<T> where T : Student
{
    Task<IEnumerable<T>> GetAllStudentsAsync();
    Task<T> GetStudentsByIdAsync(int StudentId);
    Task<T> AddStudentAsync(T student);
    Task<T> UpdateStudentAsync(T student);
    Task<T> DeleteStudentAsync(int id);
}