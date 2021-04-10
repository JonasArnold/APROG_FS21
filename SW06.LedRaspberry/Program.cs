using System;
using System.Threading;

namespace SW06.LedRaspberry
{
  class Program
  {
    static RaspberryPi raspberryPi = new RaspberryPi();

    static void Main(string[] args)
    {
      SwitchLedSomeTimes(5);
      raspberryPi.ResetLedsToDefaultFunction();
    }

    private static void SwitchLedSomeTimes(int count)
    {
      for (int i = 0; i < count; i++)
      {
        raspberryPi.SetRedLed(false);
        raspberryPi.SetGreenLed(true);
        Console.WriteLine($"LED Green");
        Thread.Sleep(1000);
        raspberryPi.SetGreenLed(false);
        raspberryPi.SetRedLed(true);
        Console.WriteLine($"LED Red");
        Thread.Sleep(1000);
      }
    }
  }
}
