using System.IO;

namespace SW06.LedRaspberry
{
  internal class RaspberryPi
  {
    /// <summary>
    /// Sets the green LED's brightness to full / off depending on passed "on" parameter.
    /// </summary>
    /// <param name="on">state to set the led to</param>
    public void SetGreenLed(bool on)
    {
      WriteToFile($"{Constants.RaspberryLedFilePath}/{Constants.RaspberryGreenLedName}/brightness", on ? "1" : "0");
    }

    /// <summary>
    /// Sets the red LED's brightness to full / off depending on passed "on" parameter.
    /// </summary>
    /// <param name="on">state to set the led to</param>
    public void SetRedLed(bool on)
    {
      WriteToFile($"{Constants.RaspberryLedFilePath}/{Constants.RaspberryRedLedName}/brightness", on ? "1" : "0");
    }

    /// <summary>
    /// Resets the green and red leds function to their default function.
    /// </summary>
    public void ResetLedsToDefaultFunction()
    {
      WriteToFile($"{Constants.RaspberryLedFilePath}/{Constants.RaspberryGreenLedName}/trigger", "mmc0");
      WriteToFile($"{Constants.RaspberryLedFilePath}/{Constants.RaspberryRedLedName}/trigger", "default-on");
    }

    private static void WriteToFile(string filePath, string content)
    {
      using StreamWriter fileStream = new StreamWriter(filePath);
      fileStream.Write(content);
      fileStream.Flush();
      fileStream.Close();
    }
  }
}
