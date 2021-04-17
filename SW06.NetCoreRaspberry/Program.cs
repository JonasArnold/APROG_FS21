using System;
using System.Threading;
using Utils;

namespace SW06.NetCoreRaspberry
{
  class Program
  {
    static void Main(string[] args)
    {
      // waiting for debugger
      Util.WaitForDebugger();

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
