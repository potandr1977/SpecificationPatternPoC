using Domain.Passengers;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public record Passenger : FiltrationFieldsSet
    {
        //public int Id { get; set; }

        public int BookingId { get; set; }

        public string BookingRef { get; set; }

        public int? PassengerNo { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? AccountId { get; set; }

        public int? Age { get; set; }

        //public DateTime? UpdateTs { get; set; }

        public Account Account { get; set; }

        public ICollection<BoardingPass> BoardingPasses { get; set; }

        public Booking Booking { get; set; }
    }
}
