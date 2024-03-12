using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public record Booking
    {
        public int Id { get;set; }

        public string BookingRef { get; set; }

        public string BookingName { get; set; }

        public int AccountId { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public decimal Price { get; set; }

        public DateTime UpdateTs { get; set; }

        public ICollection<Passenger> Passengers { get; set;}

        public ICollection<BookingLegal> BookingLegals { get; set; }
    }
}
