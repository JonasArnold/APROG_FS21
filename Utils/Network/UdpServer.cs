using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Utils.Network
{
  public class UdpServer
  {
    private const int port = 1500;
    private IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, port);
    private UdpClient udpClient;

    public UdpServer()
    {
      // configure listener
      this.udpClient = new UdpClient(this.ipEndPoint);

      // start listening and wait for UDP packet
      Console.WriteLine("Waiting for client...");
      byte[] data = this.udpClient.Receive(ref this.ipEndPoint);

      // process revieved data
      Console.WriteLine($"Message recieved from {this.ipEndPoint}:");
      Console.WriteLine(Encoding.ASCII.GetString(data, 0, data.Length));

      // "close the connection"
      this.udpClient.Close();
    }
  }
}
