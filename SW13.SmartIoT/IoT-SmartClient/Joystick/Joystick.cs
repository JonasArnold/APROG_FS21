using IoT_Common;
using System;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace IoT_SmartClient
{
  public class Joystick : MqttDevice, IJoystick
  {
    public Joystick(MqttClient client, string topic) : base(client, topic) { }

    public JoystickButton State { get; private set; }

    public event EventHandler<JoystickEventArgs> JoystickChanged;

    protected override void StatusReceived(string message, MqttMsgPublishEventArgs e)
    {
      base.StatusReceived(message, e);
      this.State = (JoystickButton)Enum.Parse(typeof(JoystickButton), message);
      // fire event
      this.JoystickChanged?.Invoke(this, new JoystickEventArgs(this.State));
    }
  }
}
