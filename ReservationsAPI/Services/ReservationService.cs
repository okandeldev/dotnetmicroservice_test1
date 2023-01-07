using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReservationsAPI.Data;
using ReservationsAPI.Interfaces;
using ReservationsAPI.Models;
using System.Net;
using System.Net.Mail;
using System.Text.Json.Serialization;

namespace ReservationsAPI.Services
{
    public class ReservationService : IReservation
    {
        private readonly ApiDbContext dbContext;

        public ReservationService()
        {
            dbContext = new ApiDbContext();
        }

        public async Task<List<Reservation>> GetReservations()
        {
            // adding receiver code for azure service bus later
            string connectionString = "Endpoint=sb://vehicletestbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=L/qP4KQW327zocOs6GCFDphVxU7Zashc6ys8YOdvCzU=";
            string queueName = "azureorderqueue";
            // since ServiceBusClient implements IAsyncDisposable we create it with "await using"
            await using var client = new ServiceBusClient(connectionString);
           // create a receiver that we can use to receive the message
            ServiceBusReceiver receiver = client.CreateReceiver(queueName);

            // the received message is a different type as it contains some service set properties
            IReadOnlyList<ServiceBusReceivedMessage> receivedMessages = (IReadOnlyList<ServiceBusReceivedMessage>)await receiver.ReceiveMessagesAsync(10);

            if (receivedMessages == null)
            {
                return null;
            }

            foreach(ServiceBusReceivedMessage receivedMessage in receivedMessages) {
                string body = receivedMessage.Body.ToString();
                var messageCreated = JsonConvert.DeserializeObject<Reservation>(body);
                await dbContext.Reservations.AddAsync(messageCreated);
                await dbContext.SaveChangesAsync();
                await receiver.CompleteMessageAsync(receivedMessage);
            }
            return await dbContext.Reservations.ToListAsync();
        }

        public async Task UpdateMailStatus(int id)
        {
            // logic send email to customer
            // TODO: configure send email 
            var reservationResult = await dbContext.Reservations.FindAsync(id);
            if (reservationResult != null && reservationResult.IsMailSent == false)
            {
                var smtpClient = new SmtpClient("smtp.live.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("vehicletestdrive11@outlook.com", "Ahfh@113"),
                    EnableSsl = true
                };
                smtpClient.Send("vehicletestdrive11@outlook.com", reservationResult.Email, "subject", "body");
                reservationResult.IsMailSent = true;
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
