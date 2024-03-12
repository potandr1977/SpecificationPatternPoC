using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Core.Configurations;

internal class PhoneConfiguration :
    EntityConfigurationBase, IEntityTypeConfiguration<Phone>
{
    public PhoneConfiguration(string tableName, string schema)
        : base(tableName, schema)
    { 
    }

    public void Configure(EntityTypeBuilder<Phone> builder)
    {
        builder.ToTable(TableName, Schema);

        builder.HasKey(x => x.Id);

        builder.Property(e => e.Id).HasColumnName("phone_id");
        builder.Property(e => e.PrimaryPhone).HasColumnName("primary_phone");
        builder.Property(e => e.PhoneType).HasColumnName("phone_type");
        builder.Property(e => e.AccountId).HasColumnName("account_id");
        builder.Property(e => e.PhoneValue).HasColumnName("phone");
        builder.Property(e => e.UpdateTs).HasColumnName("update_ts");
    }
}
