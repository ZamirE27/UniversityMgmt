using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityMgmt.Domain.Models;

public class Enrollment
{
    public int Id { get; set; }
    
    public int CourseId { get; set; } // la relacion entre las tablas de uno
    public Course? Course { get; set; }
    
    public int StudentId { get; set; } // la relacion entre las tablas de uno
    public Student? Student { get; set; }
    
}