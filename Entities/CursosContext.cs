using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TeamHubServiceUser.Entities;

public partial class CursosContext : DbContext
{
    public CursosContext()
    {
    }

    public CursosContext(DbContextOptions<CursosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<curso> curso { get; set; }

    public virtual DbSet<document> document { get; set; }

    public virtual DbSet<extension> extension { get; set; }

    public virtual DbSet<project> project { get; set; }

    public virtual DbSet<projectdocument> projectdocument { get; set; }

    public virtual DbSet<projectstudent> projectstudent { get; set; }

    public virtual DbSet<projecttask> projecttask { get; set; }

    public virtual DbSet<student> student { get; set; }

    public virtual DbSet<task> task { get; set; }

    public virtual DbSet<taskstudent> taskstudent { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;database=teamhub;user=root;password=root");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<curso>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");

            entity.Property(e => e.Clave).HasMaxLength(10);
            entity.Property(e => e.Costo).HasPrecision(6);
            entity.Property(e => e.Descripcion).HasMaxLength(1000);
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.FechaTermino).HasColumnType("datetime");
            entity.Property(e => e.Instructor).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(200);
        });

        modelBuilder.Entity<document>(entity =>
        {
            entity.HasKey(e => e.IdDocument).HasName("PRIMARY");

            entity.HasIndex(e => e.IdProject, "FK_Document_Project_idx");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Path).HasMaxLength(250);

            entity.HasOne(d => d.IdProjectNavigation).WithMany(p => p.document)
                .HasForeignKey(d => d.IdProject)
                .HasConstraintName("FK_Document_Project");
        });

        modelBuilder.Entity<extension>(entity =>
        {
            entity.HasKey(e => e.IdExtension).HasName("PRIMARY");

            entity.Property(e => e.Extension1)
                .HasMaxLength(45)
                .HasColumnName("Extension");
        });

        modelBuilder.Entity<project>(entity =>
        {
            entity.HasKey(e => e.IdProject).HasName("PRIMARY");

            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.StartDate).HasColumnType("date");
        });

        modelBuilder.Entity<projectdocument>(entity =>
        {
            entity.HasKey(e => e.IdProjectDocument).HasName("PRIMARY");

            entity.HasIndex(e => e.IdDocument, "IdDocument");

            entity.HasIndex(e => e.IdProject, "IdProject");

            entity.HasOne(d => d.IdDocumentNavigation).WithMany(p => p.projectdocument)
                .HasForeignKey(d => d.IdDocument)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("projectdocument_ibfk_2");

            entity.HasOne(d => d.IdProjectNavigation).WithMany(p => p.projectdocument)
                .HasForeignKey(d => d.IdProject)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("projectdocument_ibfk_1");
        });

        modelBuilder.Entity<projectstudent>(entity =>
        {
            entity.HasKey(e => e.IdProjectStudent).HasName("PRIMARY");

            entity.HasIndex(e => e.IdProject, "IdProject");

            entity.HasIndex(e => e.IdStudent, "IdStudent");

            entity.HasOne(d => d.IdProjectNavigation).WithMany(p => p.projectstudent)
                .HasForeignKey(d => d.IdProject)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("projectstudent_ibfk_1");

            entity.HasOne(d => d.IdStudentNavigation).WithMany(p => p.projectstudent)
                .HasForeignKey(d => d.IdStudent)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("projectstudent_ibfk_2");
        });

        modelBuilder.Entity<projecttask>(entity =>
        {
            entity.HasKey(e => e.IdProjectTask).HasName("PRIMARY");

            entity.HasIndex(e => e.IdProject, "IdProject");

            entity.HasIndex(e => e.IdTask, "IdTask");

            entity.HasOne(d => d.IdProjectNavigation).WithMany(p => p.projecttask)
                .HasForeignKey(d => d.IdProject)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("projecttask_ibfk_1");

            entity.HasOne(d => d.IdTaskNavigation).WithMany(p => p.projecttask)
                .HasForeignKey(d => d.IdTask)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("projecttask_ibfk_2");
        });

        modelBuilder.Entity<student>(entity =>
        {
            entity.HasKey(e => e.IdStudent).HasName("PRIMARY");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(15);
            entity.Property(e => e.MiddleName).HasMaxLength(15);
            entity.Property(e => e.Name).HasMaxLength(15);
            entity.Property(e => e.Password).HasMaxLength(30);
            entity.Property(e => e.ProDocumentImage).HasMaxLength(250);
            entity.Property(e => e.SurName).HasMaxLength(15);
        });

        modelBuilder.Entity<task>(entity =>
        {
            entity.HasKey(e => e.IdTask).HasName("PRIMARY");

            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.StartDate).HasColumnType("date");
        });

        modelBuilder.Entity<taskstudent>(entity =>
        {
            entity.HasKey(e => e.IdTaskStudent).HasName("PRIMARY");

            entity.HasIndex(e => e.IdStudent, "IdStudent");

            entity.HasIndex(e => e.IdTask, "IdTask");

            entity.HasOne(d => d.IdStudentNavigation).WithMany(p => p.taskstudent)
                .HasForeignKey(d => d.IdStudent)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("taskstudent_ibfk_2");

            entity.HasOne(d => d.IdTaskNavigation).WithMany(p => p.taskstudent)
                .HasForeignKey(d => d.IdTask)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("taskstudent_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
