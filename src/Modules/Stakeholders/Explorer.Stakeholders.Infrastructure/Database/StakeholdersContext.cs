﻿using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Users;

namespace Explorer.Stakeholders.Infrastructure.Database;

public class StakeholdersContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<UserRating> UserRatings { get; set; }
    public DbSet<UserProfile> UsersProfiles { get; set; }
    public DbSet<Image> Images { get; set; }

    public StakeholdersContext(DbContextOptions<StakeholdersContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("stakeholders");

        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

        modelBuilder.Entity<User>().Property(item => item.Location).HasColumnType("jsonb");

        ConfigureStakeholder(modelBuilder);
    }

    private static void ConfigureStakeholder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Person>(s => s.UserId);
    }
}