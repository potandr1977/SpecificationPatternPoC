using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Core.Configurations;

internal class BookingLegalConfiguration :
    EntityConfigurationBase, IEntityTypeConfiguration<BookingLegal>
{
    public BookingLegalConfiguration(string tableName, string schema)
        : base(tableName, schema)
    { 
    }

    public void Configure(EntityTypeBuilder<BookingLegal> builder)
    {
        builder.ToTable(TableName, Schema);

        builder.HasKey(x => x.Id);

        builder.Property(e => e.Id).HasColumnName("booking_leg_id");
        builder.Property(e => e.BookingId).HasColumnName("booking_id");
        builder.Property(e => e.FlightId).HasColumnName("flight_id");
        builder.Property(e => e.LegNum).HasColumnName("leg_num");
        builder.Property(e => e.IsReturning).HasColumnName("is_returning");
        builder.Property(e => e.UpdateTs).HasColumnName("update_ts");
    }
}
