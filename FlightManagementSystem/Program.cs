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


        //01 Register a Passenger
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


        //02 Add an Aircraft
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
          
            int aircraftId = context.Passengers.Count + 1;
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
            Console.WriteLine("Status: Operational");
        }


        //03 Register a Pilot
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




            int pioltID = context.Pilots.Count + 1;
            context.Pilots.Add(
                new Pilot
                {
                    pilotId = pioltID,
                    pilotName = pioltname,
                    pilotPhone = pioltphone,
                    licenseNumber = licensnum,
                    flightHours = 0,
                    isAvailable = true
                }
        );

            Console.WriteLine("Pilot registered successfully!");
            Console.WriteLine($"Pilot ID: {pioltID}");

        }




















        static void Main(string[] args)
        {


            // Main Menu
            bool exit = false;
            while (exit == false)
            {
                Console.WriteLine("Flight Management System");
                Console.WriteLine("========================================");
                Console.WriteLine(" 1. Register a Passenger");
                Console.WriteLine(" 2. Add an Aircraf");
                Console.WriteLine(" 3. Register a Pilot");
                Console.WriteLine(" 4. View All Flights");
                Console.WriteLine(" 5. Schedule a Flight");
                Console.WriteLine(" 6. Book a Flight");
                Console.WriteLine(" 7. Cancel a Booking");
                Console.WriteLine(" 8. Depart a Flight");
                Console.WriteLine(" 9. Cancel a Flight");
                Console.WriteLine(" 10. Passenger Booking History");
                Console.WriteLine(" 11. Flight Revenue & Load Factor Report");
                Console.WriteLine(" 0.  Exit");
                Console.Write("Select option: ");

                int option = int.Parse(Console.ReadLine());
                switch (option)
                {
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
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                    case 9:
                        break;
                    case 10: 
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
}}





