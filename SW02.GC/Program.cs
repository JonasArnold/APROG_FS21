using System;
using System.Threading;

namespace SW02.GC
{
  class Program
  {
    private const int Cycles = 20;
    private const int ValuesPerCycle = 1024*1024; // 1 Megabyte

    static void Main(string[] args)
    {
      TestGC();
      System.GC.Collect();
      Console.WriteLine($"Memory after returning back to main method and calling GC.Collect() there \t {System.GC.GetTotalMemory(false) / 1024} KBytes");

      /*
       * This is new in .NET Core. In .NET Framework the GC Collect in the method would be enough to collect all the garbage from the null reference object 
       */
    }

    private static void TestGC()
    {
      Console.WriteLine($"Memory before creation of array \t {System.GC.GetTotalMemory(false) / 1024} KBytes"); 
      int[][] jaggedArray = new int[Cycles][];
      Console.WriteLine($"Memory after creation of array \t {System.GC.GetTotalMemory(false) / 1024} KBytes");

      for (int i = 0; i < jaggedArray.Length; i++)
      {
        jaggedArray[i] = new int[ValuesPerCycle];
        Console.WriteLine($"Memory after iteration {i+1}/{Cycles} \t {System.GC.GetTotalMemory(false) / 1024} KBytes");
      }

      System.GC.Collect();
      Console.WriteLine($"Memory after calling GC.Collect() \t {System.GC.GetTotalMemory(false) / 1024} KBytes");
      jaggedArray = null;
      System.GC.Collect();
      Console.WriteLine($"Memory after killing reference and calling GC.Collect() \t {System.GC.GetTotalMemory(false) / 1024} KBytes");
    }
  }
}
