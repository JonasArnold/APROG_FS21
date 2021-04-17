using System;
using System.Threading;

namespace Utils
{
    public class Util
    {
        /// <summary>
        /// This function waits until the Debugger has been attached or the Enter Key has been pressed
        /// </summary>
        /// <returns>
        /// true => Debugger is attached
        /// false => Enter Key was pressed
        /// </returns>
        public static bool WaitForDebugger()
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1 && args[1] == "--debug")
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Waiting for Debugger or press <Enter> to continue");
                Console.ForegroundColor = color;
                while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        if (Console.ReadKey().Key == ConsoleKey.Enter)
                        {
                            break;
                        }
                    }
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        return true;
                    }
                    Thread.Sleep(100);
                }
            }
            return false;
        }
    }
}
