using IoT_Common;
using System;
using uPLibrary.Networking.M2Mqtt;

namespace IoT_Service
{
  public abstract class Joystick : MqttDevice, IJoystick
  {
    public event EventHandler<JoystickEventArgs> JoystickChanged;

    public abstract JoystickButton State { get; }

    public Joystick(MqttClient client, string topic) : base(client, topic)
    {

    }

    protected void OnJoystickChanged(JoystickButton state)
    {
      SendStatusUpdate(state.ToString());
      this.JoystickChanged?.Invoke(this, new JoystickEventArgs(state));
    }
  }
}
