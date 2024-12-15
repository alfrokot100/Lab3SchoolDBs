using System;
using System.Collections.Generic;
using Lab2SchoolDBs.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab2SchoolDBs.Data;

public partial class SchoolDBContext : DbContext
{
    public SchoolDBContext()
    {
    }

    public SchoolDBContext(DbContextOptions<SchoolDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Personal> Personals { get; set; }

    public virtual DbSet<SetGrade> SetGrades { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-9LKEI3B\\MSSQLSERVER01;Initial Catalog=SchoolDatabases;Integrated Security=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Personal>(entity =>
        {
            entity.HasKey(e => e.PersonalId).HasName("PK__Personal__283437138E108C7B");

            entity.ToTable("Personal");

            entity.Property(e => e.PersonalId).HasColumnName("PersonalID");
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.LastName).HasMaxLength(30);
            entity.Property(e => e.Roll).HasMaxLength(30);
        });

        modelBuilder.Entity<SetGrade>(entity =>
        {
            entity.HasKey(e => e.SetGradeId).HasName("PK__SetGrade__528AC02D84D13B42");

            entity.Property(e => e.SetGradeId).HasColumnName("SetGradeID");
            entity.Property(e => e.Grade).HasMaxLength(8);
            entity.Property(e => e.PersonNummerFk).HasColumnName("PersonNummer_FK");
            entity.Property(e => e.PersonalIdFk).HasColumnName("PersonalID_FK");

            entity.HasOne(d => d.PersonNummerFkNavigation).WithMany(p => p.SetGrades)
                .HasForeignKey(d => d.PersonNummerFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SetGrades__Perso__3F466844");

            entity.HasOne(d => d.PersonalIdFkNavigation).WithMany(p => p.SetGrades)
                .HasForeignKey(d => d.PersonalIdFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SetGrades__Grade__3E52440B");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.PersonNummer).HasName("PK__Students__866B88FA86C149B8");

            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.Kurs).HasMaxLength(10);
            entity.Property(e => e.LastName).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
