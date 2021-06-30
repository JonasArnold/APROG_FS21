using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SW10.ThreadServerUebung
{
  public class Server
  {
    public Server()
    {
      Thread t = new Thread(Listener);
      t.Start();
    }

    private void Listener()
    {
      IPEndPoint ep = new IPEndPoint(IPAddress.Any, 12345);
      TcpListener listener = new TcpListener(ep);
      listener.Start();
      while (true)
      {
        TcpClient client = listener.AcceptTcpClient();
        StreamWriter sw = new StreamWriter(client.GetStream());
        for (int i = 0; i < 10; i++)
        {
          Console.Write(".");
          Thread.Sleep(200);
        }
        sw.WriteLine(DateTime.Now.ToLongTimeString());
        sw.Flush();
        client.Close();
      }
    }
  }
}