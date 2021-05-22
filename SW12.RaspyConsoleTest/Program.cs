using System;
using System.IO.Ports;

namespace SW12.RaspyConsoleTest
{
  class Program
  {
    static void Main(string[] args)
    {
      SerialPort myPort = new SerialPort("/dev/ttyS0", 115200);
      if (myPort == null)
      {
        Console.WriteLine("No serial port /dev/ttyS0");
        return;
      }

      myPort.Open();

      if (!myPort.IsOpen)
      {
        Console.WriteLine("Error opening serial port");
        return;
      }

      Console.WriteLine("Port open");

      //myPort.ReadTimeout = 10000; // optional
      myPort.NewLine = "\r\n";
      while (!Console.KeyAvailable)
      {
        try
        {
          string readed = myPort.ReadLine();
          myPort.WriteLine(readed);
          Console.Write(readed);
        }
        catch (Exception ex)
        {
          Console.WriteLine($"Error: {ex.Message}");
        }
      }
    }
  }
}
