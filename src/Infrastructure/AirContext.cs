using Domain.Core.Configurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Core;

public class AirContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }

    public DbSet<BoardingPass> BoardingPasses { get; set; }

    public DbSet<Booking> Bookings { get; set; }

    public DbSet<BookingLegal> BookingLegals { get; set; }

    public DbSet<Flight> Flights { get; set; }

    public DbSet<Passenger> Passengers { get; set; }

    public DbSet<Phone> Phones { get; set; }

    public AirContext(DbContextOptions<AirContext> options)
        : base(options)
    {         
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountConfiguration("account", "postgres_air"));
        modelBuilder.ApplyConfiguration(new BookingConfiguration("booking", "postgres_air"));
        modelBuilder.ApplyConfiguration(new BookingLegalConfiguration("booking_leg", "postgres_air"));
        modelBuilder.ApplyConfiguration(new BoardingPassConfiguration("boarding_pass", "postgres_air"));
        modelBuilder.ApplyConfiguration(new FlightConfiguration("flight", "postgres_air"));
        modelBuilder.ApplyConfiguration(new PassengerConfiguration("passenger", "postgres_air"));
        modelBuilder.ApplyConfiguration(new PhoneConfiguration("phone", "postgres_air"));
    }
}
