using System;
using System.Collections.Generic;
using System.Threading;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;

namespace Utils.HardwareAccess.Joystick
{
  public class Joystick : IJoystick
  {
    private static readonly IGpioPin joystickUpPin = Pi.Gpio[BcmPin.Gpio19];  // GPIO 24
    private static readonly IGpioPin joystickDownPin = Pi.Gpio[BcmPin.Gpio13];  // GPIO 23
    private static readonly IGpioPin joystickLeftPin = Pi.Gpio[BcmPin.Gpio06];  // GPIO 22
    private static readonly IGpioPin joystickRightPin = Pi.Gpio[BcmPin.Gpio05];  // GPIO 21
    private static readonly IGpioPin joystickPushPin = Pi.Gpio[BcmPin.Gpio26];  // GPIO 25
    private static readonly List<IGpioPin> inputPins = new() { joystickUpPin, joystickDownPin, joystickLeftPin, joystickRightPin, joystickPushPin };

    #region Properties
    public JoystickButton State
    {
      get
      {
        JoystickButton state = JoystickButton.None;
        if (!joystickLeftPin.Read()) state |= JoystickButton.Left;
        if (!joystickRightPin.Read()) state |= JoystickButton.Right;
        if (!joystickUpPin.Read()) state |= JoystickButton.Up;
        if (!joystickDownPin.Read()) state |= JoystickButton.Down;
        if (!joystickPushPin.Read()) state |= JoystickButton.Center;
        return state;
      }
    }
    #endregion

    #region Events
    public event EventHandler<JoystickEventArgs> JoystickChanged;

    private void OnJoystickChanged(JoystickButton state)
    {
      Console.WriteLine($"Joystick was pushed: {state}");
      this.JoystickChanged?.Invoke(this, new JoystickEventArgs(state));
    }
    #endregion

    public Joystick()
    {
      // set pin modes
      foreach (var pin in inputPins)
      {
        pin.PinMode = GpioPinDriveMode.Input; // pullup already in hardware
      }

      // start thread to send updates whe a button was pressed
      Thread thread = new Thread(Run);
      thread.IsBackground = true; // automatically ending thread
      thread.Start();
    }

    private void Run()
    {
      JoystickButton oldState = State;  // read old state
      while (true)
      {
        JoystickButton newState = State;   // read new state
        if (oldState != newState) // only send update when state changed
        {
          oldState = newState;
          OnJoystickChanged(newState);
        }
        Thread.Sleep(50);  // always wait
      }
    }
  }
}
