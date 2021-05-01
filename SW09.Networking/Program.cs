﻿using System;
using System.Net;
using System.Threading;

namespace SW09.Networking
{
  class Program
  {
    static bool server = false, client = false;
    private const string timeServerHostname = "time.nist.gov";

    static void Main(string[] args)
    {
      Console.WriteLine("Choose to be server or client by pressing the according key: ");
      Console.WriteLine("Server: S");
      Console.WriteLine("Client: C");

      // wait for key
      while (true)
      {
        if (Console.KeyAvailable)
        {
          if (Console.ReadKey().Key == ConsoleKey.S)
          {
            server = true;
            break;
          }
          else if (Console.ReadKey().Key == ConsoleKey.C)
          {
            client = true;
            break;
          }
        }
        Thread.Sleep(100);
      }

      Console.WriteLine("---------------------------------------");

      if (server)
      {
        Console.WriteLine("\n\nStarting TCP DayTime Server...");

        TcpDayTimeServer tcpDayTimeServer = new();
        tcpDayTimeServer.StartListening();
        Console.WriteLine("Server is now listening.");
        Console.WriteLine("Stop Server by pressing Space key.");

        // wait for space
        while (true)
        {
          if (Console.KeyAvailable)
          {
            if (Console.ReadKey().Key == ConsoleKey.Spacebar)
            {
              break;
            }
          }
          Thread.Sleep(100);
        }

        tcpDayTimeServer.StopListening();
        Console.WriteLine("\n\nStopped Connection.");
      }

      if (client)
      {
        Console.WriteLine("Starting TCP DayTime Client.");
        bool stopped = false;

        while (stopped == false)
        {
          Console.WriteLine("\n\nChoose to which server to connect by pressing the according key:");
          Console.WriteLine("Offical NIST Gov Time Server: O");
          Console.WriteLine("Localhost: L");
          Console.WriteLine("RaspberryPi: R");
          Console.WriteLine("Desktop: D");
          Console.WriteLine("Stop: Spacebar");

          string hostname = "";
          // choose which connection to request time from
          while (true)
          {
            if (Console.KeyAvailable)
            {
              if (Console.ReadKey().Key == ConsoleKey.O)
              {
                hostname = timeServerHostname;
                break;
              }
              else if (Console.ReadKey().Key == ConsoleKey.L)
              {
                hostname = "localhost";
                break;
              }
              else if (Console.ReadKey().Key == ConsoleKey.R)
              {
                hostname = "eee-01176";
                break;
              }
              else if (Console.ReadKey().Key == ConsoleKey.D)
              {
                hostname = "DESKTOP-JONAS";
                break;
              }
              else if (Console.ReadKey().Key == ConsoleKey.Spacebar)
              {
                stopped = true;
                break;
              }
            }
            Thread.Sleep(100);
          }

          if (!stopped)
          {
            TcpDayTimeClient tcpDayTimeClient = new();
            tcpDayTimeClient.Connect(hostname);
            Console.WriteLine($"\nConnected to {hostname}.");

            Console.WriteLine($"ReadLine: {tcpDayTimeClient.ReadLine()}");

            tcpDayTimeClient.Close();
            Console.WriteLine("Closed Connection.");
          }
        }
      }

      return;
    }
  }
}
