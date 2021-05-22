using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;

using SW11.ChatClientLib;  // my chat client
//using ChatClientLib;     // solution chat client

namespace SW11.ChatClient
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private IChatClient client;
    private object uiLock = new object();

    public BindingList<string> Users { get; set; } = new BindingList<string>();
    public string SelectedUser { get; set; }

    public MainWindow()
    {
      InitializeComponent();
      DataContext = this;
      this.textboxUsername.Text = "RainerMueller";  // default username
      this.textboxHost.Text = "eee-00143.simple.eee.intern"; // default server
    }

    private void buttonLogin_Click(object sender, RoutedEventArgs e)
    {
      this.buttonLogin.IsEnabled = false;
      this.client = new ChatClientLib.ChatClient(this.textboxHost.Text);
      this.client.UserUpdate += UserUpdate;
      this.client.MessageReceived += MessageReceived;
      Thread.Sleep(500);
      this.client.Login(this.textboxUsername.Text);
    }

    private void buttonSend_Click(object sender, RoutedEventArgs e)
    {
      this.SendMessage();
    }

    private void texboxMessage_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Enter)
      {
        this.SendMessage();
      }
    }

    private void SendMessage()
    {
      if (String.IsNullOrEmpty(this.SelectedUser) || String.IsNullOrEmpty(this.texboxMessage.Text))
      {
        return;
      }

      this.client.SendMessage(this.SelectedUser, this.texboxMessage.Text);
      this.texboxMessage.Text = "";
    }

    private void UserUpdate(object sender, UserUpdateEventArgs e)
    {
      // checking if this thread has access to the object
      if (Dispatcher.CheckAccess())
      {
        lock (uiLock) // ensure that only one update at a time can be processed
        {
          string prevSelected = this.SelectedUser;  // store previously selected
          this.Users.Clear();
          foreach (var user in e.Users)
          {
            this.Users.Add(user);
          }
          this.SelectedUser = prevSelected;  // restore 

          // this thread has access so it can update the UI thread
          //foreach (var user in this.Users)
          //{
          //  if (e.Users.Contains(user) == false) // user gone
          //  {
          //    this.Users.Remove(user);
          //  }
          //}

          //foreach (var user in e.Users)
          //{
          //  if (this.Users.Contains(user) == false)  // new user
          //  {
          //    this.Users.Add(user);
          //  }
          //}
        }
      }
      else
      {
        // this thread has no access to the UI thread.
        // Place the update method on the Dispatcher of the UI thread.
        Dispatcher.Invoke(new EventHandler<UserUpdateEventArgs>(UserUpdate), sender, e);
      }
    }

    private void MessageReceived(object sender, MessageReceivedEventArgs e)
    {
      Dispatcher.BeginInvoke(new Action(delegate ()
      {
        this.chatbox.Items.Add(e.Message);
      }));
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
      try
      {
        this.client?.Disconnect();
      }
      catch { }
    }


  }
}
