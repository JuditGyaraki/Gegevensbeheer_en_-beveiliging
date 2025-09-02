using System;
using System.Collections.Generic;

namespace Eindopdracht_ScientistsProjects;

public partial class Project
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<AssignedTo> AssignedTos { get; set; } = new List<AssignedTo>();

     public override string ToString()
     {
        return $"TITEL: {Name}\n\nBESCHRIJVING: {Description}\n---------\n";
     }
    
       
    
}
