# processes-monitor
Test Task to monitor processes

Monitor:
This command line utility expects three arguments: a process name, its 
maximum lifetime (in minutes) and a monitoring frequency (in minutes, as 
well). When you run the program, it starts monitoring processes with the 
frequency specified. If a process of interest lives longer than the allowed
duration, the utility kills the process and adds the corresponding record to the 
log (named KillLog). When no process exists at any given moment, the utility continues 
monitoring (new processes might appear later). The utility stops when a 
special keyboard button is pressed (q, Enter).

Test Monitor:
TestMonitor.MonitorProgramsTests:
  Expected KillLog Tests
  Invalid Format Tests
TestMonitor.InvalidFormatsTests:
  With 0 Commands
  With 1 Commands
  With 2 Commands
