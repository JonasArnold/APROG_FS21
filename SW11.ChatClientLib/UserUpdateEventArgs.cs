using System;

namespace SW11.ChatClientLib
{
    public class UserUpdateEventArgs : EventArgs
    {
        public UserUpdateEventArgs(string[] users)
        {
            Users = users;
        }

        public string[] Users { get; }
    }
}
