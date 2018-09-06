﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Notifications.Config
{
    public class WhatsAppConfig : ConfigNotification
    {
        public String UrlFile { get; set; }
        public TypeSend TypeSend { get; set; }
    }

    public enum TypeSend
    {
        Text, TextImage, Image,
        Media
    }
}
