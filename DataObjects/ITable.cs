namespace DataObjects;

public interface ITable
{
    public static abstract string TableName { get; }
    // maybe not ideal to assume that all tables have an Id, but fits our case
    public abstract int Id { get; set; }
}
