using FlightManagementSystem.Models;
using System.Numerics;

namespace FlightManagementSystem
{
    internal class Program
    {
        //04 Declare it as a public static field directly on the Program class,
        public static FlightContext context = new FlightContext{
            Passengers = new List<Passenger>(),
            Pilots = new List<Pilot>(),
            Flights = new List<Flight>(),
            Bookings = new List<Booking>(),
            Aircrafts = new List<Aircraft>()
        };


        //case 01 Register a Passenger
        public static void RegisterPassenger()
        {
            Console.WriteLine("Enter passenger name: ");
            string name = Console.ReadLine();

            //validtion for name -> cannot be empty
            if (name == null)
            {
                Console.WriteLine("passanger name cannot be empty");
                return;
            }


                Console.WriteLine("Enter passenger email: ");
            string email = Console.ReadLine();

            Console.WriteLine("Enter passenger phone: ");
            string phone = Console.ReadLine();

            Console.WriteLine("Enter passport number: ");
            string pasportnum = Console.ReadLine();

            Console.Write("Enter nationality: ");
            string nationality = Console.ReadLine();



            int passengerId = context.Passengers.Count + 1;
            context.Passengers.Add(
                new Passenger
                {
                    passengerId = passengerId,
                    passengerName = name,
                    passengerEmail = email,
                    passengerPhone = phone,
                    passportNumber = pasportnum,
                    nationality = nationality
                }
                );

            Console.WriteLine("Passenger registered successfully!");
            Console.WriteLine($"Assigned ID: {passengerId}");

        }


        //case 02 Add an Aircraft
        public static void AddAircraft()
        {
            
            Console.WriteLine("Enter aircraft model: ");
            string model = Console.ReadLine();
            //validation
            if (model == null) {
                Console.WriteLine("Aircraft model cannot be empty.");
                return;
            }

            Console.WriteLine("Enter total seats: ");
            int totalSeats = int.Parse(Console.ReadLine());
          
            int aircraftId = context.Aircrafts.Count + 1;
            context.Aircrafts.Add(
                new Aircraft
                {
                    aircraftId = aircraftId,
                    model = model,
                    totalSeats = totalSeats,
                    isOperational = true,
                });

            Console.WriteLine("Aircraft added successfully!");
            Console.WriteLine($"Aircraft ID: {aircraftId}");
       

        }


        //case 03 Register a Pilot
        public static void registerPiolt()
        {
            Console.WriteLine("Enter Piolt name: ");
            string pioltname = Console.ReadLine();
            //validation
            if (pioltname == null)
            {
                Console.WriteLine("Pilot name cannot be empty.");
                return;
            }


            Console.WriteLine("Enter Piolt phone: ");
            string pioltphone = Console.ReadLine();

            Console.WriteLine("Enter License number: ");
            string licensnum = Console.ReadLine();

            Console.WriteLine("Enter Flight hours: ");
            int flight_hours = int.Parse(Console.ReadLine());


            int pioltID = context.Pilots.Count + 1;
            context.Pilots.Add(
                new Pilot
                {
                    pilotId = pioltID,
                    pilotName = pioltname,
                    pilotPhone = pioltphone,
                    licenseNumber = licensnum,
                    flightHours = flight_hours,
                    isAvailable = true
                   
                }
        );

            Console.WriteLine("Pilot registered successfully!");
            Console.WriteLine($"Pilot ID: {pioltID}");

        }


        //case 04 View All Flights
        public static void ViewFlights()
        {
            Console.WriteLine("All Flights");
            foreach (Flight flight in context.Flights)
            {
                Console.WriteLine(
                    $"Id: {flight.flightId} | " +
                    $"Flight code: {flight.flightCode} | " +
                    $"origin: {flight.origin} | " +
                    $"destenation: {flight.destination} | " +
                    $"date: {flight.departureDate} | " +
                    $"time: {flight.departureTime} | " +
                    $"available seats: {flight.availableSeats} | " +
                    $"ticket Price: {flight.ticketPrice} | " +
                    $"status: {flight.status}"

                    );
            }
        }



        //case 05 Schedule a Flight
        //aircraft + piolt + flight
        public static void scheduleFlight()
        {
            Console.Write("Enter Aircraft ID: ");
            int aircraftId = int.Parse(Console.ReadLine());
            // check if aircraft is operational or not
            //get from context
            Aircraft air = context.Aircrafts.FirstOrDefault(a => a.aircraftId == aircraftId);
            if(air.isOperational == false)
            {
                Console.WriteLine("Aircraft is not operational!");
                return;
            }



            Console.WriteLine("Enter Piolt id: ");
            int pioltId = int.Parse(Console.ReadLine());
            //check if piolt is available 
            Pilot pio = context.Pilots.FirstOrDefault(p => p.pilotId == pioltId);
            if(pio.isAvailable == false)
            {
                Console.WriteLine("pilot not found!");
                return;
            }


            Console.Write("Enter origin: ");
            string origin = Console.ReadLine();

            Console.Write("Enter destination: ");
            string destination = Console.ReadLine();

            Console.Write("Enter departure date: ");
            string departureDate = Console.ReadLine();

            Console.Write("Enter departure time: ");
            string departureTime = Console.ReadLine();

            Console.Write("Enter ticket price: ");
            decimal ticketPrice = decimal.Parse(Console.ReadLine());


            int flightid = context.Flights.Count + 1;
            string Code = $"OA- {flightid} ";
            context.Flights.Add(
                new Flight
                {
                    flightId = flightid,
                    flightCode = Code,
                    aircraftId = aircraftId,
                    pilotId = pioltId,
                    origin = origin,
                    destination = destination,
                    departureDate = departureDate,
                    departureTime = departureTime,
                    ticketPrice = ticketPrice,
                    availableSeats = air.totalSeats,
                    status = "Scheduled"

                }
                );

            pio.isAvailable = false;

            Console.WriteLine("Flight scheduled successfully!");
            Console.WriteLine($"Flight Code: {Code}");
           


        }


        //case 06 Book Flight
        public static void bookFlight()
        {
            //to book flight first u want to find passenger
            Console.WriteLine("Enter passenger id: ");
            int passengerId = int.Parse(Console.ReadLine());
            //now check if passenger #1 exists
            Passenger passenger = context.Passengers.FirstOrDefault(p => p.passengerId == passengerId);
            if (passenger == null)
            {
                Console.WriteLine("passenger not found!");
                return;
            }

            //ask where they want to go
            Console.WriteLine("Enter destination: ");
            string distination = Console.ReadLine();
            //Show flights going there, Look through all flights and find: distenation, status and seats available
            List<Flight> availableSeats = 
                context.Flights.Where(f => f.destination.Equals(distination.ToString())
                && f.status == "Scheduled"
                && f.availableSeats > 0).ToList();
            // then check availability
            if (availableSeats.Count == 0)
            {
                Console.WriteLine("no available flights found");
                return;
            }
            //print available flight 
            Console.WriteLine("Available Flight: ");
            foreach (Flight f in availableSeats)
            {
                Console.WriteLine(
                    $"Id: {f.flightId} | " +
                    $"code: {f.flightCode} | " +
                    $"from: {f.origin} | " +
                    $"to: {f.destination} | " +
                    $"date: {f.departureDate} | " +
                    $"time: {f.departureTime} | " +
                    $"seats: {f.availableSeats} | " +
                    $"price: {f.ticketPrice} | "
                    );
            }
            
            //choose flight and get it by id
            Console.WriteLine("Enter flight id: ");
            int flightId = int.Parse(Console.ReadLine());
            //find flight by id
            Flight choosflight = availableSeats.FirstOrDefault(f => f.flightId == flightId);

            // a seat label is assigned -> for passenger to know which seat will sit
            Aircraft aircraft = context.Aircrafts.First(a => a.aircraftId == choosflight.aircraftId);
            int seatNum = aircraft.totalSeats - choosflight.availableSeats + 1;
            string seatLabel = $"S{seatNum}";

            int bookingId = context.Bookings.Count + 1;
            //Create booking
            context.Bookings.Add(new Booking
            {
                bookingId = bookingId,
                passengerId = passengerId,
                flightId = flightId,
                seatNumber = seatLabel,
                //totalprice for the booking is taken from the flight's ticket price.
                totalPrice = choosflight.ticketPrice,
                bookingDate = DateTime.Now.ToString(),
                status = "confirmed"
            });

            // flight's available seat count decreases by one
            choosflight.availableSeats--;

            //print
            Console.WriteLine("Booking created successfully!");
            Console.WriteLine($"Booking id: {bookingId}");
            Console.WriteLine($"seat: {seatLabel}");
            Console.WriteLine($"total price: {choosflight.ticketPrice}");
        }



        //case 07 Cancel a Booking
        public static void cancelBooking()
        {
            Console.WriteLine("Enter booking id: ");
            int bookid = int.Parse(Console.ReadLine());
            Booking booking = context.Bookings.FirstOrDefault(b=> b.bookingId == bookid);
            if( booking == null) {
                Console.WriteLine("not found");
                return;
            }
            if(booking.status == "cancelled")
            {
                Console.WriteLine("already cancelled");
                return;
            }

            Flight flight = context.Flights.FirstOrDefault(f=> f.flightId == booking.flightId);
            booking.status = "cancelled";

            if (flight != null) { 
            flight.availableSeats++;
            }

            Console.WriteLine("booking is sucussfuly cancelled");

        }


        //case 08 Depart a Flight
        public static void departFlight()
        {
            Console.Write("Enter Flight ID: ");
            int flightId = int.Parse(Console.ReadLine());
            //find the flight 
            Flight flight = context.Flights.FirstOrDefault(f => f.flightId == flightId);
            if (flight == null)
            {
                Console.WriteLine("flight not found!");
                return;
            }

            //make sure it is scheduled
            if(flight.status != "Scheduled")
            {
                Console.WriteLine("flight cannot depart");
                return;
            }


            //update pilot hours
            Pilot pilot = context.Pilots.FirstOrDefault(p => p.pilotId == flight.pilotId);

            //then mark it as departed
            flight.status = "Departed";

            if(pilot != null)
            {
                Console.WriteLine("enter flihgt duration: ");
                int h = int.Parse(Console.ReadLine());

                pilot.flightHours += h;
                pilot.isAvailable = true;
            }
            Console.WriteLine("flight departed successfully!");
            Console.WriteLine("Pilot's total flight hours  " +pilot.flightHours);
        }


        //case 09 Cancel a Flight
        public static void cancelFlight()
        {
            //ask for flihgt id
            Console.WriteLine("Enter flight id: ");
            int flightid = int.Parse(Console.ReadLine());

            //find flight
            Flight flight = context.Flights.FirstOrDefault(f => f.flightId == flightid);
            if (flight == null)
            {
                Console.WriteLine("flight not found");
                return;
            }

            //check if its cancelled
            if(flight.status == "cancelled")
            {
                Console.WriteLine("flight already cancelled");
                return;
            }

            flight.status = "cancelled";
            //Find all confirmed bookings for that flight
            var bookings = context.Bookings.Where(e => e.flightId == flightid 
            && e.status.Equals("confirmed", StringComparison.OrdinalIgnoreCase)).ToList(); //converts the boolean result to a string

            //cancel flight and count how many were affected
            int bookEffcet = 0;
            foreach(Booking booking in bookings)
            {
                booking.status = "cancelled";  //set booking status => cancelled
                bookEffcet++;
            }

            //make pilot available again
            Pilot pilot = context.Pilots.FirstOrDefault(p => p.pilotId == flight.pilotId);
            if(pilot != null)
            {
                pilot.isAvailable = true;
            }

            Console.WriteLine("Flight cancelled successfully!");
            Console.WriteLine($"Affected bookings: {bookEffcet}");
        }


        //case 10 Passenger Booking History
        public static void passengerHistory()
        {
            //ask for passenger id
            Console.WriteLine("Enter passenger id: ");
            int passid = int.Parse(Console.ReadLine());

            //check passenger exict
            Passenger passenger = context.Passengers.FirstOrDefault(p => p.passengerId == passid);
            if(passenger == null) {
                Console.WriteLine("passenger not found");
                return;
            }

            //get all bookings for this passenger
            var bookings = context.Bookings.Where(b => b.passengerId == passid).ToList();
            if(bookings.Count == 0)
            {
                Console.WriteLine("no booking history found");
                return;
            }
            decimal totAmountSpent = 0;


            Console.WriteLine($"Bookings History for {passenger.passengerName} ");
            foreach (Booking booking in bookings)
            {
                Flight flight = context.Flights.FirstOrDefault(f => f.flightId == booking.flightId);
                if (flight != null)
                {
                    Console.WriteLine(
                        $"code: {flight.flightCode} | " +
                        $"orign: {flight.origin} | " +
                        $"destination: {flight.destination} | " +
                        $"date: {flight.departureDate} | " +
                        $"seat: {booking.seatNumber} | " +
                        $"price: {booking.totalPrice} | " +
                        $"status: {booking.status}"
                    );
                }

                //add only "confirmed" bookings
                if (booking.status.Contains("confirmed"))
                {
                    totAmountSpent += booking.totalPrice;
                }
            }

            Console.WriteLine($"total spent = {totAmountSpent}");
        }



        //case 11  Flight Revenue & Load Factor Report
        public static void flightreport()
        {
            decimal grandtotRevenue = 0;
            foreach (Flight flight in context.Flights)
            {
                int confirmBook = 0;
                decimal revenue = 0;

                foreach(Booking booking in context.Bookings)
                {
                    if(booking.flightId == flight.flightId && booking.status == "confirmed")
                    {
                        confirmBook++;
                        revenue += booking.totalPrice;
                    }
                }

                //get aircraft to know total seats
                Aircraft aircraft = context.Aircrafts.FirstOrDefault(a => a.aircraftId == flight.aircraftId);
                double loadfactor = 0;
                double confirmbook = confirmBook;
                if(aircraft != null)
                {
                    loadfactor = (confirmbook / aircraft.totalSeats) * 100;
                }

                //print report
                Console.WriteLine(
                    $"Flight Code: {flight.flightCode} | " +
                    $"Route: {flight.origin} -> {flight.destination} | " +
                    $"Bookings: {confirmBook} | " +
                    $"Revenue: {revenue} | " +
                    $"Load Factor: {loadfactor:F2}%"
                    );

                grandtotRevenue += revenue;
            }
            Console.WriteLine($"Grand Total Revenue: {grandtotRevenue}");
        }










        static void Main(string[] args)
        {


            // Main Menu
            bool exit = false;
            while (exit == false)
            {
                Console.WriteLine("Flight Management System");
                Console.WriteLine("========================================");
                Console.WriteLine(" 1. Register a Passenger"); //done
                Console.WriteLine(" 2. Add an Aircraf"); //done
                Console.WriteLine(" 3. Register a Pilot"); //done
                Console.WriteLine(" 4. View All Flights"); //done
                Console.WriteLine(" 5. Schedule a Flight");//done
                Console.WriteLine(" 6. Book a Flight");//done
                Console.WriteLine(" 7. Cancel a Booking");//done
                Console.WriteLine(" 8. Depart a Flight");//done
                Console.WriteLine(" 9. Cancel a Flight");//done
                Console.WriteLine(" 10. Passenger Booking History");//done
                Console.WriteLine(" 11. Flight Revenue & Load Factor Report");
                Console.WriteLine(" 0.  Exit");
                Console.Write("Select option: ");

                int option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    //easy
                    case 1:
                        RegisterPassenger();
                        break;
                    case 2:
                        AddAircraft();
                        break;
                    case 3:
                        registerPiolt();
                        break;
                    case 4:
                        ViewFlights();
                        break;

                        //meduim
                    case 5:
                        scheduleFlight();
                        break;
                    case 6:
                        bookFlight();
                        break;
                    case 7:
                        cancelBooking();
                        break;
                    case 8:
                        departFlight();
                        break;

                        //hard
                    case 9:
                        cancelFlight();
                        break;
                    case 10: 
                        passengerHistory();
                        break;
                    case 11:
                        break;
                    case 0: exit = true; break;
                    default: Console.WriteLine("Invalid option. Please try again."); break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            Console.WriteLine("Goodbye!");


        }

    
    }
}





