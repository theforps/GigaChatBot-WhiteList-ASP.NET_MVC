using System.Text.Json.Nodes;
using GigaChat_Bot.resourses;
using Newtonsoft.Json.Linq;

namespace GigaChat_Bot.services;

public static class JsonProcessing
{
    public static string GetAnswer(string response)
    {
        var jsonObj = JObject.Parse(response);
        var myArray = (JArray)jsonObj["choices"]!;
        var finalObj = JObject.Parse(myArray[0].ToString())!;

        var answer = finalObj["message"]!["content"]!.ToString();

        return answer;
    }
    
    public static string GetAccessToken(string response)
    {
        var jsonObj = JsonNode.Parse(response)!;

        var result = jsonObj["access_token"]!.ToString();

        return result;
    }
    
    public static string GetBanMessage()
    {
        var text = MesInfo.Ban;

        return text;
    }
}