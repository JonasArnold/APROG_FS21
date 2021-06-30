using System;

namespace SW08.Logging
{
  class Program
  {
    private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

    static void Main(string[] args)
    {
      log.Warn("Warning");
      log.Error("Error");
      log.Info("Information");
    }
  }
}
