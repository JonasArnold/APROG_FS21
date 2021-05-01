using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SW09.Networking
{
  internal class TcpDayTimeServer
  {
    private const int serverPort = 13;

    IPEndPoint ipEndPoint;
    TcpListener tcpListener;
    Thread listenerThread;
    bool cancellation;

    public TcpDayTimeServer()
    {
      this.ipEndPoint = new IPEndPoint(IPAddress.Any, serverPort);
    }

    public void StartListening()
    {
      this.cancellation = false; // reset flag
      this.tcpListener = new TcpListener(this.ipEndPoint);  //create
      this.tcpListener.Start();  // start listening (open port)

      this.listenerThread = new Thread(ListeningLoop);
      this.listenerThread.Name = "Server Listening Thread";
      this.listenerThread.Start();
    }

    public void StopListening()
    {
      cancellation = true;
      // wait for thread to end
      this.listenerThread?.Join(2000);
      this.listenerThread?.Interrupt();
      this.listenerThread = null;
      
      // dispose listener
      this.tcpListener.Stop();
      this.tcpListener = null;
    }

    private void ListeningLoop()
    {
      while(cancellation == false)
      {
        // waiting for connection
        TcpClient tcpClient = this.tcpListener.AcceptTcpClient();
        Console.WriteLine($"Client connected: {tcpClient.Client.RemoteEndPoint}");

        // send data
        using(StreamWriter sw = new(tcpClient.GetStream()))
        {
          sw.WriteLine(DateTime.Now);
          sw.Flush();
        } // closes itself

        // close connection
        tcpClient.Close();
      }
    }
  }
}
