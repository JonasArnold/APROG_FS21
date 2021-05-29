﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IoT_Common
{
    public class Sht30ChangedEventArgs : EventArgs
    {
        #region constructor & destructor
        public Sht30ChangedEventArgs(float temperature, float humidity)
        {
            Temperature = temperature;
            Humidity = humidity;
        }
        #endregion

        #region properties
        public float Temperature { get; }
        public float Humidity { get; }
        #endregion
    }
}
