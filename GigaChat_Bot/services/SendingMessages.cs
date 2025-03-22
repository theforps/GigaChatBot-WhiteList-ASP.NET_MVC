using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using GigaChat_Bot.resourses;
using GigaChat_Bot.repositories;

namespace GigaChat_Bot.services;

public class SendingMessages
{
    private BaseRepository baseRepository = new();


    public SendPhotoRequest HelloMessage(long chatId)
    {
        var photoRequest = new SendPhotoRequest(chatId, InputFile.FromUri(MesInfo.HelloImg))
        {
            Caption = MesInfo.Hello
        };

        var button = new InlineKeyboardButton("Начать")
        {
            CallbackData = "start"
        };

        photoRequest.ReplyMarkup = new InlineKeyboardMarkup(button);
        
        return photoRequest;
    }

    public static SendMessageRequest SimpleMessage(long chatId)
    {
        var messageRequest = new SendMessageRequest(chatId, MesInfo.StartMessage);

        return messageRequest;
    }

    public static async Task SendTextMessage(long chatId, string text, ITelegramBotClient client)
    {
        await client.SendTextMessageAsync(chatId, text);
    }
}