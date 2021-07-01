using System;
using System.Text;
using System.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace SW13.MqttClient
{
  class Program
  {
		static void Main(string[] args)
		{
			Console.WriteLine("Hello MQTT World! Press Space to send test message to topic 'scada/status'.");
			// creating an MqttClient object
			var client = new uPLibrary.Networking.M2Mqtt.MqttClient("192.168.1.161");
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
				Thread.Sleep(50);
				if(Console.KeyAvailable)
        {
					var key = Console.ReadKey();
					if(key.Key == ConsoleKey.Spacebar)
          {
            Console.WriteLine($"Sending \"Test Message\" to topic 'scada/status'");
						client.Publish("scada/status", Encoding.ASCII.GetBytes("Test Message"));
          }
        }
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
