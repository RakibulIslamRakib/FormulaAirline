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

        public static readonly List<Booking> _bookings = new();

        public BookingsController(
            ILogger<BookingsController> logger,
            IMessageProducer messageProducer) 
        {
            _logger = logger;
            _messageProducer = messageProducer;
        }

        [HttpPost]
        public IActionResult CreatingBooking(Booking newBooking)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            _bookings.Add(newBooking);
            try
            {
                _messageProducer.SendMessage(newBooking);

                return Ok();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        
        }
    }
}