using System;

namespace Utils.HardwareAccess.Led
{
  public interface ILed
  {
    event EventHandler<LedStateChangedEventArgs> LedStateChanged;

    bool Enable { get; set; }

    LedColor Color { get; }
  }
}
