using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Eindopdracht_ScientistsProjects;

public partial class ScientistsProjectsContext : DbContext
{
    public ScientistsProjectsContext()
    {
    }

    public ScientistsProjectsContext(DbContextOptions<ScientistsProjectsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AssignedTo> AssignedTos { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Scientist> Scientists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Filename=ScientistsProjects.sqlite");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssignedTo>(entity =>
        {
            entity.ToTable("AssignedTo");

            entity.HasIndex(e => e.Id, "IX_AssignedTo_Id").IsUnique();

            entity.Property(e => e.ProjectId).HasColumnName("Project_id");
            entity.Property(e => e.ScientistId).HasColumnName("Scientist_id");

            entity.HasOne(d => d.Project).WithMany(p => p.AssignedTos)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Scientist).WithMany(p => p.AssignedTos)
                .HasForeignKey(d => d.ScientistId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_Projects_Id").IsUnique();
        });

        modelBuilder.Entity<Scientist>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_Scientists_Id").IsUnique();

            entity.Property(e => e.Iv).HasColumnName("IV");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
