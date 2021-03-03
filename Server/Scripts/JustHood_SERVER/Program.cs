using System;
using System.Threading;

namespace JustHood_SERVER
{
    class Program
    {
        private static Thread threadConsole;
        private static bool consoleRunning;

        static void Main(string[] args)
        {
            
            DateTime now = DateTime.UtcNow;
            BroadcastConsole(ConsoleType.INFO, "Loading libraries, please wait...");

            threadConsole = new Thread(ConsoleThread);
            threadConsole.Start();
            
            Datas.networkHandleData.InitMessages();
            Datas.general.InitServer();
            
            BroadcastConsole(ConsoleType.INFO, "Server has successfully started in " + DateTime.UtcNow.Subtract(now).TotalSeconds + "s !");
        }

        private static void ConsoleThread()
        {
            string line;
            consoleRunning = true;

            while (consoleRunning)
            {
                line = Console.ReadLine();

                if (line != null)
                {
                    if (line.Equals("stop"))
                    {
                        consoleRunning = false;
                        return; 
                    }else if (line.StartsWith("datas"))
                    {
                        string[] part = line.Split(" ");
                        if (part.Length == 2)
                        {
                            Datas.networkSendData.SendAllDatas(Int16.Parse(part[1]));
                        }
                    }
                }
            }
        }
        
        public static void BroadcastConsole(ConsoleType type, string msg)
        {
            DateTime now = DateTime.Now;
            Console.WriteLine("[" +now.ToString("HH:mm:ss")+ "] ["+type.Value+"] "+msg);
        }
        
    }
}
