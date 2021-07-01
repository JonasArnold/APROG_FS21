using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Utils.Network
{
  public class TcpServer
  {
    private const int port = 1500;
    private IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, port);
    private TcpListener tcpListener;
    private Thread handlerThread;

    public TcpServer()
    {
      // configure listener
      this.tcpListener = new TcpListener(this.ipEndPoint);

      // start listening (open port)
      this.tcpListener.Start();

      // start handling
      this.handlerThread = new Thread(() => this.Handle());
      this.handlerThread.Name = "Tcp Server Handler";
      this.handlerThread.Start();
    }

    private void Handle()
    {
      // waiting for connection
      TcpClient tcpClient = this.tcpListener.AcceptTcpClient();
      Console.WriteLine($"Client connected: {tcpClient.Client.RemoteEndPoint}");

      // Send data
      NetworkStream networkStream = tcpClient.GetStream();
      StreamWriter sw = new StreamWriter(networkStream);
      sw.WriteLine("...");

      // Close connection
      sw.Flush();
      sw.Close();
      tcpClient.Close();
    }
  }

}
