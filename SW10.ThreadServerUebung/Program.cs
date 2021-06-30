using System;
using System.Threading;

namespace SW10.ThreadServerUebung
{
  class Program
  {
    static void Main(string[] args)
    {
      Server s = new Server();
      for (int i = 0; i < 5; i++)
      {
        Client c = new Client();
        new Thread(c.GetTime).Start();

        //new Thread(new Client().GetTime).Start();
      }
    }
  }
}
