using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Core.Configurations;

internal class FlightConfiguration :
    EntityConfigurationBase, IEntityTypeConfiguration<Flight>
{
    public FlightConfiguration(string tableName, string schema)
        : base(tableName, schema)
    { 
    }

    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.ToTable(TableName, Schema);

        builder.HasKey(x => x.Id);

        builder.Property(e => e.Id).HasColumnName("flight_id");
        
        builder.Property(e => e.FlightNo).HasColumnName("flight_no");
        builder.Property(e => e.ScheduledDeparture).HasColumnName("scheduled_departure");
        builder.Property(e => e.ScheduledArrival).HasColumnName("scheduled_arrival");
        builder.Property(e => e.DepartureAirport).HasColumnName("departure_airport");
        builder.Property(e => e.ArrivalAirport).HasColumnName("arriaval_airport");
        builder.Property(e => e.Status).HasColumnName("status");
        builder.Property(e => e.AircraftCode).HasColumnName("aircraft_code");
        builder.Property(e => e.ActualDeparture).HasColumnName("actual_departure");
        builder.Property(e => e.ActualArrival).HasColumnName("actual_arrival");
        builder.Property(e => e.UpdateTs).HasColumnName("update_ts");

        builder
          .HasMany(f => f.BookingLegals)
          .WithOne(bl => bl.Flight)
          .HasForeignKey(bl => bl.FlightId)
          .IsRequired(false);
    }
}
