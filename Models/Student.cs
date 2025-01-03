﻿using System;
using System.Collections.Generic;

namespace Lab2SchoolDBs.Models;

public partial class Student
{
    public int PersonalNbr { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public virtual ICollection<SetGrade> SetGrades { get; set; } = new List<SetGrade>();
}
