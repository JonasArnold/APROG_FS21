using System;

namespace SW11.ChatClientLib
{
    public class MessageReceivedEventArgs : EventArgs
    {

        public MessageReceivedEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
