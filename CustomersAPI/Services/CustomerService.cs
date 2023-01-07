using Azure.Messaging.ServiceBus;
using CustomersAPI.Data;
using CustomersAPI.Interfaces;
using CustomersAPI.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CustomersAPI.Services
{
    public class CustomerService : ICustomer
    {
        private ApiDbContext dbContext;

        public CustomerService()
        {
            this.dbContext = new ApiDbContext();
        }

        public async Task addCustomer(Customer customer)
        {
            var vehicle = await dbContext.Vehicles.FirstOrDefaultAsync(v => v.Id == customer.Vehicle.Id);
            if (vehicle == null)
            {
                await dbContext.Vehicles.AddAsync(customer.Vehicle);
                await dbContext.SaveChangesAsync();
            }
            customer.Vehicle = null;
            await dbContext.Customers.AddAsync(customer);
            await dbContext.SaveChangesAsync();

            string connectionString = "Endpoint=sb://vehicletestbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=L/qP4KQW327zocOs6GCFDphVxU7Zashc6ys8YOdvCzU=";
            string queueName = "azureorderqueue";
            // since ServiceBusClient implements IAsyncDisposable we create it with "await using"
            await using var client = new ServiceBusClient(connectionString);

            var customerJsonText = JsonConvert.SerializeObject(customer);

            // create the sender
            ServiceBusSender sender = client.CreateSender(queueName);

            // create a message that we can send. UTF-8 encoding is used when providing a string.
            ServiceBusMessage message = new ServiceBusMessage(customerJsonText);

            // send the message
            await sender.SendMessageAsync(message);
        }
    }
}
