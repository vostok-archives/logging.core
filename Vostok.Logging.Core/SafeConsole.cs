using System;

namespace Vostok.Logging.Core
{
    public static class SafeConsole
    {
        public static void TryWriteLine(string message)
        {
            try
            {
                if (ConsoleExists)
                    Console.Out.WriteLine(message);
            }
            catch
            {
                // ignored
            }
        }

        public static void TryWriteLine(object obj) =>
            TryWriteLine(obj?.ToString());

        public static void TryWrite(string message)
        {
            try
            {
                if (ConsoleExists)
                    Console.Out.Write(message);
            }
            catch
            {
                // ignored
            }
        }

        public static void TryWrite(object obj) =>
            TryWrite(obj?.ToString());

        private static bool ConsoleExists => Environment.UserInteractive && Console.Title.Length > 0;
    }
}