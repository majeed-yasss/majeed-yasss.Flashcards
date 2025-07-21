namespace DataObjects;
public class StudySession
{
    public int Id { get; set; }
    public int StackID { get; set; }
    public int Answerd { get; set; }
    public int Total { get; set; }
    public DateTime Started { get; set; }
    public DateTime Ended { get; set; }
}
