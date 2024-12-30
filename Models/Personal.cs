using System;
using System.Collections.Generic;

namespace Lab2SchoolDBs.Models;

public partial class Personal
{
    public int PersonalId { get; set; }

    public string? FirstNamn { get; set; }

    public string? LastName { get; set; }

    public string Field { get; set; } = null!;

    public string? Department { get; set; }

    public decimal? Salary { get; set; }

    public virtual ICollection<SetGrade> SetGrades { get; set; } = new List<SetGrade>();
}
