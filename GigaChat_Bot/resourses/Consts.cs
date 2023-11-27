using System.Text.Json.Nodes;

namespace GigaChat_Bot.resourses;

public static class Consts
{
    public const string BotApi = "6310507674:AAGrf9d3lFQmfUS3KjwvhUkc9HfLQ0wOxjE";

    public const string GptToken = "Njk3ZDBiY2EtNmU1Zi00NmFlLThjNTQtMjVkY2I3NmNmYjhlOjY2ZGRiODlkLTQ2N2UtNGNmNS05YjA5LWYyNmYzOWU3ZjZlYg==";
    public const string GptUid = "6f0b1291-c7f3-43c6-bb2e-9f3efb2dc98e";

    public const string GptUrl = "https://gigachat.devices.sberbank.ru/api/v1/chat/completions";
    public const string GptUrlReg = "https://ngw.devices.sberbank.ru:9443/api/v2/oauth";

    public static readonly JsonNode JsonObj = JsonNode.Parse(File.ReadAllText("Dictionary.json"))!;
}