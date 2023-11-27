using GigaChat_Bot.models;

namespace GigaChat_Bot.services;

public class Mapper
{
    public List<Message> MapToListMessages(List<History> histories)
    {
        var messages = new List<Message>();

        foreach (var x in histories)
        {
            Message message = new Message()
            {
                content = x.Message,
                role = x.Role
            };

            messages.Add(message);
        }

        return messages;
    }
    
    public History MapToHistory(Message message, User user)
    {
        var history = new History()
        {
            Message = message.content,
            Role = message.role,
            User = user
        };
        
        return history;
    }
}
