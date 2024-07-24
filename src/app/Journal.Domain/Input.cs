using System.ComponentModel.DataAnnotations;

namespace Journal.Domain;

/// <summary>
/// Represents a journal input object to be stored
/// </summary>
public class Input
{

    /// <summary>
    /// New journal builder
    /// </summary>
    /// <param name="inputText">Text input of the journal entry</param>
    public static Input BuildNewInput(string inputText)
    {
        return new Input
        {
            InsertionDateTime = DateTime.Now,
            InputText = inputText
        };
    }

    public int Id { get; set; }
    public DateTime InsertionDateTime { get; set; }
    public DateTime? UpdateDateTime { get; set; }

    [MaxLength(500)]
    public string InputText { get; set; } = string.Empty;
    public int UserId { get; set; }
    public User User { get; set; } = new();
}
