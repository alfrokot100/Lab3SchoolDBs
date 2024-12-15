using System;
using System.Collections.Generic;

namespace Lab2SchoolDBs.Models;

public partial class SetGrade
{
    public int SetGradeId { get; set; }

    public int PersonalIdFk { get; set; }

    public int PersonNummerFk { get; set; }

    public DateOnly Datum { get; set; }

    public string Grade { get; set; } = null!;

    public virtual Student PersonNummerFkNavigation { get; set; } = null!;

    public virtual Personal PersonalIdFkNavigation { get; set; } = null!;
}
