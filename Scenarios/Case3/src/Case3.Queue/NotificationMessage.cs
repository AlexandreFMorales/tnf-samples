﻿using Tnf.Bus.Client;

namespace Case3.Queue.Messages
{
    /// <summary>
    /// Representa a mensagem que será enviada/recebida
    /// </summary>
    public class NotificationMessage : Message
    {
        public string Value { get; set; }

        // Construtor vazio para que a mensagem possa ser deserializada
        public NotificationMessage()
        {
        }

        public NotificationMessage(string value)
        {
            Value = value;
        }
    }
}
