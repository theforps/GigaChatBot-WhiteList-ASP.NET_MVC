using System.ComponentModel.DataAnnotations;

namespace YandexGPT_bot.models;

public class User
{

    [Key]
    public int Id { get; set; }
    public string? Username { get; set; }
    public bool Ban { get; set; } = false;
}