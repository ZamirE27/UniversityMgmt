using Microsoft.EntityFrameworkCore;
using UniversityMgmt.Domain.Interfaces;
using UniversityMgmt.Domain.Models;
using UniversityMgmt.Infrastructure.Data;

namespace UniversityMgmt.Infrastructure.Repositories;

public class CourseRepository : ICourseRepository<Course>
{
    
    private readonly AppDbContext _context;
    public CourseRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _context.Courses.ToListAsync();
    }

    public async Task<Course?> GetByIdAsync(int id)
    {
        return await _context.Courses
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Course> AddAsync(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        return course;
    }

    public async Task<Course> UpdateAsync(Course course)
    {
        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
        return course;
    }

    public async Task<Course> DeleteAsync(Course course)
    {
        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        return course;
    }
}