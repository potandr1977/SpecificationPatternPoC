using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Core.Configurations;

internal class BookingConfiguration :
    EntityConfigurationBase, IEntityTypeConfiguration<Booking>
{
    public BookingConfiguration(string tableName, string schema)
        : base(tableName, schema)
    { 
    }

    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable(TableName, Schema);

        builder.HasKey(x => x.Id);

        builder.Property(e => e.Id).HasColumnName("booking_id");
        builder.Property(e => e.BookingRef).HasColumnName("booking_ref");
        builder.Property(e => e.BookingName).HasColumnName("booking_name");
        builder.Property(e => e.AccountId).HasColumnName("account_id");
        builder.Property(e => e.Email).HasColumnName("email");
        builder.Property(e => e.Phone).HasColumnName("phone");
        builder.Property(e => e.UpdateTs).HasColumnName("update_ts");
        builder.Property(e => e.Price).HasColumnName("price");

        builder
           .HasMany(b => b.Passengers)
           .WithOne(p => p.Booking)
           .HasForeignKey(p => p.BookingId)
           .IsRequired(true);

        builder
           .HasMany(b => b.BookingLegals)
           .WithOne(bl => bl.Booking)
           .HasForeignKey(p => p.BookingId)
           .IsRequired(true);
    }
}
