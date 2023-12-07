using System.Text.Json.Nodes;

namespace GigaChat_Bot.resourses;

public static class Consts
{
    public const string BotApi = "";

    public const string GptToken = "";
    public const string GptUid = "";

    public const string GptUrl = "https://gigachat.devices.sberbank.ru/api/v1/chat/completions";
    public const string GptUrlReg = "https://ngw.devices.sberbank.ru:9443/api/v2/oauth";

    public static readonly JsonNode JsonObj = JsonNode.Parse(File.ReadAllText("Dictionary.json"))!;
}
