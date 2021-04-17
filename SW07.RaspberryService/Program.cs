using System;

namespace SW07.RaspberryService
{
  class Program
  {
    static void Main(string[] args)
    {
      ConsoleColor color = Console.ForegroundColor;
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine("Raspberry Service started.");
      Console.ForegroundColor = color;
    }
  }
}
