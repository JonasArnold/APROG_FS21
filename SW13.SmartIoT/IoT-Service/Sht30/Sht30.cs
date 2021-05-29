﻿using IoT_Common;
using System;
using System.Collections.Generic;
using System.Text;
using uPLibrary.Networking.M2Mqtt;

namespace IoT_Service
{
    public abstract class Sht30 : MqttDevice, ISht30
    {

        #region members
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        public event EventHandler<Sht30ChangedEventArgs> TemperatureChanged;
        public event EventHandler<Sht30ChangedEventArgs> HumidityChanged;
        #endregion

        #region constructor & destructor
        public Sht30(MqttClient client, string topic ) : base(client, topic)
        {
        }
        #endregion

        #region properties
        public float Temperature { get; protected set; }
        public float Humidity { get; protected set; }
        #endregion

        #region methods
        protected void OnTemperatureChanged(float temperature)
        {
            TemperatureChanged?.Invoke(this, new Sht30ChangedEventArgs(Temperature, Humidity));
        }

        protected void OnHumidityChanged(float humidity)
        {
            // ToDo
        }

        protected void UpdateData(float temperature, float humidity)
        {
            bool sendUpdate = false;
            if (temperature != Temperature)
            {
                Temperature = temperature;
                OnTemperatureChanged(Temperature);
                sendUpdate = true;
            }
            if (humidity != Humidity)
            {
                // ToDo
            }
            if (sendUpdate)
            {
                // ToDo Send Status Update (Format example: T=31.1;H=24.1)
                log.Trace($"Sending SHT31 T={Temperature};H={Humidity}");
            }
        }
        #endregion
    }
}
