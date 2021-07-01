using System;

namespace Utils.HardwareAccess.Joystick
{
  public class JoystickEventArgs : EventArgs
  {
    public JoystickEventArgs(JoystickButton button)
    {
      Button = button;
    }
    public JoystickButton Button { get; }
  }
}
