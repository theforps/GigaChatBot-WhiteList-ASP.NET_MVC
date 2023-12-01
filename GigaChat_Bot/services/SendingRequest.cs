using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json.Nodes;
using GigaChat_Bot.models;
using GigaChat_Bot.repositories.impl;
using GigaChat_Bot.repositories.interfaces;
using GigaChat_Bot.resourses;


namespace GigaChat_Bot.services;

public class SendingRequest
{
    private IUserRepository _userRepository;
    private IHistoryRepository _historyRepository;
    private Mapper _mapper;
    private List<Message> messages;

    public SendingRequest()
    {
        messages = new();
        _mapper = new Mapper();
        _historyRepository = new HistoryRepository();
        _userRepository = new UserRepository();
    }
    
    public async Task<string> GetAnswer(int userId, string text, string api)
    {
        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        var httpClient = new HttpClient(clientHandler);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {api}");

        List<History> history = await _historyRepository.getHistory(userId);
        User user = await _userRepository.getUserById(userId);

        if (history != null && history.Count > 0)
        {
            messages = _mapper.MapToListMessages(history);
        }
        else
        {
            History settings = new History()
            {
                Message = Consts.JsonObj!["settings"]!.ToString(),
                Role = "system",
                User = user
            };

            await _historyRepository.addHistory(settings);
        }
        
        Message message = new() { content = $"{text}" };
        messages.Add(message);

        History newHistory = _mapper.MapToHistory(message, user);
        await _historyRepository.addHistory(newHistory);

        Request request = new() { messages = messages };


        string payload = JsonConvert.SerializeObject(request);
        StringContent content = new StringContent(payload, Encoding.UTF8, "application/json");

        string response = await httpClient.PostAsync(Consts.GptUrl, content).Result.Content.ReadAsStringAsync();

        JObject jsonObj = JObject.Parse(response);
        JArray myArray = (JArray)jsonObj["choices"]!;
        JObject finalObj = JObject.Parse(myArray[0].ToString())!;

        string answer = finalObj!["message"]!["content"]!.ToString();

        History botMessage = new History()
        {
            Message = answer,
            Role = "assistant",
            User = user
        };

        await _historyRepository.addHistory(botMessage);

        return answer;
    }

    public async Task<string> GetApi()
    {
        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

        HttpClient httpClient = new HttpClient(clientHandler);

        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Consts.GptToken}");
        httpClient.DefaultRequestHeaders.Add("RqUID", $"{Consts.GptUid}");

        var data = new[]
        {
            new KeyValuePair<string, string>("scope", "GIGACHAT_API_PERS"),
        };

        string response = await httpClient.PostAsync(Consts.GptUrlReg, 
            new FormUrlEncodedContent(data)).Result.Content.ReadAsStringAsync();

        JsonNode jsonObj = JsonNode.Parse(response)!;

        var result = jsonObj!["access_token"]!.ToString();

        return result;
    }
}
