using UniversityMgmt.Application.Interfaces;
using UniversityMgmt.Domain.Interfaces;
using UniversityMgmt.Domain.Models;

namespace UniversityMgmt.Application.Services;

public class EnrollmentService : IEnrollmentService<Enrollment>
{
    
    private readonly IEnrollmentRepository<Enrollment> _enrollmentRepository;
    private readonly IStudentRepository<Student> _studentRepository;
    private readonly ICourseRepository<Course> _courseRepository;

    public EnrollmentService(IEnrollmentRepository<Enrollment> enrollmentRepository,
        IStudentRepository<Student> studentRepository, ICourseRepository<Course> courseRepository)
    {
        _enrollmentRepository = enrollmentRepository;
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
    }
 
    public async Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync()
    {
        return await _enrollmentRepository.GetAllAsync();
    }

    public async Task<Enrollment?> GetEnrollmentsByIdAsync(int id)
    {
        try
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(id);
            if (enrollment == null) throw new KeyNotFoundException($"Enrollment with id {id} was not found.");
            
            return enrollment;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error retrieving enrollment: {e.Message}");
            throw;
        }
    }

    public async Task<Enrollment> EnrollStudentInCourseAsync(int courseId, int studentId)
    {
        var course = await _courseRepository.GetByIdAsync(courseId);
        var student = await _studentRepository.GetByIdAsync(studentId);
        if (course == null || student == null)
            throw new KeyNotFoundException($"Course and Student were not found");

        var existingEnrollment = await _enrollmentRepository.AnyEnrollmentAsync(courseId, studentId);
        if(existingEnrollment) 
            throw new InvalidOperationException($"the Student with id {studentId} was already enrolled in the course with id {courseId}.");
        
        var enrollment = new Enrollment
        {
            CourseId = courseId,
            StudentId = studentId
        };
        
        await _enrollmentRepository.AddAsync(enrollment);
        return enrollment;
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentAsync(int studentId)
    {
        try
        {
            var studentExist = await _enrollmentRepository.AnyStudentByIdAsync(studentId);
            if (!studentExist) throw new KeyNotFoundException($"Student with id {studentId} was not found.");

            var enrollment = await _enrollmentRepository.GetAllAsync();
            return enrollment.Where(e => e.StudentId == studentId);

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error retrieving enrollments for student: {e.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsByCourseAsync(int courseId)
    {
        try
        {
            var courseExist = await _enrollmentRepository.AnyCourseByIdAsync(courseId);
            if (!courseExist) throw new KeyNotFoundException($"Course with id {courseId} was not found.");
            
            var enrollment = await _enrollmentRepository.GetAllAsync();
            return enrollment.Where(e => e.CourseId == courseId);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error retrieving enrollments for course: {e.Message}");
            throw;
        }
    }

    public async Task<Enrollment> UpdateEnrollmentAsync(Enrollment enrollment)
    {
        try
        {
            if (enrollment == null)
                throw new ArgumentNullException(nameof(enrollment));
            
            var existingEnrollment = await _enrollmentRepository.GetByIdAsync(enrollment.Id);
            if (existingEnrollment == null)  throw new KeyNotFoundException($"Enrollment with id {enrollment.Id} was not found.");
            
            var courseExist = await _enrollmentRepository.AnyCourseByIdAsync(enrollment.CourseId);
            var studentExist = await _enrollmentRepository.AnyStudentByIdAsync(enrollment.StudentId);
            
            if(!courseExist) 
                throw new KeyNotFoundException($"Course with id {enrollment.CourseId} was not found.");
            if(!studentExist)
                throw new KeyNotFoundException($"Student with id {enrollment.StudentId} was not found.");
            
            var duplicateEnrollment = await _enrollmentRepository.AnyEnrollmentAsync(enrollment.CourseId, enrollment.StudentId);
            if(duplicateEnrollment && (existingEnrollment.CourseId != enrollment.CourseId || existingEnrollment.StudentId != enrollment.StudentId))
                throw new InvalidOperationException($"This student is already in that course");
            
            existingEnrollment.CourseId = enrollment.CourseId;
            existingEnrollment.StudentId = enrollment.StudentId;
            return await _enrollmentRepository.UpdateAsync(existingEnrollment);
            
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error updating enrollment: {e.Message}");
            throw;
        }
    }

    public async Task DeleteEnrollmentAsync(int id)
    {
        try
        {
            var existingEnrollment = await _enrollmentRepository.GetByIdAsync(id);
            if (existingEnrollment == null)
                throw new KeyNotFoundException($"Enrollment with id {id} was not found.");
            await _enrollmentRepository.DeleteAsync(existingEnrollment);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error deleting enrollment: {e.Message}");
            throw;
        }
    }
}