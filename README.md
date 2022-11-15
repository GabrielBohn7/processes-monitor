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

Example: Monitor.exe monitor 2 1

Test Monitor:
Before running each test, make sure the following processes are not already running: notepad, wscript and mspaint

1. TestMonitor.MonitorProgramsTests:
   - Expected KillLog Tests
     - With 0 running processes
     - With 0 valued frequency
     - With 0 valued lifetime
     - With 1 running processes to monitor
     - With 2 equal running processes to monitor
     - With 2 running processes after killing procces to monitor
     - With 2 running processes to monitor
     - With 3 running processes to monitor
   - Invalid Format Tests
     - With 0 arguments
     - With 0 numeric values
     - With 2 arguments
     - With 4 arguments
     - With negative valued arguments
     - With incomplete valued arguments
     - With non alphanumeric arguments

2. TestMonitor.InvalidFormatsTests:
   - With 0 Commands
   - With 1 Command
   - With 2 Commands


