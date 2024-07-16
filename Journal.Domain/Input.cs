namespace Journal.Domain;

/// <summary>
/// Represents a journal input object to be stored
/// </summary>
public class Input
{
    /// <summary>
    /// Journal input constructor
    /// </summary>
    /// <param name="inputText">Text input of the journal</param>
    public Input(string inputText)
    {
        InsertionDateTime = DateTime.Now;
        InputText = inputText;
    }

    public int Id { get; set; }
    public DateTime InsertionDateTime { get; set; }
    public DateTime? UdateDateTime { get; set; }
    public string InputText { get; set; }
}
