using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DigiPayTest.Entities;

public partial class DigipayContext : DbContext
{
    public DigipayContext()
    {
    }

    public DigipayContext(DbContextOptions<DigipayContext> options)
        : base(options)
    {
    }

    public virtual DbSet<HourlyTbl> HourlyTbls { get; set; }

    public virtual DbSet<HourlyUnitsTbl> HourlyUnitsTbls { get; set; }

    public virtual DbSet<Temperature2mTbl> Temperature2mTbls { get; set; }

    public virtual DbSet<TimeTbl> TimeTbls { get; set; }

    public virtual DbSet<WeatherTbl> WeatherTbls { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=127.0.0.1;database=digipay;user=root;password=Aa@123456", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<HourlyTbl>(entity =>
        {
            entity.HasKey(e => e.HourlyId).HasName("PRIMARY");

            entity.ToTable("HourlyTbl");

            entity.Property(e => e.HourlyId).HasColumnName("HourlyID");
        });

        modelBuilder.Entity<HourlyUnitsTbl>(entity =>
        {
            entity.HasKey(e => e.HourlyUnitsId).HasName("PRIMARY");

            entity.ToTable("HourlyUnitsTbl");

            entity.Property(e => e.HourlyUnitsId).HasColumnName("HourlyUnitsID");
            entity.Property(e => e.Temperature2m)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("temperature2m");
            entity.Property(e => e.Time)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("time");
        });

        modelBuilder.Entity<Temperature2mTbl>(entity =>
        {
            entity.HasKey(e => e.Temperature2mId).HasName("PRIMARY");

            entity.ToTable("Temperature2mTbl");

            entity.HasIndex(e => e.HourlyId, "fk_hourly_tempre");

            entity.Property(e => e.Temperature2mId).HasColumnName("Temperature2mID");
            entity.Property(e => e.HourlyId).HasColumnName("HourlyID");
            entity.Property(e => e.Temperature2m)
                .HasPrecision(10)
                .HasColumnName("temperature2m");

            entity.HasOne(d => d.Hourly).WithMany(p => p.Temperature2mTbls)
                .HasForeignKey(d => d.HourlyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_hourly_tempre");
        });

        modelBuilder.Entity<TimeTbl>(entity =>
        {
            entity.HasKey(e => e.TimeId).HasName("PRIMARY");

            entity.ToTable("TimeTbl");

            entity.HasIndex(e => e.HourlyId, "fk_hourly_time");

            entity.Property(e => e.TimeId).HasColumnName("TimeID");
            entity.Property(e => e.HourlyId).HasColumnName("HourlyID");
            entity.Property(e => e.Time)
                .HasColumnType("datetime")
                .HasColumnName("time");

            entity.HasOne(d => d.Hourly).WithMany(p => p.TimeTbls)
                .HasForeignKey(d => d.HourlyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_hourly_time");
        });

        modelBuilder.Entity<WeatherTbl>(entity =>
        {
            entity.HasKey(e => e.WeatherId).HasName("PRIMARY");

            entity.ToTable("WeatherTbl");

            entity.HasIndex(e => e.HourlyId, "fk_hourly");

            entity.HasIndex(e => e.HourlyUnitsId, "fk_hourly_units");

            entity.Property(e => e.WeatherId).HasColumnName("WeatherID");
            entity.Property(e => e.Elevation).HasColumnName("elevation");
            entity.Property(e => e.Generationtimems)
                .HasPrecision(10)
                .HasColumnName("generationtimems");
            entity.Property(e => e.HourlyId).HasColumnName("HourlyID");
            entity.Property(e => e.HourlyUnitsId).HasColumnName("HourlyUnitsID");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.Timezone)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("timezone");
            entity.Property(e => e.Timezoneabbreviation)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("timezoneabbreviation");
            entity.Property(e => e.Utcoffsetseconds).HasColumnName("utcoffsetseconds");

            entity.HasOne(d => d.Hourly).WithMany(p => p.WeatherTbls)
                .HasForeignKey(d => d.HourlyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_hourly");

            entity.HasOne(d => d.HourlyUnits).WithMany(p => p.WeatherTbls)
                .HasForeignKey(d => d.HourlyUnitsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_hourly_units");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
