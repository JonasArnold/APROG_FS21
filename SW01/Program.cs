using System;
using System.Collections.Generic;

namespace SW01
{
  class Program
  {
    static void Main(string[] args)
    {
      ForeachTest(new List<string>() { "Test1", "Test2", "Test3" });

      #region Bananabread
      PrintUpper("Hello World!");
      var key = Console.ReadKey();
      Console.WriteLine($"Key pressed: {key.KeyChar}");
      #endregion
    }

    private static void PrintUpper(string text)
    {
      Console.WriteLine(text.ToUpper());
    }

    private static void ForeachTest(IList<string> list)
    {
      foreach (string item in list)
      {
        Console.WriteLine($"Index of {item}: {list.IndexOf(item)}"); 
      }
    }
  }
}
