using IoT_Common;
using System.Collections.Generic;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;
using Unosquare.WiringPi;
using uPLibrary.Networking.M2Mqtt;

namespace IoT_Service
{
  public class Raspberry
  {
    #region members
    private Dictionary<LedColor, Led> leds;
    #endregion

    #region constructor & destructor
    public Raspberry(bool direct)
    {
      //Client = new MqttClient("10.180.21.144");
      Client = new MqttClient("127.0.0.1");
      leds = new Dictionary<LedColor, Led>(4);

      if (direct)
      {
        Pi.Init<BootstrapWiringPi>();
        leds.Add(LedColor.Blue, new LedDirect(Client, Constants.TOPIC_LEDS, P1.Pin32, LedColor.Blue));
        leds.Add(LedColor.Green, new LedDirect(Client, Constants.TOPIC_LEDS, P1.Pin36, LedColor.Green));
        leds.Add(LedColor.Red, new LedDirect(Client, Constants.TOPIC_LEDS, P1.Pin40, LedColor.Red));
        leds.Add(LedColor.Yellow, new LedDirect(Client, Constants.TOPIC_LEDS, P1.Pin38, LedColor.Yellow));

        Sht30 = new Sht30Direct(Client, Constants.TOPIC_SHT30);
        Joystick = new JoystickDirect(Client, Constants.TOPIC_JOYSTICK);
      }
      else
      {
        Com com = new Com();
        leds.Add(LedColor.Blue, new LedSerial(Client, Constants.TOPIC_LEDS, com, LedColor.Blue));
        leds.Add(LedColor.Green, new LedSerial(Client, Constants.TOPIC_LEDS, com, LedColor.Green));
        leds.Add(LedColor.Red, new LedSerial(Client, Constants.TOPIC_LEDS, com, LedColor.Red));
        leds.Add(LedColor.Yellow, new LedSerial(Client, Constants.TOPIC_LEDS, com, LedColor.Yellow));
      }
    }
    #endregion


    #region properties
    public Led this[LedColor led]
    {
      get { return leds[led]; }
    }

    public MqttClient Client { get; }

    public Sht30 Sht30 { get; }

    public Joystick Joystick { get; }
    #endregion


    #region methods
    public void Connect()
    {
      Client.Connect("");
    }

    public void Disconnect()
    {
      Client.Disconnect();
    }
    #endregion
  }
}