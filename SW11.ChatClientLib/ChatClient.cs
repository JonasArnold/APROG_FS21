using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SW11.ChatClientLib
{
  public class ChatClient : IChatClient
  {
    private readonly TcpClient client;
    private Thread listenerThread;
    private string username;
    private bool cancelled;

    public event EventHandler<MessageReceivedEventArgs> MessageReceived;
    public event EventHandler<UserUpdateEventArgs> UserUpdate;

    public ChatClient(string host, int port = 5555)
    {
      this.client = new TcpClient(host, port);
      this.listenerThread = new Thread(Listener);
      this.listenerThread.Start();
    }

    public void Disconnect()
    {
      this.SendTcpMessage($"chat/unregister:{this.username}");
      this.cancelled = true;
      this.client?.Close();
    }

    public bool Login(string user)
    {
      this.username = user;
      this.cancelled = false;
      this.SendTcpMessage($"chat/register:{this.username}");
      return true;
    }

    public void SendMessage(string toUser, string message)
    {
      this.SendTcpMessage($"chat/{toUser}:{message}");
    }

    private void SendTcpMessage(string message)
    {
      StreamWriter sw = new StreamWriter(this.client.GetStream());
      sw.WriteLine(message);
      sw.Flush();   // make sure data is sent
    }

    private void Listener()
    {
      while (cancelled == false)
      {
        StreamReader sr = new StreamReader(this.client.GetStream());
        string receivedLine = sr.ReadLine();
        Console.WriteLine("Client has data received: " + receivedLine);
        this.HandleNewData(receivedLine);
      }
    }

    private void HandleNewData(string receivedLine)
    {
      if (receivedLine.StartsWith("chat/"))
      {
        // split header and data, read out command
        var header = receivedLine.Split(':')[0];
        var data = receivedLine.Split(':')[1];
        string command = header.Split('/')[1];

        // Handle User Update
        if(command == "users")
        {
          // split single users
          var users = data.Split(';');
          this.UserUpdate?.Invoke(this, new UserUpdateEventArgs(users));
        }
        // received message from some user
        else
        {
          this.MessageReceived?.Invoke(this, new MessageReceivedEventArgs(data));
        }

      }
    }
  }
}
