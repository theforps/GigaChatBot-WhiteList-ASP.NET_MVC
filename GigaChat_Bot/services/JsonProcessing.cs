using System.Text.Json.Nodes;
using GigaChat_Bot.resourses;
using Newtonsoft.Json.Linq;

namespace GigaChat_Bot.services;

public class JsonProcessing
{
    public string GetAnswer(string response)
    {
        JObject jsonObj = JObject.Parse(response);
        JArray myArray = (JArray)jsonObj["choices"]!;
        JObject finalObj = JObject.Parse(myArray[0].ToString())!;

        string answer = finalObj!["message"]!["content"]!.ToString();

        return answer;
    }
    
    public string GetAccesToken(string response)
    {
        JsonNode jsonObj = JsonNode.Parse(response)!;

        var result = jsonObj!["access_token"]!.ToString();

        return result;
    }
    
    public string GetBanMessage()
    {
        string text = Consts.JsonObj["ban"]!.ToString();

        return text;
    }
}