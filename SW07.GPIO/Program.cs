using System;
using System.Collections.Generic;
using System.Threading;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;
using Unosquare.WiringPi;

namespace SW07.GPIO
{
  class Program
  {
    private static List<IGpioPin> ledPins;
    private static IGpioPin buttonPin;
    private static Thread blinkingThread;
    private static bool blinking = false;

    static void Main(string[] args)
    {
      // init Wiring Pi
      Pi.Init<BootstrapWiringPi>();
      Console.WriteLine("WiringPi initialized");

      // initialize GPIO pins
      ledPins = new List<IGpioPin>()
      {
        Pi.Gpio[BcmPin.Gpio21],  // led 1, GPIO 29
        Pi.Gpio[BcmPin.Gpio20],  // led 2, GPIO 28
        Pi.Gpio[BcmPin.Gpio16],  // led 3, GPIO 27
        Pi.Gpio[BcmPin.Gpio12],  // led 4, GPIO 26
      };
      buttonPin = Pi.Gpio[BcmPin.Gpio26];  // joystick push, GPIO 25

      // set all leds to output mode
      foreach (var pin in ledPins)
      {
        pin.PinMode = GpioPinDriveMode.Output;
      }

      // set button to input mode
      buttonPin.PinMode = GpioPinDriveMode.Input; // pullup already in hardware

      // Main Program loop
      Console.WriteLine("Program ready, press Joystick to start led blinking");
      while (true)
      {
        if(buttonPin.Read() == false) // low when pressed
        {
          // invert blinking state 
          blinking = !blinking;

          // wait for thread to end if it is being ended now
          if(blinking)
          {
            blinkingThread = new Thread(BlinkAllLedsEndless);
            blinkingThread.Name = "Led Blinking Thread";
            blinkingThread.Start();
          }
          else
          {
            blinkingThread?.Join();
          }

          Thread.Sleep(150);  // debounce button
        }
      }
    }

    static void BlinkAllLedsEndless()
    {
      Console.WriteLine("Starting LED blinking");
      while (blinking)
      {
        // led pattern
        foreach (var led in ledPins)
        {
          led.Write(true);
          Thread.Sleep(500);
          led.Write(false);

          if(blinking == false)
          {
            break;
          }
        }
      }
      Console.WriteLine("LED blinking ended");
    }
  }
}
