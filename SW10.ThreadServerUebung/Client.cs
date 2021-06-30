using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace SW10.ThreadServerUebung
{
  public class Client
  {
    public void GetTime()
    {
      TcpClient client = new TcpClient("127.0.0.1", 12345);
      Console.WriteLine("Client Connected");
      StreamReader sr = new StreamReader(client.GetStream());
      Console.WriteLine("Client has data received: " + sr.ReadLine());
      client.Close();
      Thread.Sleep(2000);
    }
  }
}