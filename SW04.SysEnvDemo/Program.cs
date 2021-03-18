using System;

namespace SW04.SysEnvDemo
{
  class Program
  {
    static void Main(string[] args)
    {
      PrintEnvironmentInfo();
    }

    static void PrintEnvironmentInfo()
    {
      Console.WriteLine($"Current Directory\t: {Environment.CurrentDirectory}");
      Console.WriteLine($"Is 64 Bit OS\t\t: {Environment.Is64BitOperatingSystem}");
      Console.WriteLine($"OS Version\t\t: {Environment.OSVersion}");
      Console.WriteLine($"Processor Count\t\t: {Environment.ProcessorCount}");
      Console.WriteLine($"Runtime in minutes\t: {TimeSpan.FromMilliseconds(Environment.TickCount).TotalMinutes}");
      Console.WriteLine($"Current user\t\t: {Environment.UserName}");
      Console.WriteLine($"Thread ID\t\t: {Environment.CurrentManagedThreadId}");
      Console.WriteLine($"CLR Version\t\t: {Environment.Version}");
      Console.Write($"Logical Drives\t\t: ");
      var logicalDrives = Environment.GetLogicalDrives();
      foreach (var drive in logicalDrives)
      {
        Console.Write($"{drive} ");
      }
      Console.Write(Environment.NewLine + Environment.NewLine); // two newlines
      string environmentVariable = "PROCESSOR_IDENTIFIER";
      Console.WriteLine($"Value of environment variable {environmentVariable}:\n {Environment.GetEnvironmentVariable(environmentVariable)}");
    }

    // would enable to fill with dots
    static string Pad(string label, string value)
    {
      return label.PadRight(23, '.') + ":" +value;
    }
  }
}
