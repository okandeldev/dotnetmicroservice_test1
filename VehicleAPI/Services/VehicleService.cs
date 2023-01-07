using Microsoft.EntityFrameworkCore;
using VehicleAPI.Data;
using VehicleAPI.Interfaces;
using VehicleAPI.Models;

namespace VehicleAPI.Services
{
    public class VehicleService : IVehicle
    {
        private ApiDbContext dbContext;

        public VehicleService()
        {
            this.dbContext = new ApiDbContext();
        }

        public async Task AddVehicle(Vehicle vehicle)
        {
            await dbContext.Vehicles.AddAsync(vehicle);
            dbContext.SaveChangesAsync();
        }

        public async Task DeleteVehicle(int id)
        {
            var vehicle = await dbContext.Vehicles.FindAsync(id);
            dbContext.Vehicles.Remove(vehicle);
            dbContext.SaveChangesAsync();
        }

        public async Task<List<Vehicle>> GetAllVehicles()
        {
            return await dbContext.Vehicles.ToListAsync();
        }

        public async Task<Vehicle> GetVehicleById(int id)
        {
            return await dbContext.Vehicles.FindAsync(id);
        }

        public async Task UpdateVehicle(int id, Vehicle vehicle)
        {
            var vehicleObject = await dbContext.Vehicles.FindAsync(id);
            vehicleObject.Name = vehicle.Name;
            vehicleObject.ImageUrl = vehicle.ImageUrl;
            vehicleObject.Height = vehicle.Height;
            vehicleObject.Width = vehicle.Width;
            vehicleObject.MaxSpeed = vehicle.MaxSpeed;
            vehicleObject.Price = vehicle.Price;
            vehicleObject.Displacement = vehicle.Displacement;
            await dbContext.SaveChangesAsync();
        }
    }
}
