using GigaChat_Bot.repositories;
using GigaChat_Bot.resourses;
using Telegram.Bot.Types;
using User = GigaChat_Bot.models.User;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using System.Diagnostics;

namespace GigaChat_Bot.services;

public class Navigator
{
    private SendingMessages sendingMessages;
    private Dictionary<long, bool> users;
    private BaseRepository baseRepository;
    private SendingRequest sendingRequest;

    public Navigator()
    {
        sendingMessages = new SendingMessages();
        users = new Dictionary<long, bool>();
        sendingRequest = new SendingRequest();

        var client = new TelegramBotClient(Consts.BotApi);
        client.StartReceiving(Update, Error, new ReceiverOptions
        {
            AllowedUpdates = new[]
            {
                UpdateType.Message,
                UpdateType.CallbackQuery
            },
                ThrowPendingUpdates = true
            }
        );
    }

    private async Task Update(ITelegramBotClient client, Update update, CancellationToken token)
    {
        try
        {
            var message = update.Message;
            long chatId;

            baseRepository = new BaseRepository();

            if (message != null)
            {
                chatId = message.Chat.Id;

                var user = await baseRepository.getUserByUsername(message.From!.Username);

                if (user == null)
                {
                    var newUser = new User
                    {
                        Username = message.From.Username!,
                        ChatId = chatId

                    };

                    await baseRepository.addUser(newUser);

                    user = await baseRepository.getUserByUsername(message.From!.Username);

                    Console.WriteLine(user.Username);
                }
                else if (user.Ban)
                {
                    string text = JsonProcessing.GetBanMessage();

                    await SendingMessages.SendTextMessage(chatId, text, client);

                    return;
                }

                if (message.Text != null)
                {
                    if (message.Text.Equals("/start"))
                    {
                        await client.MakeRequestAsync(sendingMessages.HelloMessage(chatId), token);
                    }
                    else if (message.Text.Equals("/restart"))
                    {
                        await baseRepository.clearHistory(user.Id);

                        var result = SendingMessages.SimpleMessage(chatId);
                        
                        await client.MakeRequestAsync(result, token);

                        users.TryGetValue(chatId, out var check);
                        
                        if(!check)
                            users.Add(chatId, true);
                    }
                    else if (message.Text != null)
                    {
                        users.TryGetValue(chatId, out var canSend);

                        if (canSend)
                        {
                            var apiKey = await sendingRequest.GetApi();

                            var result = await sendingRequest.GetAnswer(user.Id, message.Text, apiKey);

                            await SendingMessages.SendTextMessage(chatId, result, client);
                        }
                    }
                }
            }
            else if (update.CallbackQuery != null)
            {
                chatId = update.CallbackQuery.From.Id;

                if (update.CallbackQuery!.Data == "start")
                {
                    await client.MakeRequestAsync(SendingMessages.SimpleMessage(chatId), token);

                    users.Add(chatId, true);
                }

            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private async Task Error(ITelegramBotClient client, Exception ex, CancellationToken token)
    {
        Debug.WriteLine(ex.Message);
    }
}

