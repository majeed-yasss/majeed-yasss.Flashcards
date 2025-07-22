namespace DataObjects;

public class Flashcard : ITable
{
    public static string TableName => "stack_flashcards";
    public int Id { get; set; }
    public int StackID { get; set; }
    public string Front { get; set; }
    public string Back { get; set; }
}
