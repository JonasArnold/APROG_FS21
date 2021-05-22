using System;
using System.Threading;

namespace SW10.Threading
{
  class Program
  {
    static void Main(string[] args)
    {
      Thread t = new Thread(() => PrintSymbols(10, "."));
      Thread t2 = new Thread(() => PrintSymbols(5, "+"));
      t.Start();
      Thread.Sleep(1000);
      t2.Start();
      Console.WriteLine("Message from main Thread");
      Console.ReadKey();
    }

    public static void PrintSymbols(int amount, string symbol)
    {
      Console.WriteLine($"Thread with symbol {symbol} started");
      for (int i = 0; i < amount; i++)
      {
        Console.WriteLine(symbol);
        Thread.Sleep(200);
      }
      Console.WriteLine($"Thread with symbol {symbol} ended");
    }
  }
}
