﻿using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace lab2.Models
{
    class User
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }

        public User(long id, string login, string name, string bio = null)
        {
            Id = id;
            Login = login;
            Name = name;
            Bio = bio;
        }
    }
}
