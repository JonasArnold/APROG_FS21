using System;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;
using Unosquare.WiringPi;

namespace SW13.I2C
{
  class Program
  {
    static void Main(string[] args)
    {
      Utils.Util.WaitForDebugger();

      Pi.Init<BootstrapWiringPi>();
      II2CDevice tempSensor = Pi.I2C.AddDevice(0x44);

      byte[] command = new byte[2] { 0x2C, 0x06 };

      int result = tempSensor.Write(command, command.Length);
      Console.WriteLine("Result 1: " + result);
      System.Threading.Thread.Sleep(100);

      byte[] data = new byte[6];
      result = tempSensor.Read(data, data.Length);
      Console.WriteLine("Result 1: " + result);

      foreach (byte b in data) Console.Write(b + " ");

      UInt16 t = (UInt16)((data[0] << 8) | data[1]);
      float temperature = (175f * t) / 0xffff - 45;
      Console.WriteLine("temp:" + temperature);
    }
  }
}
