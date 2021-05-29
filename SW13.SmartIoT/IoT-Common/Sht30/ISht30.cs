using System;
using System.Collections.Generic;
using System.Text;

namespace IoT_Common
{
    public interface ISht30
    {
        #region events
        event EventHandler<Sht30ChangedEventArgs> TemperatureChanged;
        event EventHandler<Sht30ChangedEventArgs> HumidityChanged;
        #endregion

        #region properties
        float Temperature { get; }
        float Humidity { get; }
        #endregion
    }
}
