using System;

namespace SW01
{
  class Program
  {
    static void Main(string[] args)
    {
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
  }
}
