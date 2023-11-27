using YandexGPT_bot.repositories.impl;
using YandexGPT_bot.repositories.interfaces;
using YandexGPT_bot.resourses;
using Telegram.Bot.Types;
using User = YandexGPT_bot.models.User;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using System.Diagnostics;

namespace YandexGPT_bot.services;

public class TelegramBot
{
    private SendingMessages sendingMessages;
    private Dictionary<long, bool> users;
    private IUserRepository _userRepository;
    private SendingRequest sendingRequest;

    public TelegramBot()
    {
        sendingMessages = new SendingMessages();
        users = new Dictionary<long, bool>();
        _userRepository = new UserRepository();
        sendingRequest = new SendingRequest();

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
        var message = update.Message;
        long chatId;

        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));

        if (message != null)
        {
            chatId = message.Chat.Id;

            User user = await _userRepository.getUserByUsername(message.From!.Username);

            if (user == null)
            {
                User newUser = new User() { Username = message.From.Username! };

                await _userRepository.addUser(newUser);

                user = await _userRepository.getUserByUsername(message.From!.Username);
            }
            else if (user.Ban)
            {
                var text = Consts.JsonObj["ban"]!.ToString();

                await sendingMessages.SendTextMessage(chatId, text, client);

                return;
            }

            if (message.Text != null)
            {
                if (message.Text.Equals("/start"))
                {
                    await client.MakeRequestAsync(sendingMessages.HelloMessage(chatId));
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
                await client.MakeRequestAsync(sendingMessages.SimpleMessage(chatId));
                users.Add(chatId, true);
            }

        }
    }

    private async Task Error(ITelegramBotClient client, Exception ex, CancellationToken token)
    {
        Debug.WriteLine(ex.Message);
    }
}

