using System;
using System.Collections.Generic;
using LocalEyesAPI.Models;
using LocalEyes.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LocalEyesAPI.Data;

public partial class LocalEyesDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public LocalEyesDbContext()
    {
    }

    public LocalEyesDbContext(DbContextOptions<LocalEyesDbContext> options)
        : base(options)
    {
    }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<Municipality> Municipalities { get; set; }

    public DbSet<MunicipalityReport> MunicipalityReports { get; set; }

    public DbSet<MunicipalityUser> MunicipalityUsers { get; set; }

    public DbSet<Report> Reports { get; set; }

    public DbSet<ReportComment> ReportComments { get; set; }

    public DbSet<LocalEyes.Shared.Models.Type> Types { get; set; }

    public DbSet<UserReport> UserReports { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Report>().ToTable("Report");

        modelBuilder.Entity<Comment>().ToTable("Comment");

        modelBuilder.Entity<Municipality>().ToTable("Municipality");

        modelBuilder.Entity<MunicipalityReport>().ToTable("MunicipalityReport");

        modelBuilder.Entity<MunicipalityUser>().ToTable("MunicipalityUser");

        modelBuilder.Entity<ReportComment>().ToTable("ReportComment");

        modelBuilder.Entity<LocalEyes.Shared.Models.Type>().ToTable("Type");

        modelBuilder.Entity<UserReport>().ToTable("UserReport");


        base.OnModelCreating(modelBuilder);
    }
}
