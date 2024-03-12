using System;

namespace Domain.Entities
{
    public record Phone
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public string PhoneValue { get; set; }

        public string PhoneType { get; set; }

        public bool PrimaryPhone { get; set; }

        public DateTime UpdateTs { get; set; }

        public Account Account { get; set; }
    }
}
