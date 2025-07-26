namespace DataObjects;

public class Flashcard : ITable
{
    public Flashcard(int stackId, string front, string back) :
        this(0, stackId, front, back)
    { }
    public Flashcard(int id, int stackId, string front, string back)
    {
        Id = id;
        StackId = stackId;
        Front = front;
        Back = back;
    }

    public static string TableName => "stack_flashcards";
    public int Id { get; set; }
    public int StackId { get; set; }
    public string Front { get; set; }
    public string Back { get; set; }
    public override string ToString() =>
        $"Front: {Front}\n  Back: {Back}";
}
