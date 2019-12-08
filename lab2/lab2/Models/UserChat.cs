namespace lab2.Models
{
    class UserChat
    {
        public long Id { get; set; }
        public User User { get; set; }
        public Chat Chat { get; set; }
        public bool IsAdmin { get; set; }

        public UserChat(long id, User user, Chat chat, bool isAdmin)
        {
            Id = id;
            User = user;
            Chat = chat;
            IsAdmin = isAdmin;
        }
    }
}
