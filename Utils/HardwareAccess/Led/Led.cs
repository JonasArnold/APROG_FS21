using System;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;

namespace Utils.HardwareAccess.Led
{
  public class Led : ILed
  {
    private IGpioPin ledPin;

    #region Properties
    public LedColor Color { get; }

    public bool Enable
    {
      get { return ledPin.Read(); }
      set
      {
        var oldValue = ledPin.Read();
        ledPin.Write(value);
        if (oldValue != value)  // send update only when changed
        {
          OnLedStateChanged(value);
        }
      }
    }
    #endregion

    #region Event
    public event EventHandler<LedStateChangedEventArgs> LedStateChanged;

    protected void OnLedStateChanged(bool enabled)
    {
      Console.WriteLine($"Led {this.Color} changed to enabled={enabled}");
      LedStateChanged?.Invoke(this, new LedStateChangedEventArgs(enabled));
    }
    #endregion

    public Led(P1 pin, LedColor color)
    {
      ledPin = Pi.Gpio[pin];
      ledPin.PinMode = GpioPinDriveMode.Output;
      Enable = false;
    }
  }
}
