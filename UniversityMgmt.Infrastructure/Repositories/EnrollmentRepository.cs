using Microsoft.EntityFrameworkCore;
using UniversityMgmt.Domain.Interfaces;
using UniversityMgmt.Domain.Models;
using UniversityMgmt.Infrastructure.Data;

namespace UniversityMgmt.Infrastructure.Repositories;

public class EnrollmentRepository : IEnrollmentRepository<Enrollment>
{
    
    private readonly AppDbContext _context;
    public EnrollmentRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Enrollment>> GetAllAsync()
    {
        return await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .ToListAsync();
    }

    public async Task<Enrollment?> GetByIdAsync(int enrollmentId)
    {
        return await _context.Enrollments
            .Include(s => s.Student)
            .Include(s => s.Course)
            .AsNoTracking()
            .FirstOrDefaultAsync(enrollment => enrollment.Id == enrollmentId);
    }

    public async Task<Enrollment> AddAsync(Enrollment enrollment)
    {
        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();
        return enrollment;
    }

    public async Task<Enrollment> UpdateAsync(Enrollment enrollment)
    {
        _context.Enrollments.Update(enrollment);
        await _context.SaveChangesAsync();
        return enrollment;
    }

    public async Task<Enrollment> DeleteAsync(Enrollment enrollment)
    {
        _context.Enrollments.Remove(enrollment);
        await _context.SaveChangesAsync();
        return enrollment;
    }
}