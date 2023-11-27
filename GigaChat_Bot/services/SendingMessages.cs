using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using YandexGPT_bot.resourses;

namespace YandexGPT_bot.services;

public class SendingMessages
{

    public SendPhotoRequest HelloMessage(long chatId)
    {
        SendPhotoRequest photoRequest = new SendPhotoRequest(chatId, InputFile.FromUri(Consts.JsonObj!["helloImg"]!.ToString()));
                    
        photoRequest.Caption = Consts.JsonObj!["hello"]!.ToString();
                    
        InlineKeyboardButton button = new InlineKeyboardButton("Начать");
        button.CallbackData = "start";
        
        photoRequest.ReplyMarkup = new InlineKeyboardMarkup(button);
        
        return photoRequest;
    }

    public SendMessageRequest SimpleMessage(long chatId)
    {
        SendMessageRequest messageRequest = new SendMessageRequest(chatId, Consts.JsonObj!["startMessage"]!.ToString());

        return messageRequest;
    }

    public async Task SendTextMessage(long chatId, string text, ITelegramBotClient client)
    {
        await client.SendTextMessageAsync(chatId, text);
    }
}