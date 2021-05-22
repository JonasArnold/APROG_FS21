using System;

namespace SW11.ChatClientLib
{
    public interface IChatClient
    {
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
        event EventHandler<UserUpdateEventArgs> UserUpdate;

        void SendMessage(string toUser, string message);

        bool Login(string user);

        void Disconnect();
    }
}
