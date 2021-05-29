using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;

namespace SmartClientUI
{
  internal class RaspberryClient
  {
    private MqttClient client;

    public void Connect(string host)
    {
      if (this.client == null)  // only if not yet connected
      {
        this.client = new MqttClient(host);  // connect
        this.client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;  // register to event
      }
    }

    private void Client_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
    {
      Console.WriteLine($"MQTT message recieved. topic:<{e.Topic}> message: '{e.Message}'.");
    }
  }
}
