namespace GigaChat_Bot.models;

public class Request
{

    public string model { get; set; } = "GigaChat:latest";
    public List<Message> messages { get; set; } = new();
    public double temperature { get; set; } = 0.9;
}