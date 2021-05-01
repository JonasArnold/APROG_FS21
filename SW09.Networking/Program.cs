using System;

namespace SW09.Networking
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Starting TCP DayTime Client...");

      TcpDayTimeClient tcpDayTimeClient = new TcpDayTimeClient();
      tcpDayTimeClient.Connect();
      Console.WriteLine("Connected.");

      Console.WriteLine($"ReadLine: {tcpDayTimeClient.ReadLine()}");

      tcpDayTimeClient.Close();
      Console.WriteLine("Closed Connection.");
    }
  }
}
