using System;
using System.Threading;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;

namespace Utils.HardwareAccess.Sht30
{
  public class Sht30 : ISht30
  {
    private const byte SHT31_CRC8_POLYNOMIAL = 0x31;    // Seed for CRC polynomial
    private const byte SHT31_CRC8_INIT = 0xFF;          // Init value for CRC
    private II2CDevice tempSensor;

    #region Event
    public event EventHandler<Sht30ChangedEventArgs> TemperatureChanged;
    public event EventHandler<Sht30ChangedEventArgs> HumidityChanged;

    private void OnTemperatureChanged(float temperature)
    {
      this.TemperatureChanged?.Invoke(this, new Sht30ChangedEventArgs(Temperature, Humidity));
    }

    private void OnHumidityChanged(float humidity)
    {
      this.HumidityChanged?.Invoke(this, new Sht30ChangedEventArgs(Temperature, Humidity));
    }
    #endregion

    #region Properties
    public float Temperature { get; private set; }
    public float Humidity { get; private set; }
    #endregion

    public Sht30()
    {
      tempSensor = Pi.I2C.AddDevice(0x44);

      Thread t = new Thread(Run);
      t.IsBackground = true;
      t.Start();
    }

    public void Run()
    {
      float temperature = float.MinValue;
      float humidity = float.MinValue;
      while (true)
      {
        if (ReadTempHumidity(out temperature, out humidity))
        {
          UpdateData(temperature, humidity);
        }
        Thread.Sleep(1000);
      }
    }

    private void UpdateData(float temperature, float humidity)
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
        Humidity = humidity;
        OnHumidityChanged(Humidity);
        sendUpdate = true;
      }
      if (sendUpdate)
      {
        Console.WriteLine($"Sht30 Update: T={ Temperature };H={ Humidity }");
        // Format example: T = 31.1; H = 24.1
        // log.Trace($"Sending SHT31 T={Temperature};H={Humidity}");
      }
    }

    private byte GenerateCRC(byte[] data, byte index, byte length)
    {
      // calculates 8-Bit checksum with given polynomial
      byte crc = SHT31_CRC8_INIT;
      byte b;
      byte i;

      for (i = index; i < (index + length); i++)
      {
        crc ^= data[i];
        for (b = 0; b < 8; b++)
        {
          if ((crc & 0x80) != 0)
          {
            crc = (byte)((crc << 1) ^ SHT31_CRC8_POLYNOMIAL);
          }
          else
          {
            crc <<= 1;
          }
        }
      }
      return crc;
    }

    public bool ReadTempHumidity(out float temperature, out float humidity)
    {
      temperature = humidity = float.MinValue;

      byte[] command = new byte[2] { 0x2C, 0x06 };
      int result = tempSensor.Write(command, command.Length);
      if (result != 2)
      {
        //log.Error("Error write command to SHT30, ErrorCode:" + result);
        return false;
      }
      Thread.Sleep(100);

      byte[] data = new byte[6];
      result = tempSensor.Read(data, 6);
      if (result != 6)
      {
        //log.Error("Error reading data from SHT30, ErrorCode:" + result);
        return false;
      }

      UInt16 t = (UInt16)((data[0] << 8) | data[1]);
      if (data[2] != GenerateCRC(data, 0, 2))
      {
        //log.Error("CRC Error reading temperature from SHT30");
        return false;
      }

      UInt16 h = (UInt16)((data[3] << 8) | data[4]);
      if (data[5] != GenerateCRC(data, 3, 2))
      {
        //log.Error("CRC Error reading humidity from SHT30");
        return false;
      }

      temperature = (175f * t) / 0xffff - 45;
      humidity = (100f * h) / 0xffff;

      temperature = (float)Math.Round(temperature, 1);
      humidity = (float)Math.Round(humidity, 1);

      return true;
    }
  }
}
