using System;

namespace Utils.HardwareAccess.Led
{
  public class LedStateChangedEventArgs : EventArgs
  {
    public LedStateChangedEventArgs(bool enabled)
    {
      Enabled = enabled;
    }

    public bool Enabled { get; }
  }
}