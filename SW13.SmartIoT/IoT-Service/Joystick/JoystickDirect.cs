using IoT_Common;
using System;
using System.Collections.Generic;
using System.Threading;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;
using uPLibrary.Networking.M2Mqtt;

namespace IoT_Service
{
  public class JoystickDirect : Joystick
  {
    private static readonly IGpioPin joystickUpPin = Pi.Gpio[BcmPin.Gpio19];  // GPIO 24
    private static readonly IGpioPin joystickDownPin = Pi.Gpio[BcmPin.Gpio13];  // GPIO 23
    private static readonly IGpioPin joystickLeftPin = Pi.Gpio[BcmPin.Gpio06];  // GPIO 22
    private static readonly IGpioPin joystickRightPin = Pi.Gpio[BcmPin.Gpio05];  // GPIO 21
    private static readonly IGpioPin joystickPushPin = Pi.Gpio[BcmPin.Gpio26];  // GPIO 25
    private static readonly List<IGpioPin> inputPins = new() { joystickUpPin, joystickDownPin, joystickLeftPin, joystickRightPin, joystickPushPin };

    public override JoystickButton State { get
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

    public JoystickDirect(MqttClient client, string topic) : base(client, topic)
    {
      // set pin modes
      foreach (var pin in inputPins)
      {
        pin.PinMode = GpioPinDriveMode.Input; // pullup already in hardware
      }

      // start sending thread
      Thread sendingThread = new Thread(Run);
      sendingThread.IsBackground = true; // automatically ending thread
      sendingThread.Start();
    }

    private void Run()
    {
      JoystickButton oldState = State;  // read old state
      while(true)
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
