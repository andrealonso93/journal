using System.ComponentModel.DataAnnotations;

namespace Journal.Domain.Models;

public class User
{
    public int? Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [MaxLength(9)]
    [DataType(DataType.PostalCode)]
    public string ZipCode { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}
