using System;
using System.Threading;

namespace SW10.ThreadSyncProblem
{
  class Program
  {
    public Program(Counter counter, string id)
    {
      Counter = counter;
      ID = id;
    }

    public Counter Counter { get; }
    public string ID { get; }

    public void Run()
    {
      Random r = new Random();
      for (int i = 0; i < 10; i++)
      {
        Console.WriteLine($"Thread: {ID}, Counter: {Counter.GetNextNumber()}");
        Thread.Sleep(r.Next(50, 100));
      }
    }

    static void Main(string[] args)
    {
      Counter c = new Counter();
      Program p1 = new Program(c, "1");
      Program p2 = new Program(c, "2");
      new Thread(p1.Run).Start();
      new Thread(p2.Run).Start();
      Console.ReadKey();
    }
  }
}
