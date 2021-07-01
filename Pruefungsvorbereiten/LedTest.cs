using System;
using System.Collections.Generic;
using System.Threading;
using Utils.HardwareAccess;
using Utils.HardwareAccess.Led;

namespace Pruefungsvorbereiten
{
  internal static class LedTest
  {
    public static void Test()
    {
      RaspberryPiHardware rpi = new RaspberryPiHardware();
      rpi.Joystick.JoystickChanged += Joystick_JoystickChanged;

      List<LedColor> leds = new List<LedColor>() { LedColor.Red, LedColor.Blue, LedColor.Green, LedColor.Yellow };

      foreach (LedColor led in leds)
      {
        Console.WriteLine($"{led} led");
        rpi[led].Enable = true;
        Thread.Sleep(2000);
        rpi[led].Enable = false;
      }

      Console.WriteLine($"Current Temperature={rpi.Sht30.Temperature}, Humidity={rpi.Sht30.Humidity}");
    }

    private static void Joystick_JoystickChanged(object sender, Utils.HardwareAccess.Joystick.JoystickEventArgs e)
    {
      Console.WriteLine($"Joystick was pushed in direction: {e.Button}");
    }
  }
}
