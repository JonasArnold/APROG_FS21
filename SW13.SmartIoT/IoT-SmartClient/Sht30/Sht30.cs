using IoT_Common;
using System;
using System.Globalization;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace IoT_SmartClient
{
  public class Sht30 : MqttDevice, ISht30
  {
    #region Properties
    public float Temperature { get; private set; }

    public float Humidity { get; private set; }
    #endregion

    public Sht30(MqttClient client, string topic) : base(client, topic) { }

    public event EventHandler<Sht30ChangedEventArgs> TemperatureChanged;
    public event EventHandler<Sht30ChangedEventArgs> HumidityChanged;

    /// <summary>
    /// Parses SHT30 messages: e.g. "T=23.9;H=44.2"
    /// </summary>
    /// <param name="message">message</param>
    /// <param name="e">event args</param>
    protected override void StatusReceived(string message, MqttMsgPublishEventArgs e)
    {
      base.StatusReceived(message, e);
      var splitMessage = message.Split(";");

      // parse with invariant culture (sorts out if culture uses comma or dot as separator)
      var temperature = float.Parse(splitMessage[0].Substring(2), CultureInfo.InvariantCulture.NumberFormat);
      var humidity = float.Parse(splitMessage[1].Substring(2), CultureInfo.InvariantCulture.NumberFormat);
      Sht30ChangedEventArgs eventArgs = new Sht30ChangedEventArgs(temperature, humidity);

      // only send events if something changed
      if (this.Temperature != temperature)
      {
        this.TemperatureChanged?.Invoke(this, eventArgs);
      }
      if (this.Humidity != humidity)
      {
        this.HumidityChanged?.Invoke(this, eventArgs);
      }
    }
  }
}
