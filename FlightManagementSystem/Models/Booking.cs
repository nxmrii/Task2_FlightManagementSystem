using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManagementSystem.Models
{
    internal class Booking
    {
        public int bookingId { get; set; }
        public int passengerId { get; set; }
        public int flightId { get; set; }
        public string seatNumber { get; set; }
        public string bookingDate { get; set; }
        public decimal totalPrice { get; set; }
        public string status { get; set; }
    }
}
