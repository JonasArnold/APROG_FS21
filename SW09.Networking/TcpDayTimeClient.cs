using System.IO;
using System.Net.Sockets;

namespace SW09.Networking
{
  internal class TcpDayTimeClient
  {
    private const int serverPort = 13;

    private readonly TcpClient tcpClient;
    private Socket socket;

    public TcpDayTimeClient()
    {
      this.tcpClient = new();
    }
    public void Connect(string hostname)
    {
      this.tcpClient.Connect(hostname, serverPort);
      this.socket = this.tcpClient.Client;
    }

    public string ReadLine()
    {
      if(this.socket == null)
      {
        return string.Empty;
      }

      string line = "";
      using(StreamReader sr = new(tcpClient.GetStream()))
      {
        // read to end of stream
        line = sr.ReadToEnd();
      }
      return line;
    }

    public void Close()
    {
      this.tcpClient.Close();
    }
  }
}
