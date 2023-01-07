using ReservationsAPI.Models;

namespace ReservationsAPI.Interfaces
{
    public interface IReservation
    {
        // Get reservations from Azure Message Queue
        Task<List<Reservation>> GetReservations();
        // Confirmation Email
        Task UpdateMailStatus(int id);
    }
}
