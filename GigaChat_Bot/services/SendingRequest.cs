using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json.Nodes;
using GigaChat_Bot.models;
using GigaChat_Bot.repositories;
using GigaChat_Bot.resourses;


namespace GigaChat_Bot.services;

public class SendingRequest
{
    private BaseRepository baseRepository = new();
    private readonly Mapper _mapper = new();
    private List<Message> messages = new();

    public async Task<string> GetAnswer(int userId, string text, string api)
    {
        var clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        var httpClient = new HttpClient(clientHandler);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {api}");

        var history = await baseRepository.getHistory(userId);
        var user = await baseRepository.getUserById(userId);

        if (history != null && history.Count > 0)
        {
            messages = _mapper.MapToListMessages(history);
        }
        else
        {
            var settings = new History
            {
                Message = MesInfo.Settings,
                Role = "system",
                User = user
            };

            await baseRepository.addHistory(settings);
        }
        
        Message message = new() { content = $"{text}" };
        messages.Add(message);

        var newHistory = _mapper.MapToHistory(message, user);
        await baseRepository.addHistory(newHistory);

        Request request = new() { messages = messages };

        var payload = JsonConvert.SerializeObject(request);
        using var content = new StringContent(payload, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(Consts.GptUrl, content).Result.Content.ReadAsStringAsync();
        
        var answer = JsonProcessing.GetAnswer(response);

        var botMessage = new History
        {
            Message = answer,
            Role = "assistant",
            User = user
        };

        await baseRepository.addHistory(botMessage);

        return answer;
    }

    public async Task<string> GetApi()
    {
        var clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (
            sender, 
            cert, 
            chain, 
            sslPolicyErrors) => { return true; };

        var httpClient = new HttpClient(clientHandler);

        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Consts.GptToken}");
        httpClient.DefaultRequestHeaders.Add("RqUID", $"{Consts.GptUid}");

        var data = new[]
        {
            new KeyValuePair<string, string>("scope", "GIGACHAT_API_PERS"),
        };

        var response = await httpClient.PostAsync(Consts.GptUrlReg, 
            new FormUrlEncodedContent(data)).Result.Content.ReadAsStringAsync();

        var result = JsonProcessing.GetAccessToken(response);

        return result;
    }
}
