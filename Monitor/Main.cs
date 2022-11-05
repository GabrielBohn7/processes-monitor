using System;
using System.Diagnostics;
using System.Threading;

public class RunMain
{
    static async Task Main(string[] args)
    {
        List<string> killLog = new List<string>();

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
                    killLog = MonitorLocalMachineService.MonitorWindowsProcessesAsync(processToMonitorTuple);
                }
            }).Start();

            newProcess = Console.ReadLine();

            processToMonitorTuple.Add(new Tuple<string[], Stopwatch>(newProcess.Split(' '), new Stopwatch())) ;
            MonitorLocalMachineService.StartTimers(processToMonitorTuple);

        } while (!newProcess.Equals("q"));

        Console.WriteLine("PROCESS STATUS/KILL LOG:\n" + killLog.ToList());
    }
}