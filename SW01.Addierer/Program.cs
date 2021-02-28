using System;

namespace SW01.Adder
{
  class Program
  {
    static int Main(string[] args)
    {
      AdderError error = AdderError.OK;

      // sum up all args
      int sum = 0;
      foreach (string arg in args)
      {
        // try parse int value
        if (int.TryParse(arg, out int parsedArg))
        {
          sum += parsedArg; // add to sum
          Console.WriteLine($"Added value={parsedArg}. sum={sum}");
        }
        // else no number => ignore
      }

      // check if sum is 0 (no numbers recoginzed in args)
      if (sum == 0)
      {
        error = AdderError.NoNumberParsedFromArgs;
        Console.WriteLine("No integer Number could be parsed from the arguments.");
      }
      else // if sum is not 0 => display result
      {
        Console.WriteLine($"Calculated sum: {sum}");
      }

      return (int)error;
    }
  }
}
