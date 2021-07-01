using System;

namespace Utils.HardwareAccess.Sht30
{
  public class Sht30ChangedEventArgs : EventArgs
  {
    public Sht30ChangedEventArgs(float temperature, float humidity)
    {
      Temperature = temperature;
      Humidity = humidity;
    }

    public float Temperature { get; }
    public float Humidity { get; }
  }
}