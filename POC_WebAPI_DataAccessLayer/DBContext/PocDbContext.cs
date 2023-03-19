using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using POC_WebAPI_DataAccessLayer.Authentication_Models;
using POC_WebAPI_DataAccessLayer.Models;

namespace POC_WebAPI_DataAccessLayer.DBContext;

public partial class PocDbContext : IdentityDbContext<ApplicationUser>
{
    public PocDbContext()
    {
    }

    public PocDbContext(DbContextOptions<PocDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<College> Colleges { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<FileDetails> FileDetails { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MLI01139;Initial Catalog=POC_Db;User ID=sa;Password=***********;Integrated security=True;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<College>(entity =>
        {
            entity.HasKey(e => e.Cid);

            entity.ToTable("College", "A00");

            entity.Property(e => e.Caddress)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CAddress");
            entity.Property(e => e.Ccode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CCode");
            entity.Property(e => e.Cname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CName");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Czipcode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CZipcode");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Did);

            entity.ToTable("Department", "A00");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Dblock)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("DBlock");
            entity.Property(e => e.Dhod)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DHod");
            entity.Property(e => e.Dname)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DName");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.CidNavigation).WithMany(p => p.Departments)
                .HasForeignKey(d => d.Cid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Department_College");
        });

        modelBuilder.Entity<FileDetails>(entity =>
        {
            entity.ToTable("FileDetails", "A00");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.SidNavigation).WithMany(p => p.FileDetails)
                .HasForeignKey(d => d.Sid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FileDetails_Student");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Sid);

            entity.ToTable("Student", "A00");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Saddress)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SAddress");
            entity.Property(e => e.Sdob)
                .HasColumnType("date")
                .HasColumnName("SDob");
            entity.Property(e => e.SfirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SFirstName");
            entity.Property(e => e.SlastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SLastName");
            entity.Property(e => e.SmiddleName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SMiddleName");
            entity.Property(e => e.SrollNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SRollNumber");

            entity.HasOne(d => d.DidNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.Did)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Department");
        });
        base.OnModelCreating(modelBuilder);
        //  OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
