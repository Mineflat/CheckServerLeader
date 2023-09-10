using System.Diagnostics;

namespace CheckSakuraLeader
{
    internal class Program
    {
        private static readonly string ServerDebugURI = "https://127.0.0.1:1322";
        private static readonly string ServerProccessName = "*";
        static void Main(string[] args)
        {
            // Рассматриваем вариант, когда конкретно на этой ВМ вообще сервер Сакура не запущен
            Process[] sakuraProccess = Process.GetProcessesByName(ServerProccessName);
            if(sakuraProccess.Length== 0) Environment.Exit(1);
            // Определяем мастера сервера, исходя из переданного UUID
            if (args.Length != 1) Environment.Exit(1);
            ProccessInfoParser.CheckServerLeader(ServerDebugURI, args[0]);
        }
    }
}