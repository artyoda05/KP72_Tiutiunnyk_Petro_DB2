using System;

namespace lab2.Models
{
    class Message
    {
        public long Id { get; set; }
        public User User { get; set; }
        public Chat Chat { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }

        public Message(long id, User user, Chat chat, string text, DateTime time)
        {
            Id = id;
            User = user;
            Chat = chat;
            Text = text;
            Time = time;
        }
    }
}
