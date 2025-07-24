namespace DataObjects;

public interface ITable
{
    public static abstract string TableName { get; }
    // maybe not ideal to assume that all tables have an Id, but fits our case
    // (could be replaced with somthing like: GetKeyName() & GetKeyValue())
    public abstract int Id { get; set; }
}
