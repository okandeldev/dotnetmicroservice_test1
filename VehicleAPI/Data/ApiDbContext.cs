﻿using Microsoft.EntityFrameworkCore;
using VehicleAPI.Models;

namespace VehicleAPI.Data
{
    public class ApiDbContext: DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=VehicleApiDb");
        }
    }
}
