using System;
using System.Collections.Generic;

namespace Lab2SchoolDBs.Models;

public partial class Personal
{
    public int PersonalId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Roll { get; set; } = null!;

    public virtual ICollection<SetGrade> SetGrades { get; set; } = new List<SetGrade>();
}
