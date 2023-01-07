using Microsoft.AspNetCore.Mvc;
using ReservationsAPI.Interfaces;
using ReservationsAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReservationsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private IReservation _reservationService;

        public ReservationController(IReservation reservationService)
        {
            _reservationService = reservationService;
        }


        // GET api/<ReservationController>
        [HttpGet("")]
        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            return await _reservationService.GetReservations();
        }

        // PUT api/<ReservationController>/5
        [HttpPut("{id}")]
        public async Task PutEmailConfirmation(int id)
        {
            await _reservationService.UpdateMailStatus(id);
        }
    }
}
