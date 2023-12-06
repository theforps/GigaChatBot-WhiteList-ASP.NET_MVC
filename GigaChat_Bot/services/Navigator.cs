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
    private JsonProcessing _jsonProcessing;

    public Navigator()
    {
        _jsonProcessing = new();
        sendingMessages = new();
        users = new();
        sendingRequest = new();

        TelegramBotClient client = new TelegramBotClient(Consts.BotApi);
        client.StartReceiving(Update, Error, new ReceiverOptions()
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

            baseRepository = new();

            if (message != null)
            {
                chatId = message.Chat.Id;

                User user = await baseRepository.getUserByUsername(message.From!.Username);

                if (user == null)
                {
                    User newUser = new User()
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
                    string text = _jsonProcessing.GetBanMessage();

                    await sendingMessages.SendTextMessage(chatId, text, client);

                    return;
                }

                if (message.Text != null)
                {
                    if (message.Text.Equals("/start"))
                    {
                        await client.MakeRequestAsync(sendingMessages.HelloMessage(chatId));
                    }
                    else if (message.Text.Equals("/restart"))
                    {
                        await baseRepository.clearHistory(user.Id);
                        
                        await client.MakeRequestAsync(await sendingMessages.SimpleMessage(chatId));

                        users.TryGetValue(chatId, out bool check);
                        
                        if(!check)
                            users.Add(chatId, true);
                    }
                    else if (message.Text != null)
                    {
                        users.TryGetValue(chatId, out bool canSend);

                        if (canSend)
                        {
                            var apiKey = await sendingRequest.GetApi();

                            var result = await sendingRequest.GetAnswer(user.Id, message.Text, apiKey);

                            await sendingMessages.SendTextMessage(chatId, result, client);
                        }
                    }
                }
            }
            else if (update.CallbackQuery != null)
            {
                chatId = update.CallbackQuery.From.Id;

                if (update.CallbackQuery!.Data == "start")
                {
                    await client.MakeRequestAsync(await sendingMessages.SimpleMessage(chatId));

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

