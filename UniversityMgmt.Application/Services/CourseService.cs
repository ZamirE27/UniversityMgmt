using UniversityMgmt.Application.Interfaces;
using UniversityMgmt.Domain.Interfaces;
using UniversityMgmt.Domain.Models;

namespace UniversityMgmt.Application.Services;

public class CourseService : ICourseService<Course>
{
    
    private readonly ICourseRepository<Course> _courseRepository;
    private readonly IEnrollmentRepository<Enrollment> _enrollmentRepository;

    public CourseService(ICourseRepository<Course> courseRepository, IEnrollmentRepository<Enrollment> enrollmentRepository)
    {
        _courseRepository = courseRepository;
        _enrollmentRepository = enrollmentRepository;
    }
    
    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
    {
        return await _courseRepository.GetAllAsync();
    }

    public async Task<Course?> GetCoursesByIdAsync(int id)
    {
        try
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if(course == null) throw new  KeyNotFoundException($"Course with id {id} was not found.");
            return course;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error retrieving courses: {e.Message}");
            throw;
        }
    }

    public async Task<Course> AddCourseAsync(Course course)
    {
        try
        {
            if(course == null) throw new  ArgumentNullException(nameof(course));
            
            if(course.StartDate >= course.EndDate) throw new InvalidOperationException("Start date must be before end date.");
            
            if(string.IsNullOrWhiteSpace(course.Name)) throw new InvalidOperationException("Course must have a name.");
            
            return await _courseRepository.AddAsync(course);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error adding course: {e.Message}");
            throw;
        }
    }

    public async Task<Course> UpdateCourseAsync(Course course)
    {
        try
        {
            if(course == null) throw new ArgumentNullException(nameof(course));
        
            var existingCourse = await _courseRepository.GetByIdAsync(course.Id);
            if(existingCourse == null) throw new KeyNotFoundException($"Course with id {course.Id} was not found.");
        
            if(course.StartDate >= course.EndDate) throw new InvalidOperationException("Start date must be before end date.");
        
            if(string.IsNullOrWhiteSpace(course.Name)) throw new InvalidOperationException("Course must have a name.");
        
            return await _courseRepository.UpdateAsync(course);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error updating course: {e.Message}");
            throw;
        }
    }

    public async Task<Course> DeleteCourseAsync(int id)
    {
        try
        {
            var existingCourse = await _courseRepository.GetByIdAsync(id);
            if(existingCourse == null) 
                throw new KeyNotFoundException($"Course with id {id} was not found.");

            var enrollments = await _enrollmentRepository.AnyStudentByIdAsync(id);
            if(enrollments)
                throw new InvalidOperationException($"Course with id {id} has people enrolled, so it could not be deleted.");
            
            return await _courseRepository.DeleteAsync(existingCourse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}