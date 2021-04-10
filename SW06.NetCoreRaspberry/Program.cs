using System;
using System.Threading;

namespace SW06.NetCoreRaspberry
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Waiting for debugger...");

      // waiting for debugger
      while(System.Diagnostics.Debugger.IsAttached == false)
      {
        Thread.Sleep(100);
      }

      // debugger attached
      Console.WriteLine("Debugger found.");
      for (int i = 0; i < 100; i++)
      {
        Console.Write(".");
        Thread.Sleep(1000);
      }
    }
  }
}
