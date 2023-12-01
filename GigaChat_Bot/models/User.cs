using System.ComponentModel.DataAnnotations;

namespace GigaChat_Bot.models;

public class User
{
    [Key]
    public int Id { get; set; }
    public string Username { get; set; }
    public string? Password { get; set; }
    public bool Ban { get; set; } = false;
    public long ChatId { get; set; }
}