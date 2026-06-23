using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManagementSystem.Models
{
    internal class Aircraft
    {
        //02 CLASS MODELS & PROPERTIES
        public int aircraftId {  get; set; }
        public string model {  get; set; }
        public int totalSeats { get; set; }
        public bool isOperational { get; set; }
    }
}
