using UniversityMgmt.Application.Interfaces;
using UniversityMgmt.Domain.Interfaces;
using UniversityMgmt.Domain.Models;

namespace UniversityMgmt.Application.Services;

public class StudentService : IStudentService<Student>
{
    
    private readonly IStudentRepository<Student> _studentRepository;
    private readonly IEnrollmentRepository<Enrollment> _enrollmentRepository;
    
    public StudentService(IStudentRepository<Student> studentRepository, IEnrollmentRepository<Enrollment> enrollmentRepository)
    {
        _studentRepository = studentRepository;
        _enrollmentRepository = enrollmentRepository;
    }
    
    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        return await _studentRepository.GetAllAsync();
    }

    public async Task<Student?> GetStudentsByIdAsync(int StudentId)
    {
        try
        {
            var student = await _studentRepository.GetByIdAsync(StudentId);
             if (student == null) throw new KeyNotFoundException($"Student with id {StudentId} not found.");
            return student;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error retrieving student {StudentId}: {e.Message}");
            throw;
        }
    }

    public async Task<Student> AddStudentAsync(Student student)
    {
        try
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }

            if (string.IsNullOrWhiteSpace(student.FirstName) || string.IsNullOrWhiteSpace(student.LastName))
            {
                throw new ArgumentException($"the student must have a valid first name and/or last name");
            }

            return await _studentRepository.AddAsync(student);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error adding the student: {e.Message}");
            throw;
        }  
    }

    public async Task<Student> UpdateStudentAsync(Student student)
    {
        try
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }

            var existingStudent = await _studentRepository.GetByIdAsync(student.Id);
            if (existingStudent == null)
            {
                throw new KeyNotFoundException($"Student with id {student.Id} not found.");
            }

            return await _studentRepository.UpdateAsync(student);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error, the student could not be updated {e.Message}");
            throw;
        }
    }

    public async Task<Student> DeleteStudentAsync(int id)
    {
        try
        {
            var existingStudent = await _studentRepository.GetByIdAsync(id);
            if (existingStudent == null) throw new KeyNotFoundException($"Student with id {id} not found.");

            var enrollments = await _enrollmentRepository.AnyStudentByIdAsync(id);
            if (enrollments)
                throw new InvalidOperationException($"Student with id {existingStudent.Id} is already enrolled, so it could not be deleted.");
            
            return await _studentRepository.DeleteAsync(existingStudent);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error, the student could not be deleted {id}: {e.Message}");
            throw;
        }
    }
}