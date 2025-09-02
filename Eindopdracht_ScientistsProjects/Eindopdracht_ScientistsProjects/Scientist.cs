using System;
using System.Collections.Generic;

namespace Eindopdracht_ScientistsProjects;

public partial class Scientist
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string? Password { get; set; }

    public byte[]? Salary { get; set; }

    public byte[] Iv { get; set; }
    public byte[]? Salt { get; set; }

    public virtual ICollection<AssignedTo> AssignedTos { get; set; } = new List<AssignedTo>();

    public override string ToString()
    {
        return $"{Firstname} {Lastname}";
    }
}
