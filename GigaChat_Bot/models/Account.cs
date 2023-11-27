using System.ComponentModel.DataAnnotations;

namespace GigaChat_Bot.models;

public class Account
{
    [Key]
    public int Id { get; set; }

    public string Login { get; set; }
    public string Password { get; set; }
    public User user { get; set; }
}
