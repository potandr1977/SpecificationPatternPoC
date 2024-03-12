using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Core.Configurations;

internal class PassengerConfiguration :
    EntityConfigurationBase, IEntityTypeConfiguration<Passenger>
{
    public PassengerConfiguration(string tableName, string schema)
        : base(tableName, schema)
    { 
    }

    public void Configure(EntityTypeBuilder<Passenger> builder)
    {
        builder.ToTable(TableName, Schema);

        builder.HasKey(x => x.Id);

        builder.Property(e => e.Id).HasColumnName("passenger_id");
        builder.Property(e => e.BookingId).HasColumnName("booking_id");
        builder.Property(e => e.BookingRef).HasColumnName("booking_ref");
        builder.Property(e => e.PassengerNo).HasColumnName("passenger_no");
        builder.Property(e => e.FirstName).HasColumnName("first_name");
        builder.Property(e => e.LastName).HasColumnName("last_name");
        builder.Property(e => e.AccountId).HasColumnName("account_id");
        builder.Property(e => e.Age).HasColumnName("age");
        builder.Property(e => e.UpdateTs).HasColumnName("update_ts");
        
        builder    
        .HasMany(p => p.BoardingPasses)
        .WithOne(b => b.Passenger)
        .HasForeignKey(p => p.PassengerId)
        .IsRequired(false);
    }
}
