using System;
using System.Diagnostics;
using System.Threading;
using Monitor.MonitorLocalMachineService;

namespace Monitor.Main
{
    public static class RunMain
    {
        static void Main(string[] args)
        {
            List<string> killLog = new();

            var processToMonitorTuple = new List<Tuple<string[], Stopwatch>>();
            if (args.Length > 0)
            {
                processToMonitorTuple.Add(new Tuple<string[], Stopwatch>(args, new Stopwatch()));
                processToMonitorTuple[0].Item2.Start();
            }

            var newProcess = String.Empty;
            Console.WriteLine("Enter 'q' to stop:");
            do
            {
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    while (processToMonitorTuple.ToList().Count > 0)
                    {
                        Thread.Sleep(1000);
                        killLog = MonitorLocalMachineService.MonitorLocalMachineService.MonitorWindowsProcessesAsync(processToMonitorTuple);
                    }
                }).Start();

                newProcess = Console.ReadLine();

                processToMonitorTuple.Add(new Tuple<string[], Stopwatch>(newProcess.Split(' '), new Stopwatch()));
                MonitorLocalMachineService.MonitorLocalMachineService.StartTimers(processToMonitorTuple);

            } while (!newProcess.Equals("q"));
        }
    }
}