namespace lab2.Models
{
    class Chat
    {
        public long Id { get; set; }
        public string Tag { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }

        public Chat(long id, string tag = null, string name = null, string bio = null)
        {
            Id = id;
            Tag = tag;
            Name = name;
            Bio = bio;
        }
    }
}
