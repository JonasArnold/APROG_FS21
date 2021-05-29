using IoT_Common;
using System;
using System.Collections.Generic;
using System.Text;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;
using Unosquare.WiringPi;
using uPLibrary.Networking.M2Mqtt;

namespace IoT_Service
{
    public class LedDirect : Led
    {

        #region members
        private IGpioPin ledPin;
        #endregion

        #region constructor & destructor
        public LedDirect(MqttClient client, string topic, P1 pin, LedColor color) : base (client, topic, color)
        {
            ledPin = Pi.Gpio[pin];
            ledPin.PinMode = GpioPinDriveMode.Output;
            Enable = false;
        }
        #endregion

        #region properties
        protected override bool EnableInternal
        {
            set { ledPin.Write(value); }
            get { return ledPin.Read(); }
        }
        #endregion
    }
}
