namespace DataObjects;

public class Stack : ITable
{
    public static string TableName => "stacks";
    public int Id { get; set; }
    public string Name { get; set; }
}
