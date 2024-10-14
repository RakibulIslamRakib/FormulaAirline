using Microsoft.AspNetCore.Mvc;
using FormulaAirline.Api.Services;
using FormulaAirline.Api.Models;
using System.Collections.Generic;

namespace FormulaAirline.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly ILogger<BookingsController> _logger;
        private readonly IMessageProducer _messageProducer;
        private readonly IKafkaMessageProducer _kafkaMessageProducer;

        public static readonly List<Booking> _bookings = new();

        public BookingsController(
            ILogger<BookingsController> logger,
            IMessageProducer messageProducer,
            IKafkaMessageProducer kafkaMessageProducer) 
        {
            _logger = logger;
            _messageProducer = messageProducer;
            _kafkaMessageProducer = kafkaMessageProducer;
        }

        [HttpPost]
        public async Task<IActionResult> CreatingBooking( Booking newBooking)
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _bookings.Add(newBooking); // Assuming _bookings is a collection of bookings
            try
            {
                // Send message to RabbitMQ
                _messageProducer.SendMessage(newBooking);

                // Send message to Kafka asynchronously
                await _kafkaMessageProducer.SendMessageAsync(newBooking); // Remove Wait()

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}