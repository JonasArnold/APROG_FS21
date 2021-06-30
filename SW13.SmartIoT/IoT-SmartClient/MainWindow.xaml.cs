using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace IoT_SmartClient
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    RaspberryClient client;
    private const double ImageOpacityON = 1;
    private const double ImageOpacityOFF = 0.3;

    public MainWindow()
    {
      InitializeComponent();
      this.textBoxHost.Text = "eee-01176.local";
      //ConsoleManager.Show();
    }

    private void buttonConnect_Click(object sender, RoutedEventArgs e)
    {
      Task.Run(() => this.ConnectDisconnect());
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      // disconnect if still connected
      if (this.client != null)
      {
        this.ConnectDisconnect();
      }
    }

    void ConnectDisconnect()
    {
      this.SetEnabledState(buttonConnect, false);  // disable button

      try
      {
        if (this.client == null) // not yet connected
        {
          this.client = new RaspberryClient();
          this.client.Connect(this.GetTextBoxText(textBoxHost));
          this.RegisterToEvents();
          this.SetEnabledState(textBoxHost, false);
          this.SetContent(buttonConnect, "Disconnect");
        }
        else  // already connected 
        {
          this.UnregisterFromEvents();
          this.ClearAllValues();
          this.client.Disconnect();
          this.SetEnabledState(textBoxHost, true);
          this.SetContent(buttonConnect, "Connect");
        }
      }
      catch (Exception e)
      {
        MessageBox.Show($"Could not connect to host. \n{e.Message}", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
      }


      this.SetEnabledState(buttonConnect, true);  // re-enable button
    }

    private void ClearAllValues()
    {
      this.SetContent(labelTemperature, "");
      this.SetContent(labelHumidity, "");
      this.SetJoystickStates();
    }

    private void RegisterToEvents()
    {
      this.client.Sht30.TemperatureChanged += Sht30_TemperatureChanged;
      this.client.Sht30.HumidityChanged += Sht30_HumidityChanged;
      this.client.Joystick.JoystickChanged += Joystick_JoystickChanged;
    }

    private void UnregisterFromEvents()
    {
      try
      {
        this.client.Sht30.TemperatureChanged -= Sht30_TemperatureChanged;
        this.client.Sht30.HumidityChanged -= Sht30_HumidityChanged;
        this.client.Joystick.JoystickChanged -= Joystick_JoystickChanged;
      }
      catch { }
    }

    private void Sht30_TemperatureChanged(object sender, IoT_Common.Sht30ChangedEventArgs e)
    {
      Dispatcher.Invoke(() => { labelTemperature.Content = e.Temperature.ToString(); }); // alternative way of owning UI thread
    }

    private void Sht30_HumidityChanged(object sender, IoT_Common.Sht30ChangedEventArgs e)
    {
      Dispatcher.Invoke(() => { labelHumidity.Content = e.Humidity.ToString(); }); // alternative way of owning UI thread
    }

    private void Joystick_JoystickChanged(object sender, IoT_Common.JoystickEventArgs e)
    {
      this.SetJoystickStates(); // reset all

      if ((e.Button & IoT_Common.JoystickButton.Up) != 0) this.SetImageOpacity(ImageJoystickUp, ImageOpacityON);
      if ((e.Button & IoT_Common.JoystickButton.Down) != 0) this.SetImageOpacity(ImageJoystickDown, ImageOpacityON); 
      if ((e.Button & IoT_Common.JoystickButton.Left) != 0) this.SetImageOpacity(ImageJoystickLeft, ImageOpacityON);
      if ((e.Button & IoT_Common.JoystickButton.Right) != 0) this.SetImageOpacity(ImageJoystickRight, ImageOpacityON);
      if ((e.Button & IoT_Common.JoystickButton.Center) != 0) this.SetImageOpacity(ImageJoystickCenter, ImageOpacityON);
    }

    private void ImageLedBlue_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      //this.client.led // TODO
    }

    private void ImageLedGreen_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {

    }

    private void ImageLedYellow_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {

    }

    private void ImageLedRed_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {

    }

    #region Helpers
    public void SetEnabledState(Control controlElement, bool state)
    {
      // checking if this thread has access to the object
      if (Dispatcher.CheckAccess())
      {
        // this thread has access to the UI thread.
        controlElement.IsEnabled = state;  // change state
      }
      else
      {
        // this thread has no access to the UI thread. Request via dispatcher.
        Dispatcher.Invoke(() => this.SetEnabledState(controlElement, state));
      }
    }

    public void SetContent(ContentControl controlElement, string content)
    {
      // checking if this thread has access to the object
      if (Dispatcher.CheckAccess())
      {
        // this thread has access to the UI thread.
        controlElement.Content = content;  // change content
      }
      else
      {
        // this thread has no access to the UI thread. Request via dispatcher.
        Dispatcher.Invoke(() => this.SetContent(controlElement, content));
      }
    }

    public void SetTextBoxText(TextBox textBox, string text)
    {
      // checking if this thread has access to the object
      if (Dispatcher.CheckAccess())
      {
        // this thread has access to the UI thread.
        textBox.Text = text;  // change text
      }
      else
      {
        // this thread has no access to the UI thread. Request via dispatcher.
        Dispatcher.Invoke(() => this.SetTextBoxText(textBox, text));
      }
    }

    public string GetTextBoxText(TextBox textBox)
    {
      // checking if this thread has access to the object
      if (Dispatcher.CheckAccess())
      {
        // this thread has access to the UI thread.
        return textBox.Text; // get text
      }
      else
      {
        // this thread has no access to the UI thread. Request via dispatcher.
        return Dispatcher.Invoke(() => this.GetTextBoxText(textBox));
      }
    }

    public void SetImageOpacity(Image image, double opacity)
    {
      // checking if this thread has access to the object
      if (Dispatcher.CheckAccess())
      {
        // this thread has access to the UI thread.
        image.Opacity = opacity;
      }
      else
      {
        // this thread has no access to the UI thread. Request via dispatcher.
        Dispatcher.Invoke(() => this.SetImageOpacity(image, opacity));
      }
    }

    private void SetJoystickStates(bool up = false, bool down = false, bool left = false, bool right = false, bool center = false)
    {
      this.SetImageOpacity(ImageJoystickUp, up ? ImageOpacityON : ImageOpacityOFF);
      this.SetImageOpacity(ImageJoystickDown, down ? ImageOpacityON : ImageOpacityOFF);
      this.SetImageOpacity(ImageJoystickLeft, left ? ImageOpacityON : ImageOpacityOFF);
      this.SetImageOpacity(ImageJoystickRight, right ? ImageOpacityON : ImageOpacityOFF);
      this.SetImageOpacity(ImageJoystickCenter, center ? ImageOpacityON : ImageOpacityOFF);
    }

    #endregion
  }
}
