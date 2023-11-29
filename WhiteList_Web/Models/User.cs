using System.ComponentModel.DataAnnotations;

namespace WhiteList_Web.models;

public class User
{

    [Key]
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public bool Ban { get; set; } = false;
    public long ChatId { get; set; }
}