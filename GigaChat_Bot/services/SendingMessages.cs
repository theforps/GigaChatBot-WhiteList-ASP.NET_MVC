using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using GigaChat_Bot.resourses;
using GigaChat_Bot.repositories;

namespace GigaChat_Bot.services;

public class SendingMessages
{
    private BaseRepository baseRepository;
    public SendingMessages()
    {
        baseRepository = new();
    }



    public SendPhotoRequest HelloMessage(long chatId)
    {
        SendPhotoRequest photoRequest = new SendPhotoRequest(chatId, InputFile.FromUri(Consts.JsonObj!["helloImg"]!.ToString()));
                    
        photoRequest.Caption = Consts.JsonObj!["hello"]!.ToString();
                    
        InlineKeyboardButton button = new InlineKeyboardButton("Начать");
        button.CallbackData = "start";
        
        photoRequest.ReplyMarkup = new InlineKeyboardMarkup(button);
        
        return photoRequest;
    }

    public async Task<SendMessageRequest> SimpleMessage(long chatId, bool restart)
    {
        if(restart)
        {
            await baseRepository.clearHistory(chatId);
        }

        SendMessageRequest messageRequest = new SendMessageRequest(chatId, Consts.JsonObj!["startMessage"]!.ToString());

        return messageRequest;
    }

    public async Task SendTextMessage(long chatId, string text, ITelegramBotClient client)
    {
        await client.SendTextMessageAsync(chatId, text);
    }
}