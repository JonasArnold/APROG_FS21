using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Utils.Network
{
  public class Tcp_Client
  {
    private const string hostname = "hostname";
    private const int port = 1500;
    private IPAddress ipAddress;
    private IPEndPoint ipEndPoint;
    private TcpClient tcpClient;
    private Thread listeningThread;

    public Tcp_Client()
    {
      // configure tcp client
      this.ipAddress = Dns.GetHostEntry(hostname).AddressList[0];
      this.ipEndPoint = new IPEndPoint(this.ipAddress, port);
      this.tcpClient = new TcpClient();

      // connect
      this.tcpClient.Connect(this.ipEndPoint);

      // start listening
      this.listeningThread = new Thread(() => this.Listen());
      this.listeningThread.Name = "Tcp Client Listener";
      this.listeningThread.Start();
    }

    private void Listen()
    {
      NetworkStream networkStream = this.tcpClient.GetStream();

      // read
      StreamReader sr = new StreamReader(networkStream);
      string s = sr.ReadLine();  // reads one line until CR LF
      // string s = sr.ReadToEnd(); // reads whole text

      // close connection
      tcpClient.Close();
    }
  }
}
