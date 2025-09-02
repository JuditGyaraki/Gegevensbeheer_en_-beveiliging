using System;
using System.Collections.Generic;

namespace Eindopdracht_ScientistsProjects;

public partial class 
    
    AssignedTo
{
    public int Id { get; set; }

    public int ScientistId { get; set; }

    public int ProjectId { get; set; }

    public int? Hours { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual Scientist Scientist { get; set; } = null!;

    
}
