using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Utils.Network
{
  public class Udp_Client
  {
    private const int port = 1500;
    private IPAddress ipAddress = IPAddress.Parse("192.168.1.1");
    private IPEndPoint ipEndPoint;
    private UdpClient udpClient;

    public Udp_Client()
    {
      this.ipEndPoint = new IPEndPoint(this.ipAddress, port);
      this.udpClient = new UdpClient();

      // connect to server
      this.udpClient.Connect(this.ipEndPoint);

      // transmit byte array to the server
      byte[] data = Encoding.ASCII.GetBytes("Hello");
      this.udpClient.Send(data, data.Length);

      // close the connection
      this.udpClient.Close();
    }
  }
}
