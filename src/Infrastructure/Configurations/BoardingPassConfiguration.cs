using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Core.Configurations;

internal class BoardingPassConfiguration :
    EntityConfigurationBase, IEntityTypeConfiguration<BoardingPass>
{
    public BoardingPassConfiguration(string tableName, string schema)
        : base(tableName, schema)
    { 
    }

    public void Configure(EntityTypeBuilder<BoardingPass> builder)
    {
        builder.ToTable(TableName, Schema);

        builder.HasKey(x => x.Id);

        builder.Property(e => e.Id).HasColumnName("pass_id");
        builder.Property(e => e.PassengerId).HasColumnName("passenger_id");
        builder.Property(e => e.BookingLegId).HasColumnName("booking_leg_id");
        builder.Property(e => e.Seat).HasColumnName("seat");
        builder.Property(e => e.BoardingTime).HasColumnName("boarding_time");
        builder.Property(e => e.PreCheck).HasColumnName("precheck");
        builder.Property(e => e.UpdateTs).HasColumnName("update_ts");
    }
}
