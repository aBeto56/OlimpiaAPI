using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OlimpiaAPI.Models;

public partial class OlimpiaContext : DbContext
{
    public OlimpiaContext()
    {
    }

    public OlimpiaContext(DbContextOptions<OlimpiaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Data> Datas { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;database=Olimpia;user=root;password=;sslmode=none;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Data>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("datas");

            entity.HasIndex(e => e.PlayerId, "PlayerId");

            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'")
                .IsFixedLength();
            entity.Property(e => e.County)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'")
                .IsFixedLength();
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text");
            entity.Property(e => e.Id).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.PlayerId)
                .HasMaxLength(36)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedTime)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Player).WithMany()
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("datas_ibfk_1");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("players");

            entity.Property(e => e.Id).HasMaxLength(36);
            entity.Property(e => e.Age).HasColumnType("int(11)");
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.Height).HasColumnType("int(11)");
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Weight).HasColumnType("int(11)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
