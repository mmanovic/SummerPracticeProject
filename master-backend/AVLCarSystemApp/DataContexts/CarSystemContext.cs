using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVLCarSystemApp.ModelsDTO;

namespace AVLCarSystemApp.DataContexts
{
  public class CarSystemContext : DbContext
  {
    public CarSystemContext(DbContextOptions<CarSystemContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<CountryDto>().HasIndex(s => s.Name).IsUnique();
      modelBuilder.Entity<InventoryDto>()
        .HasOne(s => s.ModelDto)
        .WithMany(s => s.Inventories)
        .OnDelete(DeleteBehavior.Restrict);

      modelBuilder.Entity<InventoryDto>()
        .HasOne(s => s.SalonDto)
        .WithMany(s => s.Inventories)
        .OnDelete(DeleteBehavior.Restrict);

      modelBuilder.Entity<SalonDto>()
        .HasOne(s => s.CityDto)
        .WithMany(s => s.Salons)
        .OnDelete(DeleteBehavior.Restrict);

      modelBuilder.Entity<SalonDto>()
        .HasOne(s => s.CountryDto)
        .WithMany(s => s.Salons)
        .OnDelete(DeleteBehavior.Restrict);

      modelBuilder.Entity<ManufacturerDto>()
        .HasOne(s => s.CityDto)
        .WithMany(s => s.Manufacturers)
        .OnDelete(DeleteBehavior.Restrict);

      modelBuilder.Entity<ManufacturerDto>()
        .HasOne(s => s.CountryDto)
        .WithMany(s => s.Manufacturers)
        .OnDelete(DeleteBehavior.Restrict);

      modelBuilder.Entity<ModelDto>()
        .HasOne(s => s.ManufacturerDto)
        .WithMany(s => s.Models)
        .OnDelete(DeleteBehavior.Restrict);

      modelBuilder.Entity<ModelDto>()
        .HasOne(s => s.EquipmentDto)
        .WithMany(s => s.Models)
        .OnDelete(DeleteBehavior.Restrict);

      modelBuilder.Entity<ModelDto>()
        .HasOne(s => s.EngineDto)
        .WithMany(s => s.Models)
        .OnDelete(DeleteBehavior.Restrict);

      modelBuilder.Entity<EngineDto>()
        .HasOne(s => s.EngineTypeDto)
        .WithMany(s => s.Engines)
        .OnDelete(DeleteBehavior.Restrict);
    }

    public DbSet<CityDto> Cities { get; set; }

    public DbSet<CountryDto> Countries { get; set; }

    public DbSet<EngineDto> Engines { get; set; }

    public DbSet<EngineTypeDto> EngineTypes { get; set; }

    public DbSet<InventoryDto> Inventories { get; set; }

    public DbSet<ManufacturerDto> Manufacturers { get; set; }

    public DbSet<ModelDto> Models { get; set; }

    public DbSet<SalonDto> Salons { get; set; }

    public DbSet<EquipmentDto> Equipments { get; set; }

  }
}
