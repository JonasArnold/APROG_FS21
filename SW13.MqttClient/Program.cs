using System;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace SW13.MqttClient
{
  class Program
  {
		static void Main(string[] args)
		{
			Console.WriteLine("Hello MQTT World!");
			// creating an MqttClient object
			var client = new uPLibrary.Networking.M2Mqtt.MqttClient("broker.hivemq.com");
			// register to message received
			client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
			// generate a clientID and connect to Broker
			string clientId = Guid.NewGuid().ToString();
			client.Connect(clientId);
			// subscribe to a topic
			client.Subscribe(new string[] { "scada/status" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
			// Endless loop
			while (true)
			{
			}
		}
		static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
		{
			// handle message received 
			Console.Write("Message received: ");
			Console.Write(Encoding.UTF8.GetString(e.Message) + "\n");
		}
	}
}
