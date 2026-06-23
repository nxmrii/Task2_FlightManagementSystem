using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManagementSystem.Models
{
    internal class Passenger
    {
        public int passengerId { get; set; }
        public string passengerName { get; set; }
        public string passengerEmail { get; set; }
        public string passengerPhone { get; set; }
        public string passportNumber { get; set; }
        public string nationality { get; set; }
    }
}
