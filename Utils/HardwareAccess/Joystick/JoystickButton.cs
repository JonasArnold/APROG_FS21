using System;

namespace Utils.HardwareAccess.Joystick
{
  [Flags]
  public enum JoystickButton
  {
    None = 0,
    Left = 1,
    Right = 2,
    Up = 4,
    Down = 8,
    Center = 16
  }
}
