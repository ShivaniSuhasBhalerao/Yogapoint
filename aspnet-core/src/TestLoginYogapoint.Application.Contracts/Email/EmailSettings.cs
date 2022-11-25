﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TestLoginYogapoint.Email
{
    public class EmailSettings
    {
        public string EmailId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
    }
}
