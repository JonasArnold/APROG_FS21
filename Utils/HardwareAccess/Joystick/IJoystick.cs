using System;

namespace Utils.HardwareAccess.Joystick
{
  public interface IJoystick
  {
    event EventHandler<JoystickEventArgs> JoystickChanged;
    JoystickButton State { get; }
  }
}
