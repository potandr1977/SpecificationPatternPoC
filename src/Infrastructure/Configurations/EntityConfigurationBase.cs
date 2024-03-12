namespace Domain.Core.Configurations;

internal class EntityConfigurationBase
{
    protected string TableName { get; }
    protected string Schema { get; }

    public EntityConfigurationBase(string tableName, string schema)
    { 
        TableName = string .IsNullOrEmpty(tableName) 
            ? throw new ArgumentException(nameof(tableName)) 
            : tableName;

        Schema = string.IsNullOrEmpty(schema)
            ? throw new ArgumentException(nameof(schema))
            : schema;
    }
}
