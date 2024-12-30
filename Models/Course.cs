using System;
using System.Collections.Generic;

namespace Lab2SchoolDBs.Models;

public partial class Course
{
    public string CourseCode { get; set; } = null!;

    public string? CourseName { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<SetGrade> SetGrades { get; set; } = new List<SetGrade>();
}
