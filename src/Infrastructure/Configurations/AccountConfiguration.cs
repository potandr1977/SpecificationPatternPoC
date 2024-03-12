using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Core.Configurations;

internal class AccountConfiguration :
    EntityConfigurationBase, IEntityTypeConfiguration<Account>
{
    public AccountConfiguration(string tableName, string schema)
        : base(tableName, schema)
    { 
    }

    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable(TableName, Schema);

        builder.HasKey(x => x.Id);

        builder.Property(e => e.Id).HasColumnName("account_id");
        builder.Property(e => e.Login).HasColumnName("login");
        builder.Property(e => e.FirstName).HasColumnName("first_name");
        builder.Property(e => e.LastName).HasColumnName("last_name");
        builder.Property(e => e.FrequentFlyerId).HasColumnName("frequent_flyer_id");
        builder.Property(e => e.UpdateTs).HasColumnName("update_ts");

        
        builder
           .HasMany(a => a.Passengers)
           .WithOne(p => p.Account)
           .HasForeignKey(p => p.AccountId)
           .IsRequired(false);

        builder
          .HasMany(a => a.Phones)
          .WithOne(p => p.Account)
          .HasForeignKey(p => p.AccountId)
          .IsRequired(false);

    }
}
