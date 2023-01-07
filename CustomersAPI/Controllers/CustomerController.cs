using CustomersAPI.Interfaces;
using CustomersAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomer _customerService;

        public CustomerController(ICustomer customerService)
        {
            _customerService = customerService;
        }



        // POST api/<CustomerController>
        [HttpPost]
        public async Task PostCustomer([FromBody] Customer customer)
        {
            await _customerService.addCustomer(customer);
        }
    }
}
