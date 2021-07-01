using System.Collections.Generic;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;
using Unosquare.WiringPi;
using Utils.HardwareAccess.Joystick;
using Utils.HardwareAccess.Led;
using Utils.HardwareAccess.Sht30;

namespace Utils.HardwareAccess
{
  public class RaspberryPiHardware
  {
    private Dictionary<LedColor, ILed> leds = new Dictionary<LedColor, ILed>();

    #region Properties
    public IJoystick Joystick { get; }

    public ILed this[LedColor led]
    {
      get { return leds[led]; }
    }

    public ISht30 Sht30 { get; }
    #endregion

    public RaspberryPiHardware()
    {
      Pi.Init<BootstrapWiringPi>();

      this.Joystick = new Joystick.Joystick();
      this.Sht30 = new Sht30.Sht30();
      this.leds.Add(LedColor.Blue, new Led.Led(P1.Pin32, LedColor.Blue));
      this.leds.Add(LedColor.Green, new Led.Led(P1.Pin36, LedColor.Green));
      this.leds.Add(LedColor.Red, new Led.Led(P1.Pin40, LedColor.Red));
      this.leds.Add(LedColor.Yellow, new Led.Led(P1.Pin38, LedColor.Yellow));
    }
  }
}
