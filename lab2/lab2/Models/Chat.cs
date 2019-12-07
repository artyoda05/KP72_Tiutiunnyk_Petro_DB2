using System;
using System.Collections.Generic;
using System.Text;

namespace lab2.Models
{
    class Chat
    {
        public long Id { get; set; }
        public string Tag { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }

        public Chat(long id, string tag, string name, string bio = null)
        {
            Id = id;
            Tag = tag;
            Name = name;
            Bio = bio;
        }
    }
}
