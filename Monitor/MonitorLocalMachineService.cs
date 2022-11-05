using System;
using System.Diagnostics;
using System.Xml.Linq;

public class MonitorLocalMachineService
{
    public static List<string> MonitorWindowsProcesses(List<Tuple<string[], Stopwatch>> processToMonitorTuple)
    {
        if (!CheckCommandInput(processToMonitorTuple))
        {
            Console.WriteLine($"Invalid Command Format");
            var error = new List<string>();
            error.Add("Invalid Command Format");
            return error;
        }

        List<string> killLog = new List<string>();
        while (processToMonitorTuple.ToList().Count > 0)
        {
            Thread.Sleep(1000);
            var logTask = MonitorPrograms(processToMonitorTuple).Result;
            foreach (var log in logTask)
            {
                if (!string.IsNullOrEmpty(log))
                {
                    killLog.Add(log);
                }
            }
        }
        return killLog;
    }

    public static List<string> MonitorWindowsProcessesAsync(List<Tuple<string[], Stopwatch>> processToMonitorTuple)
    {
        if (!CheckCommandInput(processToMonitorTuple))
        {
            Console.WriteLine($"Invalid Command Format");
            var error = new List<string>();
            error.Add("Invalid Command Format");
            processToMonitorTuple.RemoveAll(item => item.Item1 == processToMonitorTuple[0].Item1);
            return error;
        }

        List<string> killLog = new List<string>();
        var logTask = MonitorPrograms(processToMonitorTuple).Result;
        foreach (var logInd in logTask)
        {
            if (!string.IsNullOrEmpty(logInd))
            {
                killLog.Add(logInd);
            }
        }

        return killLog;
    }

    public static async Task<List<string>> MonitorPrograms(List<Tuple<string[], Stopwatch>> processToMonitorTuple)
    {
        List<string> log = new List<string>();
        foreach (var process in processToMonitorTuple.ToList())
        {
            if (process.Item2.Elapsed.Minutes == Int32.Parse(process.Item1[2]))
            {
                process.Item2.Restart();
                log.Add(CheckProcessStatus(process.Item1, processToMonitorTuple));
            }
        }
        return log;
    }

    public static string CheckProcessStatus(string[]? process, List<Tuple<string[], Stopwatch>> processToMonitorTuple)
    {
        Process[] localByName = Process.GetProcessesByName(process[0]);
        double maximunLifetime = Double.Parse(process[1]);
        string log = String.Empty;
        
        if (!(localByName.Count() > 0))
        {
            processToMonitorTuple.RemoveAll(item => item.Item1 == process);
            Console.WriteLine($"Process {process[0]} is not running");
            log = $"Process {process[0]} is not running";
        }
        else
        {
            foreach (Process individualProcess in localByName.ToList())
            {
                var runtime = DateTime.Now - individualProcess.StartTime;
                if (runtime.TotalMinutes > maximunLifetime)
                {
                    individualProcess.Kill();
                    processToMonitorTuple.RemoveAll(item => item.Item1 == process);
                    Console.WriteLine($"Process {process[0]} killed");
                    log = $"Process {process[0]} killed";
                }
            }
        }
        
        if (String.IsNullOrEmpty(log))
        {
            Console.WriteLine($"Process {process[0]} did not exceed threshold");
            log = $"Process {process[0]} did not exceed threshold";
        }
        return log;
    }

    public static void StartTimers(List<Tuple<string[], Stopwatch>> processToMonitorTuple)
    {
        foreach (var process in processToMonitorTuple)
        {
            process.Item2.Start();
        }
    }

    public static bool CheckCommandInput(List<Tuple<string[], Stopwatch>> processToMonitorTuple)
    {
        foreach (var process in processToMonitorTuple)
        {
            var processCommand = process.Item1;
            bool onlyLetters = true;
            if (processCommand.ToList().Count != 3)
            {
                return false;
            }
            foreach (var command in processCommand)
            {
                if (int.TryParse(command, out int pc))
                {
                    onlyLetters = false;
                    if (pc < 0)
                    {
                        return false;
                    }
                }
                
                if (!command.All(c => Char.IsLetterOrDigit(c)))
                {
                    return false;
                }
            }
            if (onlyLetters)
            {
                return false;
            }
        }

        return true;
    }
}