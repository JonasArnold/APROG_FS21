using System.IO;
using System.Net.Sockets;

namespace SW09.Networking
{
  internal class TcpDayTimeClient
  {
    private const string serverHostname = "time.nist.gov";
    private const int serverPort = 13;

    private readonly TcpClient tcpClient;
    private Socket socket;

    public TcpDayTimeClient()
    {
      this.tcpClient = new();
    }
    public void Connect()
    {
      this.tcpClient.Connect(serverHostname, serverPort);
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
        // read as lines until it is not empty anymore
        do
        {
          line = sr.ReadLine();
        } while (line == "");
      }
      return line;
    }

    public void Close()
    {
      this.tcpClient.Close();
    }
  }
}
